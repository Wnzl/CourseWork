using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace CourseProject
{
    public partial class TargetFunctionGraph : Form
    {
        public TargetFunctionGraph()
        {
            InitializeComponent();
            // get a reference to the GraphPane
            GraphPane myPane = zedGraphControl1.GraphPane;

            myPane.CurveList.Clear();
            // Set the Titles
            myPane.Title.Text = "Графік змін цільової функції";
            myPane.XAxis.Title.Text = "Ітерації";
            myPane.YAxis.Title.Text = "Значення цільової функції";

            // Make up some data arrays based on the Sine function
            PointPairList targetFunctionPointsList = new PointPairList();

            decimal [] targetFunctionPoints = IO.ListOfFunctionPoints.Points;
            int numberOfPoints = IO.ListOfFunctionPoints.Points.Length;

            for (int pointNumber = 0; pointNumber < numberOfPoints; pointNumber++)
            {
                double x = Convert.ToDouble(pointNumber);
                double y = Convert.ToDouble(targetFunctionPoints[pointNumber]);
                targetFunctionPointsList.Add(x, y);
            }

            // Generate a red curve with diamond
            // symbols, and "Porsche" in the legend
            LineItem myCurve = myPane.AddCurve("", targetFunctionPointsList, Color.Red, SymbolType.None);

            //LineItem myCurve3 = myPane.AddCurve("", list3, Color.FromArgb(255, 255, 255, 0));
            //myCurve3.Line.Fill = new Fill(Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 255, 255, 0), 270F);

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
    }
}
