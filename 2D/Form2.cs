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
    public partial class Form2 : Form
    {
        private readonly Random random = new();
        private readonly Figure figure = new();
        private Figure currentFigure;
        private readonly ILineDrawer brezenhamLineDrawer;
        private readonly ILineDrawer cuttingLineDrawer;
        private readonly Color figureColor = Color.Black;

        public Form2()
        {
            InitializeComponent();

            using (var graphics = pictureBox.CreateGraphics())
            {
                var rast = new Rasterization2D(graphics);
                Rect = new(
                    rast.ToPixelCoords(100),
                    rast.ToPixelCoords(100),
                    rast.ToPixelCoords(600),
                    rast.ToPixelCoords(400)
                );
            }
                
            brezenhamLineDrawer = new BrezenhamLineDrawer();
            cuttingLineDrawer = new CuttingLineDrawer(brezenhamLineDrawer, Rect, Color.Gray);
            currentFigure = figure;

            pictureBox.Paint += DrawRectangle;
            pictureBox.Paint += Draw;
        }

        public Rectangle Rect { get; }

        public void DrawRectangle(object? sender, PaintEventArgs e)
        {
            var rast = new Rasterization2D(e.Graphics);

            var leftUpper = new Vector2(Rect.Left, Rect.Top);
            var rightUpper = new Vector2(Rect.Right, Rect.Top);
            var leftDown = new Vector2(Rect.Left, Rect.Bottom);
            var rightDown = new Vector2(Rect.Right, Rect.Bottom);

            brezenhamLineDrawer.DrawLine(leftUpper, rightUpper, rast, Color.Red);
            brezenhamLineDrawer.DrawLine(rightUpper, rightDown, rast, Color.Red);
            brezenhamLineDrawer.DrawLine(leftDown, rightDown, rast, Color.Red);
            brezenhamLineDrawer.DrawLine(leftUpper, leftDown, rast, Color.Red);
        }

        public void Draw(object? sender, PaintEventArgs e)
        {
            var rast = new Rasterization2D(e.Graphics);
            rast.DrawGrid();
        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {
            pictureBox.Invalidate();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            var me = (MouseEventArgs)e;
            var x = me.X;
            var y = me.Y;

            using (var graphics = pictureBox.CreateGraphics())
            {
                var rast = new Rasterization2D(graphics);
                currentFigure.DrawFigure(rast, cuttingLineDrawer, new(x, y), figureColor);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
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
