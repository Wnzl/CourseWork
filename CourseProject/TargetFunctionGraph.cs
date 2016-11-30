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
            zedGraphControl1.IsShowHScrollBar = true;
            zedGraphControl1.IsShowVScrollBar = true;
            //zedGraphControl1.IsAutoScrollRange = true;
            myPane.CurveList.Clear();
            // Set the Titles
            myPane.Title.Text = "Графік змін цільової функції";
            myPane.XAxis.Title.Text = "Ітерації";
            myPane.YAxis.Title.Text = "Значення цільової функції";
            
            PointPairList targetFunctionPointsList = new PointPairList();

            double [] targetFunctionPoints = IO.ListOfFunctionPoints.Points;
            int numberOfPoints = IO.ListOfFunctionPoints.Points.Length;

            for (int pointNumber = 0; pointNumber < numberOfPoints; pointNumber++)
            {
                double x = Convert.ToDouble(pointNumber);
                targetFunctionPointsList.Add(x, targetFunctionPoints[pointNumber]);
            }

            // Generate a red curve
            LineItem myCurve = myPane.AddCurve("", targetFunctionPointsList, Color.Red, SymbolType.None);

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
    }
}
