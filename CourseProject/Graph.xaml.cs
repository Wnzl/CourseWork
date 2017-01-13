using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseProject {
    /// <summary>
    /// Логика взаимодействия для Graph.xaml
    /// </summary>
    public partial class Graph : Window
    {
        decimal xMin, xMax, yMin, yMax;
        decimal MouseX0, MouseY0, MouseX, MouseY; 
        decimal Epsilon = 0.0000000001m;
        decimal Coef;
        bool CheckLeft, CheckStop;
        int First, Second;
        polinom[] eqations = new polinom[] { };
        decimal[] L;
        decimal[] XLines = new decimal[] { };
        List<decimal> Lyambda1, Lyambda2;
        string[] Signs = new string[] { };
        string[] XSigns = new string[] { };
        Matrix Solution;
        decimal[] y;
        int roundValue;
        SimplexTable[] results;

        public Graph(SimplexTable[] results, int First, int Second, decimal[] y, int roundValue) {
            InitializeComponent();
            this.First = First;
            this.Second = Second;
            this.y = y;
            this.roundValue = roundValue;
            this.results = results;
            db1.Text = First + "";
            db2.Text = Second + "";
        }

        private Matrix FindDDeltaB(int First, int Second) {
            Matrix C;
            int rows = results[0].A.GetLength(0); // количество ограничений
            int lastTable = results.GetLength(0) - 1;
            C = new Matrix(new decimal[][] { new decimal[] { 1, 0, -results[0].X[First, 1] }, new decimal[] { 0, 1, -results[0].X[Second, 1] } });
            decimal[,] Afs = SimplexMethod.formAfs(results);
            decimal[,] inversedAfs = SimplexMethod.inverseMatrix(Afs);
            //тут нужно получить обратную Афс
            for (int i = 0; i < rows; i++) {
                if (inversedAfs[i, First] != 0 || inversedAfs[i, Second] != 0)
                    C = C.Add(new Matrix(Directions.Horizontal, new decimal[] {
                        inversedAfs[i,First], //первый выбраный столбец b
                        inversedAfs[i,Second], // второй столбец б
                        - results[lastTable].X[i,1]})); //значение x 
            }
            return C;
        }

        /// <summary>
        /// Определение где находится y относительно (x z)
        /// 0 - на линии, >0 - справа, <0 - слева
        /// </summary>
        private decimal CheckAngle(decimal[] x, decimal[] y, decimal[] z)
        {
            return (z[0] - x[0]) * (y[1] - x[1]) - (z[1] - x[1]) * (y[0] - x[0]);
        }
        //сортировка точек, недоработан (при большом кол-ве точек в одной плоскости, возможна ошибка)
        private void SortSolutions()
        {
            Matrix C;
            int[] array = new int[Solution.Row];
            Dictionary<string, decimal[]> Angles = new Dictionary<string, decimal[]>();
            Angles.Add("Top", new decimal[] { Solution[0][1], 0 });
            Angles.Add("Right", new decimal[] { Solution[0][0], 0 });
            Angles.Add("Bottom", new decimal[] { Solution[0][1], 0 });
            Angles.Add("Left", new decimal[] { Solution[0][0], 0 });
            for (int i = 0; i < Solution.Row; i++)
            {
                array[i] = -1;
                if (Angles["Top"][0] < Solution[i][1])
                {
                    Angles["Top"][0] = Solution[i][1];
                    Angles["Top"][1] = i;
                }
                if (Angles["Bottom"][0] > Solution[i][1])
                {
                    Angles["Bottom"][0] = Solution[i][1];
                    Angles["Bottom"][1] = i;
                }
                if (Angles["Left"][0] > Solution[i][0])
                {
                    Angles["Left"][0] = Solution[i][0];
                    Angles["Left"][1] = i;
                }
                if (Angles["Right"][0] < Solution[i][0])
                {
                    Angles["Right"][0] = Solution[i][0];
                    Angles["Right"][1] = i;
                }
            }
            C = new Matrix(0, 2);
            int tl = 0, tr = 0, br = 0, bl = 0;
            for (int i = 0; i < Solution.Row; i++)
            {
                if (CheckAngle(Solution[(int)Angles["Left"][1]], Solution[i], Solution[(int)Angles["Top"][1]]) > 0)
                {
                    Angles.Add("TL" + tl, new decimal[] { Solution[i][1], i });
                    tl++;
                }
                if (CheckAngle(Solution[(int)Angles["Top"][1]], Solution[i], Solution[(int)Angles["Right"][1]]) > 0)
                {
                    Angles.Add("TR" + tr, new decimal[] { Solution[i][1], i });
                    tr++;
                }
                if (CheckAngle(Solution[(int)Angles["Bottom"][1]], Solution[i], Solution[(int)Angles["Right"][1]]) < 0)
                {
                    Angles.Add("BR" + br, new decimal[] { Solution[i][1], i });
                    br++;
                }
                if (CheckAngle(Solution[(int)Angles["Left"][1]], Solution[i], Solution[(int)Angles["Bottom"][1]]) < 0)
                {
                    Angles.Add("BL" + bl, new decimal[] { Solution[i][1], i });
                    bl++;
                }
            }
            C = new Matrix(Directions.Horizontal, Solution[(int)Angles["Bottom"][1]]);
            for (int i = 0; i < br; i++)
                C = C.Add(new Matrix(Directions.Horizontal, Solution[(int)Angles["BR" + i][1]]));
            C = C.Add(new Matrix(Directions.Horizontal, Solution[(int)Angles["Right"][1]]));
            for (int i = 0; i < tr; i++)
                C = C.Add(new Matrix(Directions.Horizontal, Solution[(int)Angles["TR" + i][1]]));
            C = C.Add(new Matrix(Directions.Horizontal, Solution[(int)Angles["Top"][1]]));
            for (int i = 0; i < tl; i++)
                C = C.Add(new Matrix(Directions.Horizontal, Solution[(int)Angles["TL" + i][1]]));
            C = C.Add(new Matrix(Directions.Horizontal, Solution[(int)Angles["Left"][1]]));
            for (int i = 0; i < bl; i++)
                C = C.Add(new Matrix(Directions.Horizontal, Solution[(int)Angles["BL" + i][1]]));
            Solution = C;
        }
        //вызов функции отрисовки с учетом маштаба на скроллбарах
        public void Drow()
        {
            if (Math.Abs(xMax - xMin) > 2)
                DrowGraph((int)(xMin), 
                          (int)(xMax), 
                          (int)(yMin), 
                          (int)(yMax));
        }
        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!CheckLeft) { MouseX0 = (decimal)e.GetPosition(null).X; MouseY0 = (decimal)e.GetPosition(null).Y; CheckLeft = true; }
                if (CheckLeft)
                {
                    MouseY = (decimal)e.GetPosition(null).Y - MouseY0;
                    MouseX = (decimal)e.GetPosition(null).X - MouseX0;
                    Drow();
                }
            }
            else
            {
                if (CheckLeft)
                {
                    CheckStop = true;
                    Drow();
                    MouseX0 = 0;
                    MouseY0 = 0;
                    MouseY = 0;
                    MouseX = 0;
                    CheckLeft = false;

                }
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void button_Click(object sender, RoutedEventArgs e) {
            try {
                this.First = int.Parse(db1.Text) - 1;
                this.Second = int.Parse(db2.Text) - 1;
                if (First > 12 - 1 || Second > 12 - 1 || First < 0 || Second < 0) // тут нужно норм задать количество ограничений
                    MessageBox.Show("Введених номерів обмежень не існує в задачі!");
                else {
                    this.Cursor = Cursors.Wait;
                    Solution = null;
                    Array.Resize(ref eqations, 0);
                    Array.Resize(ref Signs, 0);
                    Matrix DDeltaB = FindDDeltaB(First, Second);
                    string result = "";
                    for (int i = 0; i < DDeltaB.Row; i++) {
                        if (DDeltaB[i][1] < Epsilon && DDeltaB[i][1] > -Epsilon) {
                            Array.Resize(ref XLines, XLines.Length + 1);
                            XLines[XLines.Length - 1] = DDeltaB[i][2] / DDeltaB[i][0];
                            Array.Resize(ref XSigns, XSigns.Length + 1);
                            if (DDeltaB[i][0] > 0) XSigns[XSigns.Length - 1] = ">=";
                            else XSigns[XSigns.Length - 1] = "<=";
                        } else {
                            Array.Resize(ref eqations, eqations.Length + 1);
                            Array.Resize(ref Signs, Signs.Length + 1);
                            eqations[eqations.Length - 1] = new polinom();
                            eqations[eqations.Length - 1].Compile(-DDeltaB[i][0] / DDeltaB[i][1] + "*x +(" + DDeltaB[i][2] / DDeltaB[i][1] + ")");
                            if (DDeltaB[i][1] > 0) Signs[Signs.Length - 1] = ">=";
                            else Signs[Signs.Length - 1] = "<=";
                        }
                        for (int j = i + 1; j < DDeltaB.Row; j++) {
                            if (!(DDeltaB[i][0] < Epsilon && DDeltaB[i][0] > -Epsilon) || !(DDeltaB[j][0] < Epsilon && DDeltaB[j][0] > -Epsilon)) {
                                Matrix C = DDeltaB.GetRows(i, j).Reshenie();
                                bool flag = false;
                                for (int k = 0; k < DDeltaB.Row; k++) {
                                    decimal q = DDeltaB[k][0] * C[0][0] + DDeltaB[k][1] * C[0][1];
                                    decimal t = DDeltaB[k][2];
                                    if (Math.Round(DDeltaB[k][0] * C[0][0] + DDeltaB[k][1] * C[0][1], roundValue) < Math.Round(DDeltaB[k][2], roundValue)) {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag)
                                    if (Solution != null) Solution = Solution.Add(C);
                                    else Solution = new Matrix(C);
                            }

                        }
                        for (int j = 0; j < DDeltaB.Col; j++) {
                            result += DDeltaB[i, j] != 0 ? (DDeltaB[i, j] == 1 ? "" : DDeltaB[i, j] == -1 ? "-" : Math.Round(DDeltaB[i, j], roundValue).ToString()) +
                                (j < DDeltaB.Col - 1 ? "Δb" + (j == 0 ? First + 1 : Second + 1) : "") : "";
                            result += (j == DDeltaB.Col - 2 ? "≥" : j == DDeltaB.Col - 1 || DDeltaB[i, 1] == 0 || DDeltaB[i, 0] == 0 ? "" : DDeltaB[i, j + 1] < 0 ? " " : " +");
                        }
                        result += "\n";
                    }
                    Matrix D = Matrix.Transpose(Solution);
                    xMin = 1.2m * FindMin(D[0]);
                    xMax = 1.2m * FindMax(D[0], false);
                    yMin = 1.2m * FindMin(D[1]);
                    yMax = 1.2m * FindMax(D[1], false);
                    Label2.Text = result;
                    SortSolutions();
                    L = new decimal[Solution.Row];
                    for (int i = 0; i < Solution.Row; i++)
                        L[i] = Solution[i][0] * y[First] + Solution[i][1] * y[Second];
                    int nMax = FindMaxN(L, false), nMin = FindMinN(L);
                    Label2.Text += "\nΔLmax = " + FindMax(L, false) + "\nв (" +
                        Math.Round(Solution[nMax][0], roundValue) + "; " + Math.Round(Solution[nMax][1], roundValue) + ")";
                    Label2.Text += "\nΔLmin = " + FindMin(L) + "\nв (" +
                        Math.Round(Solution[nMin][0], roundValue) + "; " + Math.Round(Solution[nMin][1], roundValue) + ")";                
                    Drow();
                    this.Cursor = Cursors.Arrow;
                }
            }
            catch (Exception exc) {
                if(exc is DivideByZeroException) {
                    MessageBox.Show("Неможливо побудувати графік через необмеженість зверху");
                    return;
                }
                MessageBox.Show(exc.Message);
            }
        }

       
        //отрисовка всего
        private void DrowGraph(int xMin, int xMax, decimal yMin, decimal yMax)
        {
            Canvas c = CanvasGraph;
            c.Children.Clear();
            decimal width = (decimal)c.ActualWidth;
            decimal height = (decimal)c.ActualHeight;
            // Масштаби:
            decimal xScale = width / (xMax - xMin);
            decimal yScale = height / (yMax - yMin);

            decimal X0 = 1;
            //крайние точки отрисовки
            yMax += (int)(MouseY / yScale);
            yMin += (int)(MouseY / yScale);
            xMax -= (int)(MouseX / xScale);
            xMin -= (int)(MouseX / xScale);
            SolidColorBrush Brush;
            List<Point> points = new List<Point>(); // точки  дельта D
            decimal y1, y2, x1, x2;
            // начало координат
            int x0 = (int)(-xMin * xScale);
            int y0 = (int)(yMax * yScale);

            // пустой прямоугольник для корректной работы скрола мышкой
            c.Children.Add(new Rectangle() { Fill = Brushes.White, Width = c.ActualWidth, Height = c.ActualHeight });
            Label3.Text = "";
                for (int i = 0; i < Solution.Row; i++)
                {
                    x1 = x0 + Solution[i][0] * xScale;
                    y1 = y0 - Solution[i][1] * yScale;
                    if (!points.Contains(new Point((double)x1, (double)y1)))
                    {
                        points.Add(new Point((double)x1, (double)y1));
                        Label3.Text += char.ConvertFromUtf32(0x0041 + points.Count - 1) + " (" + Math.Round(Solution[i][0], 3) + "; " + Math.Round(Solution[i][1], 3) + ")\n";
                    }
                }
                //прорисовка ответа
                c.Children.Add(new Polygon()
                {
                    Fill = Brushes.Khaki,
                    Points = new PointCollection(points)
                });

            // сетка и цифры на оси:
            int xStep = 1; // шаг сетки
            int dd = xMin;
            for (int dx = xMin + xStep; dx < xMax; dx += xStep)
            {
                decimal x = x0 + dx * xScale;
                if (dx != 0 && (dx - dd) * xScale > (decimal)Math.Max(xMin.ToString().Length * 6, xMax.ToString().Length * 7.5))
                {                     
                    AddText(c, dx + "", x - 0.2m * xStep * xScale, y0 + 1);
                    AddLine(c, Brushes.LightGray, x, 0, x, height);
                    dd = dx;
                }
            }
            int yStep = 1; // Крок сітки
            dd = (int)yMin;
            for (int dy = (int)yMin + yStep; dy < yMax; dy += yStep)
            {
                decimal y = y0 - dy * yScale;
                if (dy != 0 && (dy - dd) * yScale > 13)
                {
                    if (Lyambda1 != null) if (Math.Abs(dy) > (yMax - yMin) * 0.02m) AddText(c, (int) (dy / Coef) + "", X0 + 2, y - 0.45m * yStep * yScale);
                    AddLine(c, Brushes.LightGray, 0, y, width, y);
                    if (Math.Abs(dy) > (yMax - yMin) * 0.02m) AddText(c, dy + "", x0 + 2, y - 0.45m * yStep * yScale);
                    dd = dy;
                }
            }

            // Осі:
            AddLine(c, Brushes.Black, x0, 0, x0, height);
            AddLine(c, Brushes.Black, x0 + 4, 7, x0, 0);
            AddLine(c, Brushes.Black, x0 - 4, 7, x0, 0);
            AddLine(c, Brushes.Black, 0, y0, width, y0);
            AddLine(c, Brushes.Black, width - 7, y0 + 4, width, y0);
            AddLine(c, Brushes.Black, width - 7, y0 - 4, width, y0);
            AddText(c, "0", x0 - 3, y0);
            
            // график
            if (Lyambda1 == null)
            {
                AddText(c, "Δb" + (First + 1), width - 25, y0 - 17);
                AddText(c, "Δb" + (Second + 1), x0 - 30, 2);
                for (int i = 0; i < eqations.Length; i++)
                {
                    y1 = (decimal)eqations[i].Find(xMin);
                    y2 = (decimal)eqations[i].Find(xMax);
                    Brush = Signs[i] == ">=" ? Brushes.Red : Brushes.Blue;
                    AddLine(c, Brush, x0 + xMin * xScale, y0 - y1 * yScale, x0 + xMax * xScale, y0 - y2 * yScale);
                    if (i < XLines.Length)
                    {
                        Brush = XSigns[i] == ">=" ? Brushes.Red : Brushes.Blue;
                        AddLine(c, Brush, x0 + XLines[i] * xScale, y0 - yMin * yScale, x0 + XLines[i] * xScale, y0 - yMax * yScale);
                    }
                }
                //точки ответа
                for (int i = 0; i < points.Count; i++)
                {
                    c.Children.Add(new Ellipse()
                    {
                        Fill = Brushes.Black,
                        Width = 6,
                        Height = 6,
                        Margin = new Thickness(points[i].X - 3, points[i].Y - 3, 0, 0)
                    });
                    AddText(c, char.ConvertFromUtf32(0x0041 + i), (decimal)points[i].X - 10, (decimal)points[i].Y - 15);
                }
                // легенда
                y1 = 20;
                y2 = 40;
                x1 = width - 55;
                x2 = x1 - 30;
                c.Children.Add(new Rectangle()
                {
                    Margin = new Thickness((double)x2 - 5, 0, 0, 0),
                    Stroke = Brushes.Black,
                    Fill = Brushes.WhiteSmoke,
                    Width = 90,
                    Height = 80
                });
                AddLine(c, Brushes.Blue, x1 - 10, y1 + 10, x2, y1 + 10);
                AddLine(c, Brushes.Red, x1 - 10, y2 + 10, x2, y2 + 10);
                AddText(c, "Знак прямой:", x2, y1 - 20);
                AddText(c, "≤", x1, y1);
                AddText(c, "≥", x1, y2);
                c.Children.Add(new Rectangle()
                {
                    Margin = new Thickness((double)x2, 60, 0, 0),
                    Stroke = Brushes.Black,
                    Fill = Brushes.Khaki,
                    Width = 25,
                    Height = 15
                });
                AddText(c, "DΔb", x1, y2 + 20);
            }
            else
            {
                X0 = x0 + Lyambda1.Count * xScale;
                dd = (int)yMin;
                AddLine(c, Brushes.Black, X0, 0, X0, height);
                AddLine(c, Brushes.Black, X0 + 4, 7, X0, 0);
                AddLine(c, Brushes.Black, X0 - 4, 7, X0, 0);
                AddText(c, "Итерация", width - 60, y0 - 17);
                AddText(c, "λ1", x0 - 15, 2);
                AddText(c, "λ2", X0 - 15, 2);
                for (int i = 0; i < Lyambda1.Count - 1; i++)
                {
                    x1 = x0 + i * xScale;
                    x2 = x0 + (i + 1) * xScale;
                    y1 = y0 - Lyambda1[i] * yScale * Coef;
                    y2 = y0 - Lyambda1[i + 1] * yScale * Coef;
                    AddLine(c, Brushes.Blue, x1, y1, x2, y2);
                    c.Children.Add(new Ellipse()
                    {
                        Fill = Brushes.Black,
                        Width = 6,
                        Height = 6,
                        Margin = new Thickness((double)x1 - 3, (double)y1 - 3, 0, 0)
                    });
                    if (i == Lyambda1.Count - 2)
                        c.Children.Add(new Ellipse()
                        {
                            Fill = Brushes.Black,
                            Width = 6,
                            Height = 6,
                            Margin = new Thickness((double)x2 - 3, (double)y2 - 3, 0, 0)
                        });
                    y1 = y0 - Lyambda2[i] * yScale;
                    y2 = y0 - Lyambda2[i + 1] * yScale;
                    AddLine(c, Brushes.Red, x1, y1, x2, y2);
                    c.Children.Add(new Ellipse()
                    {
                        Fill = Brushes.Black,
                        Width = 6,
                        Height = 6,
                        Margin = new Thickness((double)x1 - 3, (double)y1 - 3, 0, 0)
                    });
                    if (i == Lyambda1.Count - 2)
                        c.Children.Add(new Ellipse()
                        {
                            Fill = Brushes.Black,
                            Width = 6,
                            Height = 6,
                            Margin = new Thickness((double)x2 - 3, (double)y2 - 3, 0, 0)
                        });
                }
                y1 = height - 40;
                y2 = height - 20;
                x1 = width - 55;
                x2 = x1 - 30;
                c.Children.Add(new Rectangle()
                {
                    Margin = new Thickness((double)x2 - 5, (double)y1 - 20, 0, 0),
                    Stroke = Brushes.Black,
                    Fill = Brushes.WhiteSmoke,
                    Width = 90,
                    Height = 60
                });
                AddLine(c, Brushes.Blue, x1 - 10, y1 + 10, x2, y1 + 10);
                AddLine(c, Brushes.Red, x1 - 10, y2 + 10, x2, y2 + 10);
                AddText(c, "Функция:", x2, y1 - 20);
                AddText(c, "λ1", x1, y2);
                AddText(c, "λ2", x1, y1);
            }
            // остановка перетягивания графика мышкой
            if (CheckStop)
            {
                this.xMax = xMax;
                this.xMin = xMin;
                this.yMax = yMax;
                this.yMin = yMin;
                CheckStop = false;
            }
        }

        private void SizeChenged(object sender, SizeChangedEventArgs e)
        {
            Drow();
        }

        //добавление прямой
        private void AddLine(Canvas c, Brush stroke, decimal x1, decimal y1, decimal x2, decimal y2)
        {
            c.Children.Add(new Line() { X1 = (double)x1, X2 = (double)x2, Y1 = (double)y1, Y2 = (double)y2, Stroke = stroke });
        }

        // добавление текста
        private void AddText(Canvas c, string text, decimal x, decimal y)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = Brushes.Black;
            Canvas.SetLeft(textBlock, (double)x);
            Canvas.SetTop(textBlock, (double)y);
            c.Children.Add(textBlock);
        }

        private void CanvasGraph_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            decimal x = xMax - xMin, y = yMax - yMin;
            int deltaX = x > 10000 ? 1 : x > 5000 ? 2 : x > 2000 ? 3 : x > 1500 ? 6 : x > 500 ? 10 : x > 100 ? 15 : x > 50 ? 20 : 40;
            int deltaY = y > 10000 ? 1 : y > 5000 ? 2 : y > 2000 ? 3 : y > 1500 ? 6 : y > 500 ? 10 : y > 100 ? 15 : y > 50 ? 20 : 40;
            int stepX = e.Delta / deltaX, stepY = e.Delta / deltaY;
            if (e.XButton1 == MouseButtonState.Released)
            {
                if (xMin < -stepX) xMin += stepX;
                if (xMax > stepX) xMax -= stepX;
            }
            if (e.XButton2 == MouseButtonState.Released)
            {
                if (yMin < -stepY) yMin += stepY;
                if (yMax > stepY) yMax -= stepY;
            }
            Drow();
        }

        private decimal FindMax(decimal[] array, bool Abs = true) {
            decimal Max = array[0];
            if (Abs) {
                for (int i = 0; i < array.Length; i++)
                    if (Max < Math.Abs(array[i])) Max = Math.Abs(array[i]);
            } else
                for (int i = 0; i < array.Length; i++)
                    if (Max < array[i]) Max = array[i];
            return Max;
        }

        private decimal FindMin(decimal[] array) {
            decimal Min = array[0];
            for (int i = 1; i < array.Length; i++)
                if (Min > array[i]) Min = array[i];
            return Min;
        }

        private int FindMaxN(decimal[] array, bool Abs = true) {
            int Max = 0;
            if (Abs) {
                for (int i = 1; i < array.Length; i++)
                    if (Math.Abs(array[Max]) < Math.Abs(array[i])) Max = i;
            } else
                for (int i = 1; i < array.Length; i++)
                    if (array[Max] < array[i]) Max = i;
            return Max;
        }

        private int FindMinN(decimal[] array) {
            int Min = 0;
            for (int i = 1; i < array.Length; i++)
                if (array[Min] > array[i]) Min = i;
            return Min;
        }
    }
}
