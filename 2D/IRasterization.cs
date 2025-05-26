using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphics
{
    public interface IRasterization
    {
        public int PixelSize { get; }
        public Vector2 ToPixelCoords(Vector2 coords)
        {
            return new Vector2(
                ToPixelCoords(coords.X),
                ToPixelCoords(coords.Y)
            );
        }

        public int ToPixelCoords(float coord);
        public void FillPixel(ColoredPoint pixel);
        public void DrawGrid();
    }
}
