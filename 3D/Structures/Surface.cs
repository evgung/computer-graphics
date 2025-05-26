using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphics
{
    public class Surface
    {
        public Surface()
        {
        }

        public Surface(List<Vertex> vertices)
        {
            Vertices = vertices;
        }

        public List<Vertex> Vertices { get; } = new();
    }
}
