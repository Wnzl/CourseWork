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
            SimplexMethod.solve(AFirst, CFirst);
        }
            

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
