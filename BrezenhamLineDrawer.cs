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
        public IRasterization Rast { get; }

        public BrezenhamLineDrawer(IRasterization rast)
        {
            Rast = rast;
        }

        public void DrawLine(Vector2 from, Vector2 to, Graphics graphics, Color color)
        {
            var x0 = Rast.ToPixelCoords(from).X;
            var y0 = Rast.ToPixelCoords(from).Y;
            var x1 = Rast.ToPixelCoords(to).X;
            var y1 = Rast.ToPixelCoords(to).Y;
            var dx = Math.Abs(x1 - x0);
            var dy = Math.Abs(y1 - y0);
            var sx = (x0 < x1) ? Rast.PixelSize : -Rast.PixelSize; // Направление по X
            var sy = (y0 < y1) ? Rast.PixelSize : -Rast.PixelSize; // Направление по Y
            var err = dx - dy;

            while (true)
            {
                Rast.FillPixel(x0, y0, graphics, color);
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
