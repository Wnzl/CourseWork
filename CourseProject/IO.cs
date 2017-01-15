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
                { Name = colName, HeaderText = colName, Width = 35 });
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
        public static string writeSolve(SimplexTable[] simplexTable, int roundValue)
        {
            int lastTable = simplexTable.GetLength(0) - 1;
            int xCount = simplexTable[lastTable].X.GetLength(1) - 2;
            int simplexTableLength = simplexTable[lastTable].X.GetLength(0);
            int situation = simplexTable[lastTable].situation;
            string answer = "";
            if(situation == 1) { 
            answer += "Отриманий опорний план \r\nFs = { ";

            foreach (int Fs in simplexTable[lastTable].fs) {
                answer += "A" + Fs.ToString() + " ";
            }
            answer += "}\r\n\r\nОтримані такі значення прямої задачі:\r\n";
            for (int index = 1; index <= xCount; index++)
            {
                for (int i = 0; i < simplexTableLength; i++)
                {
                    if (simplexTable[lastTable].X[i, 0] == index)
                    {
                        answer += "x" + index + " = " + Math.Round(simplexTable[lastTable].X[i, 1], roundValue) + "\r\n";
                        break;
                    }
                    else if (i == simplexTableLength - 1)
                        answer += "x" + index + " = " + 0 + "\r\n";
                }
            }
            answer += "\r\nОтримані такі значення зворотньої задачі:";
            decimal[] y = SimplexMethod.getY(simplexTable);
            int yCount = y.GetLength(0);
            for (int i = 0; i < yCount; i++) {
                answer += "\r\ny*" + (i+1) + " = " + Math.Round(y[i], roundValue) + ";";
            }
            answer += "\r\n\r\nЗначення цільової функції прямої та зворотньої задачі\r\nL = " + Math.Round(simplexTable[lastTable].L, roundValue).ToString() + "\r\n";
            decimal L2 = 0;
            for (int i = 0; i < simplexTableLength; i++) {
                L2 += simplexTable[0].X[i, 1] * y[i];
            }
            answer += "L* = " + Math.Round(L2, roundValue);
            return answer;
            }else {
                answer += "Задача не може бути розв'язана через необмеженість лінійної форми зверху на множині планів Dm";
                return answer;
            }
        }

        /// <summary>
        /// Виведення детального розв'язку в html-файл
        /// </summary>
        public static void drowSolve(SimplexTable[] tables, int roundValue)
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
                        solveString += "<td>" + Math.Round(table.X[index, i], roundValue) + "</td>";
                    //Вывод теты
                    if (table.situation == 3)
                    { //Проверяем не является ли табилца последней (в последней не считаем тету)
                        if (table.teta[index] != 99999999999999)
                            solveString += "<td>" + Math.Round(table.teta[index], roundValue) + "</td>";
                        else
                            solveString += "<td>---</td>";
                        solveString += "</tr>";
                    }
                }
                //Вывод дельты
                solveString += "<tr><td>" + (index + 1) + "</td><td></td><td>&Delta;</td>";
                for (int i = 0; i < table.delta.GetLength(0); i++)
                {
                    solveString += "<td>" + Math.Round(table.delta[i], roundValue) + "</td>";
                }
                solveString += "</tr>";
                //Сравниваем дельту с "тестовыми данным", если совпадает значит все ок
                if (currentTable != 0)
                { //Проверяем не является ли таблица первой (для первой не нужна эта проверка)
                    solveString += "<tr><td>" + (index + 1) + "'</td><td></td><td></td>";
                    for (int i = 0; i < previousTable.checkRow.GetLength(0); i++)
                    {
                        solveString += "<td>" + Math.Round(previousTable.checkRow[i], roundValue) + "</td>";
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
        public static void drowAdmissibility(SimplexTable[] tables, int roundValue)
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
                        solveString += Math.Round(tables[lastTable].X[i, 1], roundValue) + "; ";
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

            solveString += "</b><br><h2>Перевіримо виконання умов Ax* = b </h2></br>";

            decimal[] gamma = new decimal[simplexTableLength];
            for (int row = 0; row < simplexTableLength; row++)
            {
                gamma[row] = 0;
                for(int currentX = 2; currentX < xCount + 2; currentX++)
                {
                    gamma[row] += tables[0].X[row, currentX] * xResults[currentX - 2];
                    solveString += Math.Round(tables[0].X[row, currentX], roundValue) + " * "+ Math.Round(xResults[currentX - 2], roundValue) + " + ";
                }
                solveString += " = " + Math.Round(gamma[row], roundValue);
                solveString += "<br><b>&gamma; " + (row+1) + "= " + Math.Round(tables[0].X[row,1], roundValue) +" - "+ Math.Round(gamma[row], roundValue) + " = "+ Math.Round((tables[0].X[row, 1] - gamma[row]), roundValue) + "</b><br>";
            }

            //Вывод проверки обратной задачи
            solveString += "<br><h2>Виконаємо перевiрку зворотньої задачі</h2></br>";

            solveString += "<br>Для отримання у* використаємо результатами рішення прямої задачі: </br>";
            decimal[] y = SimplexMethod.getY(tables);
            int yCount = y.GetLength(0);
            solveString += "<br> y* = {";
            for(int i = 0; i < yCount; i++) {
                solveString += Math.Round(y[i],roundValue) + "; ";
            }
            solveString += "} </br>";

            int columns = tables[0].xCount;
            int rows = tables[0].X.GetLength(0);
            decimal[] yGamma = new decimal[columns];
            for (int column = 0; column < columns; column++) {
                yGamma[column] = 0;
                for (int row = 0; row < rows; row++) {
                    yGamma[column] += tables[0].A[row, column] * y[row];
                    solveString += Math.Round(tables[0].A[row, column], roundValue) + " * " + Math.Round(y[row], roundValue) + " + ";
                }
                solveString += " = " + Math.Round(yGamma[column], roundValue);
                solveString += "<br><b>&gamma; " + (column + 1) + "= " + Math.Round(tables[0].C[column], roundValue) + " - " + Math.Round(yGamma[column], roundValue) + " = " + Math.Round((tables[0].C[column] - yGamma[column]), roundValue) + "</b><br>";
                yGamma[column] = Math.Round((tables[0].C[column] - yGamma[column]), roundValue);
            }
            if(yGamma.Max() <= 0.0000001m) { 
            solveString += "<br> Максимальна &gamma; = " + yGamma.Max() + " <= epsilon (0.0000001) </br>";
            solveString += "<h1> Отримане рішення допустиме! </h1><br/>";
            }else {
                solveString += "<br> Максимальна &gamma; = " + yGamma.Max() + " > epsilon (0.0000001) </br>";
                solveString += "<h1> Отримане рішення не допустиме! </h1><br/>";
            }
            solveString += "</body>\r\n</html>";
            using (StreamWriter sw = new StreamWriter("admissibility.html", false, Encoding.Default)) //false вказує, що файл буде перезаписано.
            {
                sw.WriteLine(solveString);
            }
        }

        /// <summary>
        /// Виведення перевірки оптимальностi в html-файл
        /// </summary>
        public static void drowOptimality(SimplexTable[] tables, int roundValue) {
            string solveString = "<!DOCTYPE html>\r\n<html lang='uk'>\r\n<head>\r\n<title>Перевірка оптимальностi</title>\r\n<style>\r\ndiv{font-size: 14pt; padding: 10px 0px;}\r\n</style>\r\n</head>\r\n<body>";
            solveString += "<h2>В результаті роботи ПР були отримані такі значення: </h2><br>";
            solveString += "Fs* = {";
            int lastTable = tables.GetLength(0) - 1;
            int xCount = tables[lastTable].X.GetLength(1) - 2;
            int simplexTableLength = tables[lastTable].X.GetLength(0);
            int rows = tables[0].X.GetLength(0);
            for (int row = 0; row < rows; row++)
                solveString += "A" + tables[lastTable].fs[row] + ", ";
            solveString += "} <br>";

            solveString += "x* = { ";
            decimal[] xResults = new decimal[xCount];
            for (int index = 1; index <= xCount; index++) {
                for (int i = 0; i < simplexTableLength; i++) {
                    if (tables[lastTable].X[i, 0] == index) {
                        solveString += Math.Round(tables[lastTable].X[i, 1], roundValue) + "; ";
                        xResults[index - 1] = tables[lastTable].X[i, 1];
                        break;
                    } else if (i == simplexTableLength - 1) {
                        solveString += "0; ";
                        xResults[index - 1] = 0;
                    }
                }
            }
            solveString += "} <br>";

            decimal[] y = SimplexMethod.getY(tables);
            int yCount = y.GetLength(0);
            solveString += "<br> y* = {";
            for (int i = 0; i < yCount; i++) {
                solveString += Math.Round(y[i], roundValue) + "; ";
            }
            solveString += "} </br>";

            solveString += "<b>L1* = </b>";
            decimal L1 = 0;
            for (int i = 0; i < xCount; i++) {
                solveString += tables[0].C[i] + " * " + Math.Round(xResults[i],roundValue) +" + ";
                L1 += tables[0].C[i] * xResults[i];
                }
            solveString += " = <b>" + Math.Round(L1,roundValue) + "</b> <br>";

            solveString += "<b>L2* = </b>";
            decimal L2 = 0;
            for (int i = 0; i < simplexTableLength; i++) {
                solveString += tables[0].X[i,1] + " * " + Math.Round(y[i], roundValue) + " + ";
                L2 += tables[0].X[i,1] * y[i];
            }
            solveString += " = <b>" + Math.Round(L2, roundValue) + "</b> <br>";

            decimal L3 = tables[lastTable].delta[0];
            solveString += "<b>L3* = " + Math.Round(L3,roundValue) +"</b><br>";

            decimal Lcp = (L1 + L2 + L3) / 3;
            solveString += "<b>Lcp</b> = (" + Math.Round(L1, roundValue) + " + "
                                            + Math.Round(L2, roundValue) + " + "
                                            + Math.Round(L3, roundValue) + ") / 3 = <b>" 
                                            + Math.Round(Lcp,roundValue) + "</b><br>";
            int counter = 0;

            solveString += "|" + Math.Round(Lcp, roundValue) + " - " + Math.Round(L1, roundValue) + "| = <b>" + Math.Round(Math.Abs(Lcp - L1), roundValue);
            if (Math.Abs(Lcp - L1) <= 0.0000001m)
                solveString += " <= epsilon (0,0000001) </b><br>";
            else {
                solveString += " > epsilon (0,0000001) </b><br>";
                counter++;
            }
            solveString += "|" + Math.Round(Lcp, roundValue) + " - " + Math.Round(L2, roundValue) + "| = <b>" + Math.Round(Math.Abs(Lcp - L2),roundValue);
            if (Math.Abs(Lcp - L2) <= 0.0000001m)
                solveString += " <= epsilon (0,0000001) </b><br>";
            else {
                solveString += " > epsilon (0,0000001) </b><br>";
                counter++;
            }
            solveString += "|" + Math.Round(Lcp, roundValue) + " - " + Math.Round(L3, roundValue) + "| = <b>" + Math.Round(Math.Abs(Lcp - L3), roundValue);
            if (Math.Abs(Lcp - L3) <= 0.0000001m)
                solveString += " <= epsilon (0,0000001) </b><br>";
            else {
                solveString += " > epsilon (0,0000001) </b><br>";
                counter++;
            }

            if (counter == 0)
                solveString += "<h2>Отриманий план є оптимальним! </h2>";
            else
                solveString += "<h2>Отриманий план не є оптимальним! </h2>";
            solveString += "</body>\r\n</html>";
            using (StreamWriter sw = new StreamWriter("optimality.html", false, Encoding.Default)) //false вказує, що файл буде перезаписано.
            {
                sw.WriteLine(solveString);
            }
        }

        /// <summary>
        /// Виведення перевірки опорности в html-файл
        /// </summary>
        public static void drowReference(SimplexTable[] tables, int roundValue) {
            string solveString = "<!DOCTYPE html>\r\n<html lang='uk'>\r\n<head>\r\n<title>Перевірка опорности</title>\r\n<style>\r\ndiv{font-size: 14pt; padding: 10px 0px;}\r\n</style>\r\n</head>\r\n<body>";
            solveString += "В результаті роботи ПР був отриманий такий опорний план: <br>";
            solveString += "Fs* = {";
            int lastTable = tables.GetLength(0) - 1;
            int rows = tables[0].X.GetLength(0);
            for (int row = 0; row < rows; row++)
                solveString += "A" + tables[lastTable].fs[row] + ", ";
            solveString += "} <br>";
            solveString += "<h2>Сформуємо матрицю Аfs*: </h2>";
            solveString += "<table border=\"1\">";
            decimal[,] Afs = SimplexMethod.formAfs(tables);
            for(int row = 0; row < rows; row++) {
                solveString += "<tr>";
                for(int col = 0; col < rows; col++) {
                    solveString += "<td> <b>" + Afs[row,col] + "</b> </td>";
                 }
                solveString += "</tr>";
            }
            solveString += "</table>";
            decimal determinant = SimplexMethod.determinant(Afs);
            solveString += "<h2>Визначник матриці буде дорівнювати " + determinant;
            if(determinant != 0) 
                solveString += " != 0 </h2> Отриманий план є опорним! <br>";
            else
                solveString += " == 0 </h2> Отриманий план не є опорним! <br>";
            solveString += "</body>\r\n</html>";
            using (StreamWriter sw = new StreamWriter("reference.html", false, Encoding.Default)) //false вказує, що файл буде перезаписано.
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
            catch (Exception)
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
            catch (Exception)
            {
                MessageBox.Show("Сталася помилка при завантаженні даних. Можливо, файл матриці записаний невірно.", "Помилка при завантаженні даних");
            }
        }

        /// <summary>
        /// Збереження результатів розрахунків у txt-файл
        /// </summary>
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
            catch (Exception)
            {
                MessageBox.Show("Сталася помилка при збереженні результатів у файл", "Помилка при збереженні");
            }
        }

        /// <summary>
        /// Запис значень цільової функції з кожної ітерації у масив
        /// </summary>
        public static double[] getTargetFunctionPoints(SimplexTable[] results)
        {
            int pointsLength = results.Length;
            double[] pointsArray = new double[pointsLength];
            for(int currentTable = 0; currentTable < pointsLength; currentTable++)
                pointsArray[currentTable] = Convert.ToDouble(results[currentTable].L);
            return pointsArray;
        }

        /// <summary>
        /// Клас для передавання точок до графіка
        /// </summary>
        public static class ListOfFunctionPoints
        {
            public static double[] Points { get; set; }
        }
        
        /// <summary>
        /// Забирає значення з TextBox кількості знаків після коми
        /// </summary>
        public static int getAnswerRoundValue(TextBox AnswerRoundBox)
        {
            try
            {
                int answerRoundValue = Convert.ToInt32((AnswerRoundBox.Text));
                if (answerRoundValue < 1 || answerRoundValue > 28)
                {
                    MessageBox.Show("Введіть значення від 1 до 28.\nЗначення скинуто до 8", "Помилка вибору значення заокруглення");
                    AnswerRoundBox.Text = "8";
                    return 8;
                }
                return answerRoundValue;
            }
            catch (Exception)
            {
                MessageBox.Show("Перевірте введені значення.\nЗначення скинуто до 8", "Помилка вибору значення заокруглення");
                AnswerRoundBox.Text = "8";
                return 8;
            }
        }

        /// <summary>
        /// Перевірка цільової функції і обмежень на пусті значення
        /// </summary>
        public static int checkingMatrix(DataGridView dataGridView1, DataGridView dataGridView2)
        {
            try
            {
                int colsNum = dataGridView1.ColumnCount;
                int rowsNum = dataGridView2.RowCount;
                for (int row = 0; row < colsNum; row++)
                {
                    if (dataGridView1.Rows[0].Cells[row].Value == null)
                    {
                        MessageBox.Show("Здається, ви забули вказати одне зі значень цільової функції.", "Помилка вводу!");
                        return -1;
                    }
                }
                for (int row = 0; row < rowsNum; row++)
                {
                    if (dataGridView2.Rows[row].Cells[colsNum].Value == null)
                    {
                        MessageBox.Show("Здається, ви забули вказати одне з обмежень.", "Помилка вводу!");
                        return -1;
                    }
                    if (Convert.ToDecimal(dataGridView2.Rows[row].Cells[colsNum].Value) < 0)
                    {
                        MessageBox.Show("Невірно вказане одне з обмежень, воно не може бути від'ємним", "Помилка вводу!");
                        return -1;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Невірно вказані обмеження", "Помилка вводу");
            }
            return 0;
        }
    }
}
