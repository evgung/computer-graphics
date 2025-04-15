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
        public IRasterization Rast { get; }

        public CdaLineDrawer(IRasterization rast)
        {
            Rast = rast;
        }

        public void DrawLine(Vector2 from, Vector2 to, Graphics graphics, Color color)
        {
            var pixelFrom = Rast.ToPixelCoords(from);
            var pixelTo = Rast.ToPixelCoords(to);
            var dx = (pixelTo.X - pixelFrom.X) / (float)Rast.PixelSize;
            var dy = (pixelTo.Y - pixelFrom.Y) / (float)Rast.PixelSize;
            var L = (Math.Abs(dx) > Math.Abs(dy)) ? Math.Abs(dx) : Math.Abs(dy);
            dx = dx / L * Rast.PixelSize;
            dy = dy / L * Rast.PixelSize;
            
            var x = from.X;
            var y = from.Y;

            for (var i = 0; i <= L; i++)
            {
                Rast.FillPixel(x, y, graphics, color);
                x += dx;
                y += dy;
            }
        }
    }
}
