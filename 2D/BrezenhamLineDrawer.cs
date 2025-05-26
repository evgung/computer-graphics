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
    public class BrezenhamLineDrawer : ILineDrawer
    {
        public void DrawLine(Vector2 from, Vector2 to, IRasterization rast, Color color)
        {
            var x0 = rast.ToPixelCoords(from).X;
            var y0 = rast.ToPixelCoords(from).Y;
            var x1 = rast.ToPixelCoords(to).X;
            var y1 = rast.ToPixelCoords(to).Y;
            var dx = Math.Abs(x1 - x0);
            var dy = Math.Abs(y1 - y0);
            var sx = (x0 < x1) ? rast.PixelSize : -rast.PixelSize; // Направление по X
            var sy = (y0 < y1) ? rast.PixelSize : -rast.PixelSize; // Направление по Y
            var err = dx - dy;

            while (true)
            {
                rast.FillPixel(new ColoredPoint(new Vector2(x0, y0), color));
                if (x0 == x1 && y0 == y1) break;

                var e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }
    }
}
