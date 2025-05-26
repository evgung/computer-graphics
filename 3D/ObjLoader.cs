using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphics
{
    public class ObjLoader
    {
        public float MinX { get; private set; } = float.MaxValue;
        public float MaxX { get; private set; } = float.MinValue;
        public float MinY { get; private set; } = float.MaxValue;
        public float MaxY { get; private set; } = float.MinValue;
        public float MinZ { get; private set; } = float.MaxValue;
        public float MaxZ { get; private set; } = float.MinValue;
        public Stack<Color> Colors { get; }

        public ObjLoader(Stack<Color> colors)
        {
            Colors = colors;
        }

        public List<Obj> LoadFromFile(string filePath)
        {
            var objList = new List<Obj>();

            using (var reader = new StreamReader(filePath))
            {
                string line;
                var obj = CreateNewObj();
                var vertices = new List<Vector3D>();
                var normals = new List<Vector3D>();

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("o "))
                    {
                        if (!obj.IsEmpty)
                        {
                            objList.Add(obj);
                            obj = CreateNewObj();
                        }
                    }
                    else if (line.StartsWith("v "))
                    {
                        vertices.Add(ParseVertex(line));
                    }
                    else if (line.StartsWith("vn "))
                    {
                        normals.Add(ParseVertex(line));
                    }
                    else if (line.StartsWith("f "))
                    {
                        var surface = ParseSurface(line, vertices, normals);
                        obj.Surfaces.Add(surface);
                    }
                }

                objList.Add(obj);
            }

            return objList;
        }

        private Obj CreateNewObj()
        {
            if (!Colors.TryPop(out var color))
            {
                color = Color.Black;
            }
            return new Obj(color);
        }

        private Vector3D ParseVertex(string line)
        {
            var culture = CultureInfo.InvariantCulture;
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var x = float.Parse(parts[1], culture);
            var y = float.Parse(parts[2], culture);
            var z = float.Parse(parts[3], culture);

            MinX = Math.Min(x, MinX);
            MaxX = Math.Max(x, MaxX);
            MinY = Math.Min(y, MinY);
            MaxY = Math.Max(y, MaxY);
            MinZ = Math.Min(z, MinZ);
            MaxZ = Math.Max(z, MaxZ);

            return new Vector3D(x, y, z);
        }

        private Surface ParseSurface(string line, List<Vector3D> vertices, List<Vector3D> normals)
        {
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var surface = new Surface();

            for (int i = 1; i < parts.Length; i++)
            {
                var indices = parts[i].Split('/');
                var vIndex = int.Parse(indices[0]) - 1;
                var nIndex = int.Parse(indices[2]) - 1;
                var vertex = vertices[vIndex];
                var normal = normals[nIndex];

                surface.Vertices.Add(new Vertex(vertex, normal));
            }

            return surface;
        }
    }
}
