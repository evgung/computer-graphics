using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerGraphics
{
    public partial class Form1 : Form
    {
        private readonly Random random = new();
        private readonly Figure figure = new();
        private readonly IRasterization rast = new Rasterization();
        private readonly ILineDrawer cdaLineDrawer;
        private readonly ILineDrawer brezenhamLineDrawer;
        private readonly Vector2 brezenhamBegin;
        private readonly Vector2 indent = new(100, 20);
        private readonly Color figureColor = Color.Gray;

        public Form1()
        {
            InitializeComponent();

            brezenhamBegin = new(pictureBox.Width / 2, 0);
            cdaLineDrawer = new CdaLineDrawer(rast);
            brezenhamLineDrawer = new BrezenhamLineDrawer(rast);

            pictureBox.Paint += DrawBorders;
            pictureBox.Paint += Draw;
        }

        public void DrawBorders(object? sender, PaintEventArgs e)
        {
            var graph = e.Graphics;
            var width = pictureBox.Width;
            var height = pictureBox.Height;

            var pen = new Pen(Color.Red, 3);
            graph.DrawLine(pen, 0, height / 2, width, height / 2);
            graph.DrawLine(pen, width / 2, 0, width / 2, height / 2);
        }

        public void Draw(object? sender, PaintEventArgs e)
        {
            rast.DrawGrid(e.Graphics);
            figure.DrawFigure(e.Graphics, cdaLineDrawer, indent, figureColor);
            figure.DrawFigure(e.Graphics, brezenhamLineDrawer, indent + brezenhamBegin, figureColor);
        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {
            pictureBox.Invalidate();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            var from = new Vector2(
                random.Next(0, pictureBox.Width),
                random.Next(pictureBox.Height / 2, pictureBox.Height)
            );
            var to = new Vector2(
                random.Next(0, pictureBox.Width),
                random.Next(pictureBox.Height / 2, pictureBox.Height)
            );

            using (var graphics = pictureBox.CreateGraphics())
            {
                brezenhamLineDrawer.DrawLine(from, to, graphics, Color.Gray);
            }
        }
    }
}
