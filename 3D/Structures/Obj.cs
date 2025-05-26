using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphics
{
    public class Obj
    {
        public Obj(Color color)
        {
            Color = color;
        }

        public List<Surface> Surfaces { get; private set; } = new();
        public Color Color { get; set; }

        public bool IsEmpty => Surfaces.Count == 0;

        public Obj Transform(Matrix matrix)
        {
            var obj = new Obj(Color);

            obj.Surfaces = Surfaces.Select
            (
                surface => new Surface(surface.Vertices.Select
                (
                    vertex => vertex.Transform(matrix)
                ).ToList())
            ).ToList(); 

            return obj;
        }
    }
}
