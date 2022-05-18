using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MyUfo
{
    public partial class Form2 : Form
    {
        List<double> listpoint;
        public Form2(List<double> l)
        {
            InitializeComponent();
            listpoint = l;

            Series series2 = new Series();
            chart1.Series.Add(series2);

            Axis ay = new Axis();
            ay.Title = "Погрешность";
            chart1.ChartAreas[0].AxisY = ay;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 100;
            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 5;

            Axis ax = new Axis();
            ax.Title = "Количество членов ряда";
            chart1.ChartAreas[0].AxisX = ax;
            chart1.ChartAreas[0].AxisX.Minimum = 2;
            chart1.ChartAreas[0].AxisX.Maximum = 10;
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 1;

            chart1.Series[0].Color = Color.MidnightBlue;
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[1].Color = Color.Red;
            chart1.Series[1].ChartType = SeriesChartType.Point;

            for (int i = 0; i < listpoint.Count; i++)
            {
                chart1.Series[0].Points.AddXY(i + 2, Math.Round(listpoint[i], 2));
                chart1.Series[1].Points.AddXY(i + 2, Math.Round(listpoint[i], 2));
            }
        }
    }
}
