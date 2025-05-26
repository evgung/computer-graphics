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
    public class Rasterization2D : IRasterization
    {
        public int PixelSize { get; } = 10;
        public Graphics Graphics { get; }

        public Rasterization2D(Graphics graphics)
        {
            Graphics = graphics;
        }

        public void DrawGrid()
        {
            var width = Graphics.ClipBounds.Width;
            var height = Graphics.ClipBounds.Height;

            for (var x = 0; x < width; x += PixelSize)
            {
                Graphics.DrawLine(Pens.Black, x, 0, x, height);
            }

            for (var y = 0; y < height; y += PixelSize)
            {
                Graphics.DrawLine(Pens.Black, 0, y, width, y);
            }
        }

        public int ToPixelCoords(float coord)
        {
            return (int)coord / PixelSize * PixelSize;
        }

        public void FillPixel(ColoredPoint pixel)
        {
            var brush = new SolidBrush(pixel.Color);
            var coords = ((IRasterization)this).ToPixelCoords(new Vector2(pixel.Point.X, pixel.Point.Y));
            Graphics.FillRectangle(brush, coords.X, coords.Y, PixelSize, PixelSize);
        }
    }
}
