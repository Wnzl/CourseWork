using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void insertValues_Click(object sender, EventArgs e) {
            resetButtons(sender, e);
            decimal[,] AFirst = new decimal[,] { {1300, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                                                      {9100, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13},
                                                      {12400, 11, 5, 9, 21, 1, 19, 1, 31, 1, 0, 0, 0, 29},
                                                      {11240, 0, 7, 11, 9, 21, 1, 19, 31, 11, 29, 0, 0, 3},
                                                      {12402, 0, 0, 7, 21, 11, 9, 1, 19, 1, 31, 29, 0, 4},
                                                      {41240, 0, 0, 0, 7, 9, 11, 21, 19, 1, 29, 31, 6, 5},
                                                      {12405, 7, 0, 19, 11, 9, 31, 29, 21, 11, 6, 0, 0, 6},
                                                      {21240, 9, 0, 0, 11, 29, 31, 19, 21, 1, 11, 6, 0, 7},
                                                      {12407, 11, 0, 0, 0, 5, 9, 19, 31, 21, 29, 11, 6, 8},
                                                      {51240, 0, 31, 9, 11, 1, 21, 19, 29, 11, 70, 0, 0, 9},
                                                      {12408, 7, 0, 0, 5, 1, 9, 11, 1, 19, 21, 29, 0, 31},
                                                      {12410, 11, 0, 0, 0, 9, 1, 19, 1, 21, 1, 29, 31, 10},
                   };
            decimal[] CFirst = new decimal[] { 315, 489, 663, 837, 1011, 1185, 1359, 1533, 1707, 1881, 2055, 2229, 2403 };
            
            //-----Заповнення таблиці даними з масиву-----
            //Отримання розмірності
            int nCols = CFirst.GetLength(0);
            int nRows = AFirst.GetLength(0);
            //Будуємо таблиці
            IO.buildMatrix(dataGridView1, dataGridView2, nCols, nRows);
            //Заповнюємо цільову функцію
            for (int i = 0; i < nCols; i++)
                dataGridView1.Rows[0].Cells[i].Value = CFirst[i].ToString();

            //Заповнюємо матрицю обмежень
            //collsAdded враховує додані колонки зі знаком і b, потрібна для знахоження індексу елементів b в таблиці
            int collsAdded = dataGridView2.ColumnCount;
            for (int i = 0; i < nRows; i++)
            {
                //Задаємо b
                dataGridView2.Rows[i].Cells[collsAdded - 1].Value = AFirst[i, 0].ToString();
                for (int j = 0, matrJ = 1; j < nCols; j++, matrJ++)
                    //Задаємо A
                    dataGridView2.Rows[i].Cells[j].Value = AFirst[i, matrJ].ToString();
            }
        }

        /// <summary>
        /// Знаходження розв'зяку
        /// </summary>
        private void Solve_Click(object sender, EventArgs e)
        {
            enableButtons(sender, e);
            try
            {
                label3.Text = "Loading";
                getDetailSolveButton.Enabled = true;
                SimplexTable[] results = SimplexMethod.solve(getLimitationMatrix(sender, e), getTargetFunction(sender, e));
                int last = results.Length - 1;
                AnswerBox.Text = IO.writeSolve(results[last]);

                IO.drowSolve(results);
                IO.drowAdmissibility(results);
                IO.ListOfFunctionPoints.Points = IO.getTargetFunctionPoints(results);

                //Якщо 
                if (getDetailSolve.Checked)
                {
                    getDetailSolve_Click(sender, e);
                }
                label3.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Виникла помилка при розв'язанні задачі. Перевірте введені значення", "Помилка при розв'язанні задачі");
                resetButtons(sender, e);
            }
            //для виведення системної помилки: catch (Exception ex) { MessageBox.Show("Виникла помилка при розв'язанні задачі. Перевірте введені значення\r\n\r\nДеталі:\r\n" + ex, "Помилка при розв'язанні задачі"); }
        }

        /// <summary>
        /// Розмітка таблиць для введення даних після натискання кнопки
        /// </summary>
        private void buildMatrix_Click(object sender, EventArgs e)
        {
            resetButtons(sender, e);
            //Кількість колонок і рядків
            try
            {
                int colsNum = Convert.ToInt16(numCols.Text);
                int rowsNum = Convert.ToInt16(numRows.Text);
                if (colsNum < 2 || rowsNum < 2)
                    throw new System.ArgumentException("Недійсні значення. Мінімальний розмір 2х2");
                IO.buildMatrix(dataGridView1, dataGridView2, colsNum, rowsNum);
                // Табуляграма неактивна поки не розв'язали задачу
                getDetailSolveButton.Enabled = false;
            }
            catch (Exception ex) { MessageBox.Show("Не вдалося побудувати матрицю, перевірте введені значення\r\n\r\nДеталі:\r\n" + ex, "Помилка при побудові матриці"); }
            //для виведення системної помилки: catch (Exception ex) { MessageBox.Show("Не вдалося побудувати матрицю, перевірте введені значення\r\n\r\nДеталі:\r\n" + ex, "Помилка при побудові матриці"); }
        }
        /// <summary>
        /// Отримання вектора цільової функції (масив)
        /// </summary>
        private decimal[] getTargetFunction(object sender, EventArgs e)
        {
            return IO.getTargetFunction(sender, e, dataGridView1);
        }

        /// <summary>
        /// Отримання матриці обмежень (масив)
        /// </summary>
        private decimal[,] getLimitationMatrix(object sender, EventArgs e)
        {
            return IO.getLimitationMatrix(sender, e, dataGridView2);
        }

        /// <summary>
        /// Отримання значення куди прямує функція MIN/MAX
        /// </summary>
        private bool getMaxMin(object sender, EventArgs e)
        {
            return IO.getMaxMin(sender, e, MaxMinBox);
        }

        /// <summary>
        /// Вихід з програми
        /// </summary>
        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Виведеня вікна з детальним розв'язком
        /// </summary>
        private void getDetailSolve_Click(object sender, EventArgs e)
        {
            detailSolve form = new detailSolve();
            form.Show();
        }

        /// <summary>
        /// Виведеня вікна "Про програму"
        /// </summary>
        private void проПрограмуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About form = new About();
            form.Show();
        }

        /// <summary>
        /// Виклик вікна з перевіркою допустимості
        /// </summary>
        private void admissibilityCheck_Click(object sender, EventArgs e)
        {
            admissibilityCheck form = new admissibilityCheck();
            form.Show();
        }

        /// <summary>
        /// Виклик функції збереження матриці у txt-файл
        /// </summary>
        private void saveMatrix_Click(object sender, EventArgs e)
        {
            IO.saveMatrix(dataGridView1, dataGridView2, IO.getMaxMin(sender, e, MaxMinBox));
        }

        /// <summary>
        /// Виклик функції завантаження матриці з txt-файлу
        /// </summary>
        private void loadMatrix_Click(object sender, EventArgs e)
        {
            resetButtons(sender, e);
            IO.loadMatrix(dataGridView1, dataGridView2, MaxMinBox);
        }
        
        /// <summary>
        /// Кнопки неактивні до розв'язку задачі, очищення поля відповіді
        /// </summary>
        private void resetButtons(object sender, EventArgs e)
        {
            //Табуляграма неактивна поки не розв'язали задачу
            getDetailSolveButton.Enabled = false;
            //Перевірка і збереження відповіді теж
            checkingButton.Enabled = false;
            saveResult.Enabled = false;
            //Очистимо поле відповіді
            AnswerBox.Clear();
        }

        /// <summary>
        /// Кнопки активні після розв'язку задачі
        /// </summary>
        private void enableButtons(object sender, EventArgs e)
        {
            //Табуляграма активна після розв'язку задачі
            getDetailSolveButton.Enabled = true;
            //Перевірка і збереження відповіді теж
            checkingButton.Enabled = true;
            saveResult.Enabled = true;
        }
        /// <summary>
        /// Виклик функції збереження відповіді в txt-файл
        /// </summary>
        private void saveResult_Click(object sender, EventArgs e)
        {
            IO.saveResult(AnswerBox);
        }

        /// <summary>
        /// Виведення довідки
        /// </summary>
        private void HelpButton_Click(object sender, EventArgs e)
        {
            Help form = new Help();
            form.Show();
        }

        /// <summary>
        /// Виведення графіка змін цільової функції по ітераціям
        /// </summary>
        private void TargetFunctionGraphButton_Click(object sender, EventArgs e)
        {
            TargetFunctionGraph form = new TargetFunctionGraph();
            form.Show();
        }
    }
}
