using System.Drawing;
using System;
using System.Threading;
using System.Windows.Forms;
using TSP_Visual;

class Program
{
    static double GetDistance(PointF a, PointF b)
    {
        double dx = a.X - b.X;
        double dy = a.Y - b.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    static double get_cost(PointF[] points)
    {
        double cost = 0;
        for (int i = 1; i < points.Length; i++)
        {
            cost += GetDistance(points[i], points[i - 1]);
        }
        cost += GetDistance(points[points.Length - 1], points[0]);
        return cost;
    }

    static PointF[] Swap(PointF[] points, Random rand)
    {
        PointF[] new_points = (PointF[])points.Clone();
        int a = rand.Next(1, points.Length);
        int b = rand.Next(1, points.Length);
        while (a == b)
        {
            b = rand.Next(1, points.Length);
        }
        PointF temp = new_points[a];
        new_points[a] = new_points[b];
        new_points[b] = temp;
        return new_points;
    }

    static bool swapProb(double temp, double dCost, Random rand)
    {
        double prob = Math.Exp(-dCost / temp);
        return rand.NextDouble() < prob;
    }

    static void RunAlgorithm(Form1 form)
    {
        PointF[] curr_points = new PointF[14];
        curr_points[0] = new PointF(4, 0);
        curr_points[1] = new PointF(0,10);
        curr_points[2] = new PointF(20, 12);
        curr_points[3] = new PointF(4,42);
        curr_points[4] = new PointF(59, 3);
        curr_points[5] = new PointF(57, 42);
        curr_points[6] = new PointF(16, 9);
        curr_points[7] = new PointF(9, 16);
        curr_points[8] = new PointF(15,22);
        curr_points[9] = new PointF(6,36);
        curr_points[10] = new PointF(42, 9);
        curr_points[11] = new PointF(8, 32);
        curr_points[12] = new PointF(29, 11);
        curr_points[13] = new PointF(2, 22);
        PointF[] new_points = new PointF[14];
        Random rand = new Random();

        double temp = 1000;
        double curr_cost = 0;
        double new_cost;
        double delta;

        while (temp > 0.001)
        {
            curr_cost = get_cost(curr_points);
            new_points = Swap(curr_points, rand);
            new_cost = get_cost(new_points);
            delta = new_cost - curr_cost;

            if (delta < 0 || swapProb(temp, delta, rand))
            {
                curr_points = new_points;
                curr_cost = new_cost;

                form.Invoke((MethodInvoker)(() =>
                {
                    form.UpdateRoute(curr_points, curr_cost);
                }));

                Thread.Sleep(00); 
            }

            temp *= 0.999;
        }

        MessageBox.Show("Final cost: " + curr_cost.ToString("F2"));
    }

    [STAThread]
    static void Main(string[] args)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        Form1 form = new Form1();

        form.Load += (s, e) =>
        {
            Thread algoThread = new Thread(() => RunAlgorithm(form));
            algoThread.Start();
        };

        Application.Run(form);
    }
}
