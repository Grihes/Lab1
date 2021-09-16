using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static double Function(double k, double tOut, double t, double h)
        {
            return t - k * (t - tOut) * h;
        }
        public static double FindTemp(double k, double tOut, double t, double h, double setTime)
        {
            double time = 0;
            double tForDrink = t;
            while (time < setTime)
            {
                tForDrink = Function(k, tOut, tForDrink, h);
                time += h;
            }
            return tForDrink;
        }
        public static double FindK(double tOut, double t, double h, double[] experimentResults)
        {
            double diff = 0;
            double difference = Int32.MaxValue;
            double k = 0;
            for (double j = 0; j < 1; j += 0.001)
            {
                for (int i = 0; i < experimentResults.Length; i++)
                    diff += Math.Pow(experimentResults[i] - FindTemp(j, tOut, t, h, i), 2);
                if (diff <= difference)
                {
                    difference = diff;
                    k = j;
                }
                diff = 0;
            }
            return k;
        }
        public static double[] GetTheorRes(double k, double tOut, double t, double h, double[] experimentResults)
        {
            double tForDrink = t;
            double[] theoreticTemp = new double[16];
            for (int i = 0; i < theoreticTemp.Length; i++)
                theoreticTemp[i] = FindTemp(k, tOut, t, h, i);
            return theoreticTemp;
        }
        public static double GetTimeCooling (double k, double tOut, double t, double h, double tForDrink)
        {
            double time = 0;
            while (t > tForDrink)
            {
                t= Function(k, tOut,t, h);
                time += h;
            }
            return time;
        }
        public static double Findh(double lBoard, double rBoard, double k, double tOut, double t, double time, double realTemp)
        {
           while (rBoard > lBoard)
            {
                var midValue = Math.Round((rBoard + lBoard)/2, 4);
                var Temperature = FindTemp(k, tOut, t, midValue, time);
                if ( Math.Round((Math.Abs(realTemp- Temperature) / realTemp), 4)>=0.001)
                    rBoard = midValue;
                else
                    lBoard = midValue + 0.0001;
            }
            return rBoard;
        }
    }
}
