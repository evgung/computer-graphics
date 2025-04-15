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
    public interface IRasterization
    {
        public int PixelSize { get; }
        public void DrawGrid(Graphics graphics);
        public Vector2 ToPixelCoords(Vector2 coords);
        public int ToPixelCoords(float coord);
        public void FillPixel(float x, float y, Graphics graphics, Color color);
    }

    public class Rasterization : IRasterization
    {
        public int PixelSize { get; } = 10;

        public void DrawGrid(Graphics graphics)
        {
            var width = graphics.ClipBounds.Width;
            var height = graphics.ClipBounds.Height;

            for (var x = 0; x < width; x += PixelSize)
            {
                graphics.DrawLine(Pens.Black, x, 0, x, height);
            }

            for (var y = 0; y < height; y += PixelSize)
            {
                graphics.DrawLine(Pens.Black, 0, y, width, y);
            }
        }

        public Vector2 ToPixelCoords(Vector2 coords)
        {
            return new Vector2(
                ToPixelCoords(coords.X),
                ToPixelCoords(coords.Y)
            );
        }

        public int ToPixelCoords(float coord)
        {
            return (int)coord / PixelSize * PixelSize;
        }

        public void FillPixel(float x, float y, Graphics graphics, Color color)
        {
            var brush = new SolidBrush(color);
            var coords = ToPixelCoords(new Vector2(x, y));
            graphics.FillRectangle(brush, coords.X, coords.Y, PixelSize, PixelSize);
        }
    }
}
