using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSP_Visual
{


    public partial class Form1 : Form
    {
        public PointF[] pointsToDraw;
        public double currentCost = 0;

        float scale = 10f;
        PointF offset = new PointF(100, 100); // כדי לרכז את המסלול בתוך הטופס

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;
            this.Paint += DrawRoute;
            this.Width = 900;
            this.Height = 700;
        }

        private void DrawRoute(object sender, PaintEventArgs e)
        {
            if (pointsToDraw == null || pointsToDraw.Length < 2)
                return;

            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Cyan, 2);
            Brush cityBrush = Brushes.Red;
            Brush textBrush = Brushes.White;
            Font font = new Font("Consolas", 9);
            Font bigFont = new Font("Consolas", 14, FontStyle.Bold);

            // קווים
            for (int i = 0; i < pointsToDraw.Length - 1; i++)
            {
                g.DrawLine(pen,
                    pointsToDraw[i].X * scale + offset.X, pointsToDraw[i].Y * scale + offset.Y,
                    pointsToDraw[i + 1].X * scale + offset.X, pointsToDraw[i + 1].Y * scale + offset.Y);
            }

            g.DrawLine(pen,
                pointsToDraw[pointsToDraw.Length - 1].X * scale + offset.X, pointsToDraw[pointsToDraw.Length - 1].Y * scale + offset.Y,
                pointsToDraw[0].X * scale + offset.X, pointsToDraw[0].Y * scale + offset.Y);

            // נקודות + קואורדינטות
            for (int i = 0; i < pointsToDraw.Length; i++)
            {
                float x = pointsToDraw[i].X * scale + offset.X;
                float y = pointsToDraw[i].Y * scale + offset.Y;

                g.FillEllipse(cityBrush, x - 4, y - 4, 8, 8);
                g.DrawString($"({pointsToDraw[i].X},{pointsToDraw[i].Y})", font, textBrush, x + 6, y - 6);
            }

            // הצגת עלות המסלול
            g.DrawString($"Cost: {currentCost:F2}", bigFont, Brushes.Yellow, 20, 20);
        }

        public void UpdateRoute(PointF[] newRoute, double cost)
        {
            pointsToDraw = (PointF[])newRoute.Clone();
            currentCost = cost;
            Invalidate();
        }
    }


}
