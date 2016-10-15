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

        private void button1_Click(object sender, EventArgs e) {
            //тут для примера вводим всякие значения
            double[,] AFirst = new double[,] { {15, 3, 6, 9, 3},
                                                  {5,-3,2,3,2 } }; //матрица ограничений из формы ввода
            double[] CFirst = new double[] { -2, 2, 9, 1 }; //коэффициенты целевой функции из формы ввода
            //нужно в одельный метод закинуть, пока для пробы
            //матрица ограничений с учётом добавленных переменных
            double[,] A = new double[AFirst.GetLength(0), AFirst.GetLength(0) + AFirst.GetLength(1)];
            for (int i=0; i < AFirst.GetLength(0); i++)
            {
                for (int j = 0; j < AFirst.GetLength(0) + AFirst.GetLength(1); j++)
                {
                    if (j < AFirst.GetLength(1)) A[i, j] = AFirst[i, j];
                    else
                    {
                        if (j == i) A[i, j] = 1;
                        else A[i, j] = 0;
                    }
                }
            }
            //коэффициенты целевой функции с учетом добавленных переменных
            double[] C = new double[AFirst.GetLength(0) + AFirst.GetLength(1) - 1];
            for (int i = 0; i < AFirst.GetLength(0) + AFirst.GetLength(1)-1; i++)
            {
                if (i < AFirst.GetLength(1)-1) C[i] = CFirst[i];
                else C[i] = 0;     
            }
            int[] fs = new int [AFirst.GetLength(0)]; //коэффициенты базиса
            for (int i = 0; i < AFirst.GetLength(0); i++)
            {
                fs[i] = AFirst.GetLength(0) + i + 1;
            }
           
            SimplexMethod.solve(A, C, fs);
        }
            

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
