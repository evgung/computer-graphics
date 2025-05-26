using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphics
{
    public class Matrix
    {
        private float[,] matrix;

        public Matrix(int height, int width)
        {
            Width = width;
            Height = height;
            matrix = new float[Height, Width];
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public float this[int row, int col]
        {
            get => matrix[row, col];
            set => matrix[row, col] = value;
        }

        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            var result = new Matrix(matrix1.Height, matrix2.Width);

            for (int i = 0; i < matrix1.Height; i++)
                for (int j = 0; j < matrix2.Width; j++)
                    for (int k = 0; k < matrix1.Width; k++)
                        result[i, j] += matrix1[i, k] * matrix2[k, j];

            return result;
        }

        public static Vector3D operator *(Matrix matrix, Vector3D vector)
        {
            var mult = matrix * vector.ToMatrix();
            return new Vector3D(mult);
        }
    }
}
