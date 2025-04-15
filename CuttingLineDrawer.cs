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
    public class CuttingLineDrawer : ILineDrawer
    {
        public IRasterization Rast { get; }
        public ILineDrawer MainDrawer { get; }
        public Rectangle CutRect { get; }
        public Color OutsideColor { get; set; }

        public CuttingLineDrawer(IRasterization rast, ILineDrawer mainDrawer, Rectangle cutRect, Color outsideColor)
        {
            Rast = rast;
            MainDrawer = mainDrawer;
            CutRect = cutRect;
            OutsideColor = outsideColor;
        }

        public void DrawLine(Vector2 from, Vector2 to, Graphics graphics, Color color)
        {
            from = Rast.ToPixelCoords(from);
            to = Rast.ToPixelCoords(to);
            var fromCode = GetPointCode(from);
            var toCode = GetPointCode(to);
            var newFrom = from;
            var newTo = to;

            while ((fromCode | toCode) != 0)
            {
                if ((fromCode & toCode) != 0)
                {
                    MainDrawer.DrawLine(from, to, graphics, OutsideColor);
                    return;
                }
                var dx = newTo.X - newFrom.X;
                var dy = newTo.Y - newFrom.Y;

                if (fromCode != 0)
                {
                    newFrom = MovePoint(newFrom, dx, dy);
                    fromCode = GetPointCode(newFrom);
                }
                else
                {
                    newTo = MovePoint(newTo, dx, dy);
                    toCode = GetPointCode(newTo);
                }
            }
                
            DrawCutLine(graphics, color, from, to, newFrom, newTo);
        }

        private Vector2 MovePoint(Vector2 point, float dx, float dy)
        {
            if (point.X < CutRect.Left)
            {
                point.Y += dy / dx * (CutRect.Left - point.X);
                point.X = CutRect.Left;
            }
            else if (point.X > CutRect.Right)
            {
                point.Y += dy / dx * (CutRect.Right - point.X);
                point.X = CutRect.Right;
            }
            else if (point.Y < CutRect.Top)
            {
                point.X += dx / dy * (CutRect.Top - point.Y);
                point.Y = CutRect.Top;
            }
            else if (point.Y > CutRect.Bottom)
            {
                point.X += dx / dy * (CutRect.Bottom - point.Y);
                point.Y = CutRect.Bottom;
            }

            return Rast.ToPixelCoords(point);
        }
        
        private int GetPointCode(Vector2 point)
        {
            var code = new StringBuilder();

            code.Append(point.Y < CutRect.Top ? 1 : 0);
            code.Append(point.Y > CutRect.Bottom ? 1 : 0);
            code.Append(point.X > CutRect.Right ? 1 : 0);
            code.Append(point.X < CutRect.Left ? 1 : 0);

            return Convert.ToInt32(code.ToString(), 2);
        }

        private void DrawCutLine(Graphics graphics, Color color, 
            Vector2 from, Vector2 to, Vector2 newFrom, Vector2 newTo)
        {
            if (from != newFrom)
                MainDrawer.DrawLine(from, newFrom, graphics, OutsideColor);
            if (to != newTo)
                MainDrawer.DrawLine(to, newTo, graphics, OutsideColor);

            MainDrawer.DrawLine(newFrom, newTo, graphics, color);
        }
    }
}
