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
                    double[,] A = new double[,] { {15, 3, 6, 9, 3},
                                                  {5,-3,2,3,2 } }; //матрица ограничений

            int[] fs = { 1, 4 }; //коэффициенты базиса
            double[] C = new double[] { -2, 2, 9, 1 }; //коэффициенты целевой функции
            SimplexMethod.solve(A,C, fs);
        }
            

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
