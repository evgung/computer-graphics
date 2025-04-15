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
        private readonly IRasterization rast = new Rasterization();
        private readonly ILineDrawer brezenhamLineDrawer;
        private readonly ILineDrawer cuttingLineDrawer;
        private readonly Color figureColor = Color.Black;

        public Form2()
        {
            InitializeComponent();

            Rect = new(
                rast.ToPixelCoords(100),
                rast.ToPixelCoords(100),
                rast.ToPixelCoords(600),
                rast.ToPixelCoords(400)
            );

            brezenhamLineDrawer = new BrezenhamLineDrawer(rast);
            cuttingLineDrawer = new CuttingLineDrawer(rast, brezenhamLineDrawer, Rect, Color.Gray);

            pictureBox.Paint += DrawRectangle;
            pictureBox.Paint += Draw;
        }

        public Rectangle Rect { get; }

        public void DrawRectangle(object? sender, PaintEventArgs e)
        {
            var graph = e.Graphics;

            var leftUpper = new Vector2(Rect.Left, Rect.Top);
            var rightUpper = new Vector2(Rect.Right, Rect.Top);
            var leftDown = new Vector2(Rect.Left, Rect.Bottom);
            var rightDown = new Vector2(Rect.Right, Rect.Bottom);

            brezenhamLineDrawer.DrawLine(leftUpper, rightUpper, graph, Color.Red);
            brezenhamLineDrawer.DrawLine(rightUpper, rightDown, graph, Color.Red);
            brezenhamLineDrawer.DrawLine(leftDown, rightDown, graph, Color.Red);
            brezenhamLineDrawer.DrawLine(leftUpper, leftDown, graph, Color.Red);
        }

        public void Draw(object? sender, PaintEventArgs e)
        {
            rast.DrawGrid(e.Graphics);
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
                figure.DrawFigure(graphics, cuttingLineDrawer, new(x, y), figureColor);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            pictureBox.Invalidate();
        }
    }
}
