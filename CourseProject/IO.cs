using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CourseProject
{
    class IO
    {
        /// <summary>
        /// Очищення вказаного об'єкту dataGridView
        /// </summary>
        /// <param name="dataGridView">dataGridView для очищення</param>
        private static void clearGrid(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.Refresh();
        }

        /// <summary>
        /// Розмітка таблиць для введення даних
        /// </summary>
        public static void buildMatrix(DataGridView dataGridView1, DataGridView dataGridView2, int colsNum, int rowsNum)
        {
            //Очистимо Grid
            clearGrid(dataGridView1);
            clearGrid(dataGridView2);
            
            //Створимо таблицю для введення вектора цільової функції
            for (int i = 0; i < colsNum; i++)
            {
                int colIndex = i + 1;
                string colName = "x" + colIndex.ToString();
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
                { Name = colName, HeaderText = colName, Width = 38 });
            }

            //Створимо таблицю для введення матриці обмежень
            //Додаємо колонки для Ax
            for (int i = 0; i < colsNum; i++)
            {
                int colIndex = i + 1;
                string colName = "a" + colIndex.ToString();
                dataGridView2.Columns.Add(new DataGridViewTextBoxColumn()
                { Name = colName, HeaderText = colName, Width = 38 });
            }
            //Колонка для b
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn()
            { Name = "b", HeaderText = "b", Width = 38 });

            //Додаємо рядки
            dataGridView1.RowCount = 1;
            dataGridView2.RowCount = rowsNum;
        }

        /// <summary>
        /// Отримання вектора цільової функції (масив)
        /// </summary>
        public static decimal[] getTargetFunction(object sender, EventArgs e, DataGridView dataGridView1)
        {
            int colsNum = dataGridView1.ColumnCount;
            int colsOfFunction = colsNum;
            decimal[] targetFunction = new decimal[colsOfFunction];
            for (int i = 0; i < colsOfFunction; i++)
                targetFunction[i] = Convert.ToDecimal(dataGridView1.Rows[0].Cells[i].Value);
            return targetFunction;
        }

        /// <summary>
        /// Отримання матриці обмежень (масив)
        /// </summary>
        public static decimal[,] getLimitationMatrix(object sender, EventArgs e, DataGridView dataGridView)
        {
            int colsNum = Convert.ToInt16(dataGridView.ColumnCount);
            int rowsNum = Convert.ToInt16(dataGridView.RowCount);
            int colsOfMassive = colsNum;
            decimal[,] limitationMatrix = new decimal[rowsNum, colsOfMassive];
            //Індекс b = кількість колонок - 1
            int bIndex = colsNum - 1;

            //забираємо значення b
            for (int rowIndex = 0; rowIndex < rowsNum; rowIndex++)
                limitationMatrix[rowIndex, 0] = Convert.ToDecimal(dataGridView.Rows[rowIndex].Cells[bIndex].Value);

            //забираємо значення A
            //colMassive для виводу значень A в масив береться індекс на 1 більше, перший займає b
            //colsOfFunction - 2, адже беремо лише значення А
            for (int rowIndex = 0; rowIndex < rowsNum; rowIndex++) 
                for (int colGridIndex = 0, colIndexMassive = 1; colGridIndex < colsNum - 1; colGridIndex++, colIndexMassive++)
                {
                    limitationMatrix[rowIndex, colIndexMassive] = Convert.ToDecimal(dataGridView.Rows[rowIndex].Cells[colGridIndex].Value);
                }

            /*string a = ""; //це було для перевірки масиву
            for (int row = 0; row < limitationMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < limitationMatrix.GetLength(1); col++)
                    a += limitationMatrix[row, col].ToString() + " ";
                a += "\r\n";
            }
            MessageBox.Show(a);*/
            return limitationMatrix;
        }

        /// <summary>
        /// Отримання значення куди прямує функція MIN/MAX. TRUE при значенні Max
        /// </summary>
        public static bool getMaxMin(object sender, EventArgs e, ComboBox MaxMinBox)
        {
            if (MaxMinBox.SelectedItem == null)
                MessageBox.Show("Не обране значення Max/Min. За замовчуванням обереться Max");
            else
            {
                if (MaxMinBox.SelectedItem.ToString() == "Min")
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Виведення результату в TextBox
        /// </summary>
        public static string writeSolve(SimplexTable simplexTable)
        {
            int xCount = simplexTable.X.GetLength(1) - 2;
            int simplexTableLength = simplexTable.X.GetLength(0);
            string answer = "";
            for(int index = 1; index <= xCount; index++)
            {
                for (int i = 0; i < simplexTableLength; i++)
                {
                    if (simplexTable.X[i, 0] == index)
                    {
                        answer += "x" + index + " = " + simplexTable.X[i, 1] + "\r\n";
                        break;
                    }
                    else if (i == simplexTableLength - 1)
                        answer += "x" + index + " = " + 0 + "\r\n";
                }
            }
            answer += "\r\nFs = { ";

            foreach (int Fs in simplexTable.fs)
                {
                    answer += "A" + Fs.ToString() + " ";
                }
            answer += "}\r\nL = " + simplexTable.L.ToString() + "\r\n";
            return answer;
        }

        /// <summary>
        /// Виведення детального розв'язку в html-файл
        /// </summary>
        public static void drowSolve(SimplexTable[] tables)
        {
            string solveString = "<!DOCTYPE html>\r\n<html lang='uk'>\r\n<head>\r\n<title>Детальний розв'язок</title>\r\n<style>\r\ndiv{font-size: 14pt; padding: 10px 0px;}\r\n</style>\r\n</head>\r\n<body>";
            //Вивід початку таблиці
            int lastTable = tables.GetLength(0);
            for (int currentTable = 0; currentTable < lastTable; currentTable++)
            {
                solveString += "<table border = '1'><tr><td></td><td></td><td></td><td>C</td>";
                SimplexTable table = tables[currentTable];
                SimplexTable previousTable = new SimplexTable();
                if (currentTable != 0) {
                    previousTable = tables[currentTable - 1]; //Предыдущая таблица для вывода checkRow (косяк с записью этой строки в таблицы)
                }
                //Цикл значень С
                foreach (int i in table.C)
                {
                    solveString += "<td>" + i + "</td>";
                }
                solveString += "<td></td></tr>";
                //Ще заголовки, 2-й ряд
                solveString += "<tr><td>N</td><td>Cs</td><td>Fs</td>";
                //Виводимо індекси А в заголовку
                for (int i = 0; i < table.X.GetLength(1) - 1; i++)
                {
                    solveString += "<td>A" + i + "</td>";
                }
                solveString += "<td>&Theta;</td></tr>";
                int index = 0;
                for (; index < table.X.GetLength(0); index++)
                {
                    //Виводимо номер, Cs, Fs
                    solveString += "<tr><td>" + (index + 1) + "</td><td>" + table.Cs[index] + "</td><td>" + table.fs[index] + "</td>";
                    //Цикл для значень Х
                    for (int i = 1; i < table.X.GetLength(1); i++)
                        solveString += "<td>" + table.X[index, i] + "</td>";
                    //Вывод теты
                    if (table.situation == 3)
                    { //Проверяем не является ли табилца последней (в последней не считаем тету)
                        if (table.teta[index] != 99999999999999)
                            solveString += "<td>" + table.teta[index] + "</td>";
                        else
                            solveString += "<td>---</td>";
                        solveString += "</tr>";
                    }
                }
                //Вывод дельты
                solveString += "<tr><td>" + (index + 1) + "</td><td></td><td>&Delta;</td>";
                for (int i = 0; i < table.delta.GetLength(0); i++)
                {
                    solveString += "<td>" + table.delta[i] + "</td>";
                }
                solveString += "</tr>";
                //Сравниваем дельту с "тестовыми данным", если совпадает значит все ок
                if (currentTable != 0)
                { //Проверяем не является ли таблица первой (для первой не нужна эта проверка)
                    solveString += "<tr><td>" + (index + 1) + "'</td><td></td><td></td>";
                    for (int i = 0; i < previousTable.checkRow.GetLength(0); i++)
                    {
                        solveString += "<td>" + previousTable.checkRow[i] + "</td>";
                    }
                }
                solveString += "</tr></table>";
                if (table.situation == 3)
                {
                    solveString += "<div style='height: 50px;'>Ітерація: " + currentTable + ", " +
                                    "\nситуація = " + table.situation + ", "+
                                    "\nобране r = " + table.r + ", " +
                                    "\nобране k = " + table.k + ".</div>";

                }
                if (table.situation == 2)
                {
                    //тут нельзя найти решение
                }
                if (table.situation == 1)
                {
                    // тут нашли решение и может быть стоит вывести в хтмл
                }

            }
            solveString += "</body>\r\n</html>";
            using (StreamWriter sw = new StreamWriter("out.html", false, Encoding.Default)) //false вказує, що файл буде перезаписано.
            {
                sw.WriteLine(solveString);
            }
        }

        /// <summary>
        /// Виведення перевірки допустимості в html-файл
        /// </summary>
        public static void drowAdmissibility(SimplexTable[] tables)
        {
            string solveString = "<!DOCTYPE html>\r\n<html lang='uk'>\r\n<head>\r\n<title>Перевірка допустимості</title>\r\n<style>\r\ndiv{font-size: 14pt; padding: 10px 0px;}\r\n</style>\r\n</head>\r\n<body>";
            solveString += "Після розв'язку отримуємо опорний план: <br>";
            int lastTable = tables.GetLength(0) - 1;
            int xCount = tables[lastTable].X.GetLength(1) - 2;
            int simplexTableLength = tables[lastTable].X.GetLength(0);
            solveString += "x* = { ";
            Boolean xIsPositive = true;
            decimal[] xResults = new decimal[xCount];
            for (int index = 1; index <= xCount; index++)
            {
                for (int i = 0; i < simplexTableLength; i++)
                {
                    if (tables[lastTable].X[i, 0] == index)
                    {
                        if (tables[lastTable].X[i, 0] < 0)
                            xIsPositive = false;
                        solveString += tables[lastTable].X[i, 1] + "; ";
                        xResults[index-1] = tables[lastTable].X[i, 1];
                        break;
                    }
                    else if (i == simplexTableLength - 1)
                    {
                        solveString += "0; ";
                        xResults[index-1] = 0;
                    }
                }
            }
            solveString += "} <br><b>";
            if (xIsPositive == true)
                solveString += "x* >= 0 <br>";
            else
                solveString += "x* <= 0 <br>";

            solveString += "</b>Перевіримо виконання умов Ax* = b <br>";

            decimal[] gamma = new decimal[simplexTableLength];
            for (int row = 0; row < simplexTableLength; row++)
            {
                gamma[row] = 0;
                for(int currentX = 2; currentX < xCount; currentX++)
                {
                    gamma[row] += tables[0].X[row, currentX] * xResults[currentX - 2];
                    solveString += tables[0].X[row, currentX]+ " * "+ xResults[currentX - 2] + " + ";
                }
                solveString += " = " + gamma[row];
                solveString += "<br><b>&gamma; " + (row+1) + "= " + tables[0].X[row,1] +" - "+ gamma[row] + " = "+ (tables[0].X[row, 1] - gamma[row]) + "</b><br>";

            }

            solveString += "</body>\r\n</html>";
            using (StreamWriter sw = new StreamWriter("admissibility.html", false, Encoding.Default)) //false вказує, що файл буде перезаписано.
            {
                sw.WriteLine(solveString);
            }
        }

        /// <summary>
        /// Збереження даних з таблиць в txt-файл
        /// </summary>
        public static void saveMatrix(DataGridView dataGridView1, DataGridView dataGridView2, bool MaxMin)
        {
            try
            {
                //Дізнаємося кількість рядків/колонок
                int columnsCFirst = dataGridView1.ColumnCount;
                int columnsAFirst = dataGridView2.ColumnCount;
                int rowsAFirst = dataGridView2.RowCount;
                int columnsCFirstMinusOne = columnsCFirst - 1;
                int columnsAFirstMinusOne = columnsAFirst - 1;
                int rowsAFirstMinusOne = rowsAFirst - 1;

                //Перевіряємо, чи є рядки/колонки
                if (columnsCFirst < 2 || columnsAFirst < 2)
                {
                    MessageBox.Show("Таблиці не існують!\n Створіть таблиці та заповність їх.", "Помилка збереження");
                }
                else {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
                saveFileDialog1.Title = "Оберіть файл, куди зберегти";

                    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                        string saveString = "";

                        for (int i = 0; i < columnsCFirst; i++)
                        {
                            saveString += dataGridView1.Rows[0].Cells[i].Value;
                            if (i < columnsCFirst - 1) saveString += " ";
                        }
                        //Додамо значення MaxMin
                        saveString += " " + Convert.ToInt32(MaxMin) + "\n";

                        for (int row = 0; row < rowsAFirst; row++)
                        {
                            for (int col = 0; col < columnsAFirst; col++)
                            {
                                saveString += dataGridView2.Rows[row].Cells[col].Value;
                                if (col < columnsAFirstMinusOne) saveString += " ";
                            }
                            if (row < rowsAFirstMinusOne) saveString += "\n";
                        }
                        using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, Encoding.Default)) //false вказує, що файл буде перезаписано.
                        {
                            sw.WriteLine(saveString);
                        }
                        MessageBox.Show("Матрицю успішно збережено у файл "+ saveFileDialog1.FileName, "Збережено");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка при збереженні матриці у файл", "Помилка при збереженні");
            }
        }

        /// <summary>
        /// Збереження даних в таблиці з txt-файлу
        /// </summary>
        public static void loadMatrix(DataGridView dataGridView1, DataGridView dataGridView2, ComboBox MaxMin)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Text files (*.txt)|*.txt";
                openFileDialog1.Title = "Оберіть файл матриці";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Зчитування даних у масиви
                    string[] fullMatrix = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                    string[] temporaryMatrix = fullMatrix[0].Split(' ');
                    //Визначення розмірності таблиць
                    int cols = temporaryMatrix.Length;
                    int rows = fullMatrix.Length - 1; //Розмір Ax=b без урахування Cx=L
                    int colsC = cols - 1;

                    //Будуємо таблиці
                    IO.buildMatrix(dataGridView1, dataGridView2, cols - 1, rows);

                    //Прямування MaxMin
                    if (temporaryMatrix[cols - 1] == "1") MaxMin.SelectedIndex = 0;
                    else MaxMin.SelectedIndex = 1;

                    //Заповнюємо таблицю цільової функції
                    for (int col = 0; col < colsC; col++)
                    {
                        dataGridView1.Rows[0].Cells[col].Value = temporaryMatrix[col];
                    }

                    //Заповнюємо таблицю обмежень
                    for (int row = 0, rowFullMatrix = 1; row < rows; row++, rowFullMatrix++)
                    {
                        temporaryMatrix = fullMatrix[rowFullMatrix].Split(' ');
                        for (int col = 0; col < cols; col++)
                        {
                            dataGridView2.Rows[row].Cells[col].Value = temporaryMatrix[col];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка при завантаженні даних. Можливо, файл матриці записаний невірно.", "Помилка при завантаженні даних");
            }
        }

        public static void saveResult(TextBox AnswerBox)
        {
            try
            {
                if(AnswerBox.Text == "")
                {
                    throw new System.ArgumentException("Поле відповіді пусте. Збереженян неможилве", "Помилка збереженя відповіді");
                }
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
                    saveFileDialog1.Title = "Оберіть файл, куди зберегти";

                    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                        string saveString = AnswerBox.Text;
                        
                        using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, Encoding.Default)) //false вказує, що файл буде перезаписано.
                        {
                            sw.WriteLine(saveString);
                        }
                        MessageBox.Show("Результати обчислень успішно збережені у файл " + saveFileDialog1.FileName, "Збережено");
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка при збереженні результатів у файл", "Помилка при збереженні");
            }
        }
    }
}
