using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphics
{
    public static class TransformationMatrices
    {
        public static Matrix ProjOrto(float left, float right, float bottom, float top, float near, float far)
        {
            return new Matrix(4, 4)
            {
                [0, 0] = 2.0f / (right - left),
                [1, 1] = 2.0f / (top - bottom),
                [2, 2] = -2.0f / (far - near),
                [3, 3] = 1.0f,
                [0, 3] = -(right + left) / (right - left),
                [1, 3] = -(top + bottom) / (top - bottom),
                [2, 3] = -(far + near) / (far - near)
            };
        }

        public static Matrix YRotation(double angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);

            return new Matrix(4, 4)
            {
                [0, 0] = cos,
                [0, 2] = sin,
                [2, 0] = -sin,
                [2, 2] = cos,
                [1, 1] = 1,
                [3, 3] = 1
            };
        }

        public static Matrix Viewport(float x, float y, float width, float height, float depth)
        {
            return new Matrix(4, 4)
            {
                [0, 0] = width / 2.0f,
                [1, 1] = -height / 2.0f,
                [2, 2] = depth / 2.0f,
                [3, 3] = 1.0f,
                [0, 3] = x + width / 2.0f,
                [1, 3] = y + height / 2.0f,
                [2, 3] = depth / 2.0f
            };
        }

        public static Matrix LookAt(Camera camera)
        {
            return new Matrix(4, 4)
            {
                [0, 0] = camera.Right.X,
                [0, 1] = camera.Right.Y,
                [0, 2] = camera.Right.Z,
                [1, 0] = camera.Up.X,
                [1, 1] = camera.Up.Y,
                [1, 2] = camera.Up.Z,
                [2, 0] = camera.Direction.X,
                [2, 1] = camera.Direction.Y,
                [2, 2] = camera.Direction.Z,
                [0, 3] = -camera.Position.X,
                [1, 3] = -camera.Position.Y,
                [2, 3] = -camera.Position.Z,
                [3, 3] = 1
            };
        }

        public static Matrix Scale(float kx, float ky, float kz)
        {
            return new Matrix(4, 4)
            {
                [0, 0] = kx,
                [1, 1] = ky,
                [2, 2] = kz,
                [3, 3] = 1
            };
        }
    }
}
