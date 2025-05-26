using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphics
{
    public struct Vector3D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; } = 1.0f;

        public Vector3D(float x, float y, float z)
        {
            X = x; 
            Y = y; 
            Z = z;
        }

        public Vector3D(float x, float y, float z, float w) : this(x, y, z)
        {
            W = w;
        }

        public Vector3D(Matrix columnMatrix)
        {
            X = columnMatrix[0, 0];
            Y = columnMatrix[1, 0];
            Z = columnMatrix[2, 0];
            W = columnMatrix[3, 0];
        }

        public Vector3D(Vector3 vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }

        public Matrix ToMatrix()
        {
            return new Matrix(4, 1)
            {
                [0, 0] = X,
                [1, 0] = Y,
                [2, 0] = Z,
                [3, 0] = W,
            };
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X / W, Y / W, Z / W);
        }

        public ColoredPoint ToColoredPoint(Color color)
        {
            var vector3 = ToVector3();

            return new ColoredPoint(
                new Vector2(vector3.X, vector3.Y),
                color,
                vector3.Z
            );
        }
    }
}
