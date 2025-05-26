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
    public class CdaLineDrawer : ILineDrawer
    {
        public void DrawLine(Vector2 from, Vector2 to, IRasterization rast, Color color)
        {
            var pixelFrom = rast.ToPixelCoords(from);
            var pixelTo = rast.ToPixelCoords(to);
            var dx = (pixelTo.X - pixelFrom.X) / (float)rast.PixelSize;
            var dy = (pixelTo.Y - pixelFrom.Y) / (float)rast.PixelSize;
            var L = (Math.Abs(dx) > Math.Abs(dy)) ? Math.Abs(dx) : Math.Abs(dy);
            dx = dx / L * rast.PixelSize;
            dy = dy / L * rast.PixelSize;
            
            var x = from.X;
            var y = from.Y;

            for (var i = 0; i <= L; i++)
            {
                rast.FillPixel(new ColoredPoint(new Vector2(x, y), color));
                x += dx;
                y += dy;
            }
        }
    }
}
