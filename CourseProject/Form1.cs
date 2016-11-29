﻿using System;
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
            /*decimal[,] AFirst = new decimal[,] { {1300, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
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
            decimal[] CFirst = new decimal[] { 315, 489, 663, 837, 1011, 1185, 1359, 1533, 1707, 1881, 2055, 2229, 2403 };*/
            
            decimal[,] AFirst = new decimal[,] { {41000, 1, 19, 2, 18, 3, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 5, 15},
            {51000, 0, 2, 18, 1, 19, 3, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 16, 5, 15},
            {21000, 0, 0, 18, 2, 1, 19, 3, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 4, 15, 5},
            {41000, 0, 0, 0, 5, 15, 4, 16, 3, 17, 2, 18, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 19},
            {51008, 0, 0, 0, 0, 3, 17, 2, 18, 4, 16, 1, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 0, 5},
            {71000, 0, 0, 0, 0, 0, 4, 16, 3, 17, 2, 18, 1, 19, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 15},
            {81000, 0, 0, 0, 0, 0, 0, 1, 19, 2, 18, 3, 17, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 15, 5},
            {91008, 0, 0, 0, 0, 0, 0, 0, 15, 5, 2, 18, 3, 17, 0, 0, 0, 0, 0, 0, 0, 19, 0, 0, 1, 16, 4},
            {51004, 0, 0, 0, 0, 0, 0, 0, 0, 18, 2, 19, 1, 0, 0, 0, 0, 0, 0, 0, 0, 5, 15, 4, 16, 3, 17},
            {21005, 1, 19, 2, 18, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 17, 4, 16, 15, 5},
            {2260, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {33510, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26}};
            decimal[] CFirst = new decimal[] { 476, 798, 1120, 1442, 1764, 2086, 2408, 2730, 3052, 3374, 3696, 4018, 4340, 4662, 4984, 5306, 5628, 5950, 6272, 6594, 6916, 7238, 7560, 7882, 8204, 8526 };
            
            //-----Заповнення таблиці даними з масиву-----
            //Отримання розмірності
            int nCols = CFirst.GetLength(0);
            int nRows = AFirst.GetLength(0);
            //Будуємо таблиці
            IO.buildMatrix(sender, e, dataGridView1, dataGridView2, nCols, nRows);
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
            // Табуляграма неактивна поки не розв'язали задачу
            getDetailSolveButton.Enabled = false;

            //Очистимо поле відповіді
            AnswerBox.Clear();
        }

        /// <summary>
        /// Знаходження розв'зяку
        /// </summary>
        private void Solve_Click(object sender, EventArgs e)
        {
            try
            {
                getDetailSolveButton.Enabled = true;
                SimplexTable[] results = SimplexMethod.solve(getLimitationMatrix(sender, e), getTargetFunction(sender, e));
                int last = results.Length - 1;
                AnswerBox.Text = IO.writeSolve(results[last]);

                IO.drowSolve(results);
                IO.drowAdmissibility(results); 
                //Якщо 
                if (getDetailSolve.Checked)
                    getDetailSolve_Click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show("Виникла помилка при розв'язанні задачі. Перевірте введені значення", "Помилка при розв'язанні задачі"); }
            //для виведення системної помилки: catch (Exception ex) { MessageBox.Show("Виникла помилка при розв'язанні задачі. Перевірте введені значення\r\n\r\nДеталі:\r\n" + ex, "Помилка при розв'язанні задачі"); }
        }

        /// <summary>
        /// Розмітка таблиць для введення даних після натискання кнопки
        /// </summary>
        private void buildMatrix_Click(object sender, EventArgs e)
        {
            //Очистимо поле відповіді
            AnswerBox.Clear();

            //Кількість колонок і рядків
            try
            {
                int colsNum = Convert.ToInt16(numCols.Text);
                int rowsNum = Convert.ToInt16(numRows.Text);
                if (colsNum < 2 || rowsNum < 2)
                    throw new System.ArgumentException("Недійсні значення. Мінімальний розмір 2х2");
                IO.buildMatrix(sender, e, dataGridView1, dataGridView2, colsNum, rowsNum);
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
            IO.saveMatrix(sender, e, dataGridView1, dataGridView2, IO.getMaxMin(sender, e, MaxMinBox));
        }

        /// <summary>
        /// Виклик функції завантаження матриці з txt-файлу
        /// </summary>
        private void loadMatrix_Click(object sender, EventArgs e)
        {

        }
    }
}
