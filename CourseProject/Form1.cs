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
             
            SimplexMethod.solve(AFirst, CFirst);
        }
            

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
