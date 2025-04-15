using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComputerGraphics
{
    public interface ITriangleFiller
    {
        public IRasterization Rast { get; }
        void FillTriangle(Graphics graphics, ColoredPoint point1, ColoredPoint point2, ColoredPoint point3);
    }

    public class TriangleFiller : ITriangleFiller
    {
        public IRasterization Rast { get; }

        public TriangleFiller(IRasterization rast)
        {
            Rast = rast;
        }

        public void FillTriangle(Graphics graphics, ColoredPoint point1, ColoredPoint point2, ColoredPoint point3)
        {
            var points = new List<ColoredPoint> { point1, point2, point3 }
                .OrderBy(point => point.Point.Y)
                .ToList();

            // Верхняя половина
            FillHalfOfTriangle(graphics, points[0], points[1], points[0], points[2]);
            // Нижняя половина
            FillHalfOfTriangle(graphics, points[1], points[2], points[0], points[2]);
        }

        private void FillHalfOfTriangle
            (Graphics graphics, ColoredPoint from, ColoredPoint to, ColoredPoint point1, ColoredPoint point3)
        {
            for (var y = from.Point.Y; y <= to.Point.Y; y += Rast.PixelSize)
            {
                var t1 = (to.Point.Y != from.Point.Y) 
                    ? (y - from.Point.Y) / (to.Point.Y - from.Point.Y) 
                    : 1;
                var t2 = (point3.Point.Y != point1.Point.Y)
                    ? (y - point1.Point.Y) / (point3.Point.Y - point1.Point.Y)
                    : 1;

                var x1 = InterpolatePoint(from.Point.X, to.Point.X, t1);
                var x2 = InterpolatePoint(point1.Point.X, point3.Point.X, t2);

                var color1 = InterpolateColor(from.Color, to.Color, t1);
                var color2 = InterpolateColor(point1.Color, point3.Color, t2);

                DrawLine(graphics, y, Rast.ToPixelCoords(x1), Rast.ToPixelCoords(x2), color1, color2);
            }
        }

        private void DrawLine(Graphics graphics, float y, float x1, float x2, Color color1, Color color2)
        {
            if (x1 > x2)
            {
                (x1, x2) = (x2, x1);
                (color1, color2) = (color2, color1);
            }

            for (var x = x1; x <= x2; x += Rast.PixelSize)
            {
                var t = (x1 != x2) ? (x - x1) / (x2 - x1) : 1;
                var color = InterpolateColor(color1, color2, t);
                Rast.FillPixel(x, y, graphics, color);
            }
        }

        private float InterpolatePoint(float begin, float end, float t) => begin + (end - begin) * t;

        private Color InterpolateColor(Color color1, Color color2, float t)
        {
            return Color.FromArgb(
                (int)InterpolatePoint(color1.R, color2.R, t),
                (int)InterpolatePoint(color1.G, color2.G, t),
                (int)InterpolatePoint(color1.B, color2.B, t)
            );
        }
    }
}
