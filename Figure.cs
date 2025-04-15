using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerGraphics
{
    public enum Vertex
    {
        CentralUpper = 0,
        LeftUpper = 1,
        RightUpper = 2,
        LeftLower = 3,
        RightLower = 4,
        CentralLower = 5,
    }

    public class Figure
    {
        private readonly Dictionary<Vertex, ColoredPoint> vertices = new()
        {
            [Vertex.CentralUpper] = new(new Vector2(100, 0), Color.Red),
            [Vertex.LeftUpper] = new(new Vector2(0, 50), Color.Green),
            [Vertex.LeftLower] = new(new Vector2(0, 180), Color.Blue),
            [Vertex.RightUpper] = new(new Vector2(200, 50), Color.Blue),
            [Vertex.RightLower] = new(new Vector2(200, 180), Color.Green),
            [Vertex.CentralLower] = new(new Vector2(100, 230), Color.Red),
        };

        private readonly Dictionary<Vertex, List<Vertex>> toConnect = new()
        {
            [Vertex.LeftUpper] = new() { Vertex.CentralUpper, Vertex.RightUpper, Vertex.LeftLower, Vertex.CentralLower },
            [Vertex.RightUpper] = new() { Vertex.CentralUpper, Vertex.RightLower, Vertex.CentralLower },
            [Vertex.CentralLower] = new() { Vertex.LeftLower, Vertex.RightLower },
        };

        private readonly List<Vertex[]> triangles = new()
        {
            new[] { Vertex.CentralUpper, Vertex.LeftUpper, Vertex.RightUpper },
            new[] { Vertex.LeftUpper, Vertex.RightUpper, Vertex.CentralLower },
            new[] { Vertex.LeftUpper, Vertex.LeftLower, Vertex.CentralLower },
            new[] { Vertex.RightUpper, Vertex.RightLower, Vertex.CentralLower },
        };

        public void DrawFigure(Graphics graphics, ILineDrawer drawer, Vector2 leftUpperCorner, Color color)
        {
            foreach (var pair in toConnect)
            {
                foreach (var point in pair.Value)
                {
                    drawer.DrawLine(
                        vertices[pair.Key].Point + leftUpperCorner,
                        vertices[point].Point + leftUpperCorner,
                        graphics,
                        color
                    );
                }
            }
        }

        public void FillFigure(Graphics graphics, ITriangleFiller triangleFiller, Vector2 leftUpperCorner)
        {
            foreach (var triangle in triangles)
            {
                triangleFiller.FillTriangle(
                    graphics,
                    vertices[triangle[0]].Move(leftUpperCorner),
                    vertices[triangle[1]].Move(leftUpperCorner),
                    vertices[triangle[2]].Move(leftUpperCorner)
                );
            }
        }

        public Figure ResizeFigure(float k)
        {
            var figure = new Figure();

            foreach (var vertex in figure.vertices)
            {
                figure.vertices[vertex.Key] = vertex.Value with
                {
                    Point = vertex.Value.Point * k
                };
            }

            return figure;
        }
    }
}
