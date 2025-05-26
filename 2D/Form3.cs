using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace ComputerGraphics
{
    public partial class Form3 : Form
    {
        private readonly ITriangleFiller triangleFiller;
        private readonly Figure figure = new();
        private Figure currentFigure;
        private readonly Vector2 leftUpperCorner = new(100, 100);

        public Form3()
        {
            InitializeComponent();

            triangleFiller = new TriangleFiller(false);
            currentFigure = figure;

            pictureBox.Paint += DrawFigure;
        }

        private void DrawFigure(object sender, PaintEventArgs e)
        {
            var rast = new Rasterization2D(e.Graphics);
            currentFigure.FillFigure(rast, triangleFiller, leftUpperCorner);
            rast.DrawGrid();
        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {
            pictureBox.Invalidate();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            var newSize = 1 + trackBar1.Value / 10f;
            currentFigure = figure.ResizeFigure(newSize);
            pictureBox.Invalidate();
        }
    }
}
