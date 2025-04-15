using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphics
{
    public struct ColoredPoint
    {
        public Vector2 Point;
        public Color Color;

        public ColoredPoint(Vector2 point, Color color)
        {
            Point = point;
            Color = color;
        }

        public ColoredPoint Move(Vector2 vector)
        {
            return new ColoredPoint(Point + vector, Color);
        }
    }
}
