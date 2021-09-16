using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            double tOut = 22;
            double t = 83;
            double h = 0.01;
            var experimentResults = new double[] { 83, 77.7, 75.1, 73.0, 71.1, 69.4, 67.8, 66.4, 64.7, 63.4, 62.1, 61.0, 59.9, 58.7, 57.8, 56.6 };
            double k = Program.FindK(tOut, t, h, experimentResults);
            BuildChart(k, tOut, t, h, experimentResults);
            BuildTable13(k, tOut, t, h);
            BuildTable15(k, tOut, t, h, experimentResults);
            BuildTable16(k, tOut, t, experimentResults);
            BuildChart2(k,tOut, t, h, experimentResults);
            BuildTable17(k, tOut, t);

        }
        private void BuildChart(double k , double tOut, double t, double h, double[] experimentResults)
        {
            var chartArea = chart1.ChartAreas[0];
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chartArea.AxisY.Minimum = 45;
            chartArea.AxisY.Maximum = 100;
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = 16;
            chartArea.AxisY.Interval = 3;
            var theoreticalResults = Program.GetTheorRes(k, tOut, t, h, experimentResults);
            for (int i = 0; i < experimentResults.Length; i++)
            {
                chart1.Series[0].Points.AddXY(i, experimentResults[i]);
                chart1.Series[1].Points.AddXY(i, theoreticalResults[i]);
            }
            chartArea.AxisY.Title = "t, °C";
            chartArea.AxisX.Title = "τ, min";
        }
        private void BuildTable13 (double k , double tOut, double t, double h) 
            {
                DataTable table = new DataTable();
                dataGridView1.AllowUserToAddRows = false;
                table.Columns.Add("Difference, °C", typeof(double));
                table.Columns.Add("Cooling time, min", typeof(double));
                for (double i=2; i<=8; i*=2)
                {
                    table.Rows.Add((61.0 / i), Math.Round(Program.GetTimeCooling(k, tOut, t, h, 61.0 / i + tOut), 3));
                }
                dataGridView1.DataSource = table;
            }
        private void BuildTable15(double k , double tOut, double t, double h, double[] experimentResults)
        {
            DataTable table = new DataTable();
            dataGridView2.AllowUserToAddRows = false;
            table.Columns.Add("Time when milk was added at the begginig, min", typeof(double));
            table.Columns.Add("Time when milk was added at the end, min", typeof(double));
            table.Rows.Add(Math.Round(Program.GetTimeCooling(k, tOut, 85, h, 75), 3), Math.Round(Program.GetTimeCooling(k, tOut, 90, h, 80), 3));
            dataGridView2.DataSource = table;
        }
        private void BuildTable16(double k, double tOut, double t, double[] experimentResults)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Δt, min", typeof(double));
            table.Columns.Add("Theoretical results, °C", typeof(double));
            table.Columns.Add("Experimental results, °C", typeof(double));
            table.Columns.Add("Difference, °C", typeof(double));
            table.Rows.Add(0.1, Program.FindTemp(k, tOut, t, 0.1, 1), 80.5, Math.Abs(Program.FindTemp(k, tOut, t, 0.1, 1) - 80.5));
            table.Rows.Add(0.05, Program.FindTemp(k, tOut, t, 0.05, 1), 80.5, Math.Abs(Program.FindTemp(k, tOut, t, 0.05, 1) - 80.5));
            table.Rows.Add(0.025, Program.FindTemp(k, tOut, t, 0.025, 1), 80.5, Math.Abs(Program.FindTemp(k, tOut, t, 0.025, 1) - 80.5));
            table.Rows.Add(0.01, Program.FindTemp(k, tOut, t, 0.01, 1), 80.5, Math.Abs(Program.FindTemp(k, tOut, t, 0.01, 1) - 80.5));
            table.Rows.Add(0.005, Program.FindTemp(k, tOut, t, 0.005, 1), 80.5, Math.Abs(Program.FindTemp(k, tOut, t, 0.005, 1) - 80.5));
            dataGridView3.DataSource = table;
        }
        private void BuildChart2(double k , double tOut, double t, double h, double[] experimentResults)
        {
            var chartArea = chart2.ChartAreas[0];
            chart2.Series[0].Points.Clear();;
            chartArea.AxisY.Interval = 0.025;
            chartArea.AxisX.Interval = 0.01;
            chart2.Series[0].Points.AddXY(0.1, Math.Abs(Program.FindTemp(k, tOut, t, 0.1, 1) - 80.5));
            chart2.Series[0].Points.AddXY(0.05, Math.Abs(Program.FindTemp(k, tOut, t, 0.05, 1) - 80.5));
            chart2.Series[0].Points.AddXY(0.025, Math.Abs(Program.FindTemp(k, tOut, t, 0.025, 1) - 80.5));
            chart2.Series[0].Points.AddXY(0.01, Math.Abs(Program.FindTemp(k, tOut, t, 0.01, 1) - 80.5));
            chart2.Series[0].Points.AddXY(0.005, Math.Abs(Program.FindTemp(k, tOut, t, 0.005, 1) - 80.5));
            chartArea.AxisY.Title = "Difference, °C";
            chartArea.AxisX.Title = "Δt, min";
        }
        private void BuildTable17( double k, double tOut, double t)
        {
            DataTable table = new DataTable();
            dataGridView4.AllowUserToAddRows = false;
            table.Columns.Add("t, min", typeof(double));
            table.Columns.Add("step, min", typeof(double));
            table.Rows.Add(1, Program.Findh(0, 0.5, k, tOut, t,1, 80.5));
            table.Rows.Add(5, Program.Findh(0, 0.5, k, tOut, t, 5, 71.4));
            dataGridView4.DataSource = table;
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }
    }
}
