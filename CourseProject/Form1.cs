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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void insertValues_Click(object sender, EventArgs e) {
                 double[,] AFirst = new double[,] { {1300, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
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
                 double[] CFirst = new double[] { 315, 489, 663, 837, 1011, 1185, 1359, 1533, 1707, 1881, 2055, 2229, 2403};

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
        }

        /// <summary>
        /// Знаходження розв'зяку
        /// </summary>
        private void Solve_Click(object sender, EventArgs e)
        {
            try
            {
                SimplexTable[] results = SimplexMethod.solve(getLimitationMatrix(sender, e), getTargetFunction(sender, e));
                int last = results.Length - 1;
                AnswerBox.Text = IO.writeSolve(results[last]);

                IO.drowSolve(results, 0);
                IO.drowSolve(results, 1);
            }
            catch (Exception ex) { MessageBox.Show("Виникла помилка при розв'язанні задачі\r\n\r\nДеталі:\r\n" + ex, "Помилка"); }
        }

        /// <summary>
        /// Розмітка таблиць для введення даних після натискання кнопки
        /// </summary>
        private void buildMatrix_Click(object sender, EventArgs e)
        {
            //Кількість колонок і рядків
            try
            {
                int colsNum = Convert.ToInt16(numCols.Text);
                int rowsNum = Convert.ToInt16(numRows.Text);
                if (colsNum < 2 || rowsNum < 2)
                    throw new System.ArgumentException("Недійсні значення");
                IO.buildMatrix(sender, e, dataGridView1, dataGridView2, colsNum, rowsNum);
            }
            catch (Exception ex) { MessageBox.Show("Не вдалося побудувати матрицю, перевірте значення\r\n\r\nДеталі:\r\n" + ex, "Помилка"); }
        }

        /// <summary>
        /// Зміна поведінки знаків після зміни значення прапорця sameSign 
        /// (sameSign - В матриці обмежень однакові знаки нерівностей?)
        /// </summary>
        private void sameSign_Click(object sender, EventArgs e)
        {
            IO.sameSign_Check(sender, e, dataGridView2, sameSign);
        }

        /// <summary>
        /// Отримання вектора цільової функції (масив)
        /// </summary>
        private double[] getTargetFunction(object sender, EventArgs e)
        {
            return IO.getTargetFunction(sender, e, dataGridView1);
        }

        /// <summary>
        /// Отримання матриці обмежень (масив)
        /// </summary>
        private double[,] getLimitationMatrix(object sender, EventArgs e)
        {
            return IO.getLimitationMatrix(sender, e, dataGridView2);
        }

        /// <summary>
        /// Отримання значення куди прямує функція MIN/MAX
        /// </summary>
        private bool getMaxMin(object sender, EventArgs e)
        {
            return IO.getMaxMin(sender, e, dataGridView1);
        }
        

        //Елемент меню. Кнопка Вийти
        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
