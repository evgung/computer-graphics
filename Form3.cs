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
        private readonly IRasterization rast = new Rasterization();
        private readonly ITriangleFiller triangleFiller;
        private readonly Figure figure = new();
        private readonly Vector2 leftUpperCorner = new(100, 100);

        public Form3()
        {
            InitializeComponent();

            triangleFiller = new TriangleFiller(rast);

            pictureBox.Paint += DrawFigure;
        }

        private void DrawFigure(object sender, PaintEventArgs e)
        {
            figure.FillFigure(e.Graphics, triangleFiller, leftUpperCorner);
            rast.DrawGrid(e.Graphics);
        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {
            pictureBox.Invalidate();
        }
    }
}
