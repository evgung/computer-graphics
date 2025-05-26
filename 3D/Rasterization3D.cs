using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphics
{
    public class Rasterization3D : IRasterization
    {
        private readonly float[,] zBuffer;
        public int PixelSize { get; } = 1;
        public Bitmap Bitmap { get; private set; }
        public int Width { get; }
        public int Height { get; }

        public Rasterization3D(int width, int height)
        {
            Width = width;
            Height = height;
            zBuffer = new float[width, height];
            Bitmap = new Bitmap(Width, Height);
        }

        public void ClearBuffers()
        {
            Bitmap = new Bitmap(Width, Height);
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    zBuffer[x, y] = float.MinValue;
        }

        public void FillPixel(ColoredPoint pixel)
        {
            var x = (int)pixel.Point.X;
            var y = (int)pixel.Point.Y;

            if (pixel.Z > zBuffer[x, y])
            {
                zBuffer[x, y] = (float)pixel.Z;
                Bitmap.SetPixel(x, y, pixel.Color);
            }
        }

        public void DrawGrid() { }

        public int ToPixelCoords(float coord) => (int)coord;
    }
}
