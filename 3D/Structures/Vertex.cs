using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphics
{
    public struct Vertex
    {
        public Vertex(Vector3D coordinates, Vector3D normal)
        {
            Coordinates = coordinates;
            Normal = normal;
        }

        public Vector3D Coordinates { get; set; }
        public Vector3D Normal { get; set; }

        public Vertex Transform(Matrix matrix)
        {
            return new Vertex(matrix * Coordinates, Normal);
        }
    }
}
