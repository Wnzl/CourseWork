using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace CourseProject
{
    public partial class TargetFunctionGraph : Form
    {
        public TargetFunctionGraph()
        {
            InitializeComponent();
            // get a reference to the GraphPane
            GraphPane myPane = zedGraphControl1.GraphPane;
            zedGraphControl1.IsShowHScrollBar = true;
            zedGraphControl1.IsShowVScrollBar = true;
            zedGraphControl1.IsAutoScrollRange = true;
            myPane.CurveList.Clear();
            // Set the Titles
            myPane.Title.Text = "Графік змін цільової функції";
            myPane.XAxis.Title.Text = "Ітерації";
            myPane.YAxis.Title.Text = "Значення цільової функції";
            
            PointPairList targetFunctionPointsList = new PointPairList();

            double [] targetFunctionPoints = IO.ListOfFunctionPoints.Points;
            int numberOfPoints = IO.ListOfFunctionPoints.Points.Length;

            for (int pointNumber = 0; pointNumber < numberOfPoints; pointNumber++)
            {
                double x = Convert.ToDouble(pointNumber);
                targetFunctionPointsList.Add(x, targetFunctionPoints[pointNumber]);
            }

            // Generate a red curve
            LineItem myCurve = myPane.AddCurve("", targetFunctionPointsList, Color.Red, SymbolType.None);
            //Щоб не показувало значення як число, "масштабоване" в степені
            myPane.YAxis.Scale.MagAuto = false;

            //-----Додаємо сітку для графіка-----
            
            // Включаем отображение сетки напротив крупных рисок по оси X
            myPane.XAxis.MajorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси X:
            // Длина штрихов равна 10 пикселям, ... 
            myPane.XAxis.MajorGrid.DashOn = 10;

            // затем 5 пикселей - пропуск
            myPane.XAxis.MajorGrid.DashOff = 5;


            // Включаем отображение сетки напротив крупных рисок по оси Y
            myPane.YAxis.MajorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            myPane.YAxis.MajorGrid.DashOn = 10;
            myPane.YAxis.MajorGrid.DashOff = 5;


            // Включаем отображение сетки напротив мелких рисок по оси X
            myPane.YAxis.MinorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси Y: 
            // Длина штрихов равна одному пикселю, ... 
            myPane.YAxis.MinorGrid.DashOn = 1;

            // затем 2 пикселя - пропуск
            myPane.YAxis.MinorGrid.DashOff = 2;

            // Включаем отображение сетки напротив мелких рисок по оси Y
            myPane.XAxis.MinorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            myPane.XAxis.MinorGrid.DashOn = 1;
            myPane.XAxis.MinorGrid.DashOff = 2;
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
    }
}
