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
                    double[,] A = new double[,] { {15, 3, 6, 9, 3},
                                                  {5,-3,2,3,2 } };
                                                    
            double[,] fs = new double[,] { { 3, 3 },
                                            { -3,2} };
            double[,] X = SimplexMethod.newTableCoeffsCount(A,fs);

            int x = X.GetLength(0);
            int y = X.GetLength(1);
            for (int i = 0; i < x; i++) {
                out1.Text += ("\n\n");
                for (int j = 0; j < y; j++) {
                    out1.Text += X[i, j] + "   ";
                }
            }
        }
            

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
