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
    public enum FigVertex
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
        private readonly Dictionary<FigVertex, ColoredPoint> vertices = new()
        {
            [FigVertex.CentralUpper] = new(new Vector2(100, 0), Color.Red),
            [FigVertex.LeftUpper] = new(new Vector2(0, 50), Color.Green),
            [FigVertex.LeftLower] = new(new Vector2(0, 180), Color.Blue),
            [FigVertex.RightUpper] = new(new Vector2(200, 50), Color.Blue),
            [FigVertex.RightLower] = new(new Vector2(200, 180), Color.Green),
            [FigVertex.CentralLower] = new(new Vector2(100, 230), Color.Red),
        };

        private readonly Dictionary<FigVertex, List<FigVertex>> toConnect = new()
        {
            [FigVertex.LeftUpper] = new() { FigVertex.CentralUpper, FigVertex.RightUpper, FigVertex.LeftLower, FigVertex.CentralLower },
            [FigVertex.RightUpper] = new() { FigVertex.CentralUpper, FigVertex.RightLower, FigVertex.CentralLower },
            [FigVertex.CentralLower] = new() { FigVertex.LeftLower, FigVertex.RightLower },
        };

        private readonly List<FigVertex[]> triangles = new()
        {
            new[] { FigVertex.CentralUpper, FigVertex.LeftUpper, FigVertex.RightUpper },
            new[] { FigVertex.LeftUpper, FigVertex.RightUpper, FigVertex.CentralLower },
            new[] { FigVertex.LeftUpper, FigVertex.LeftLower, FigVertex.CentralLower },
            new[] { FigVertex.RightUpper, FigVertex.RightLower, FigVertex.CentralLower },
        };

        public void DrawFigure(IRasterization rast, ILineDrawer drawer, Vector2 leftUpperCorner, Color color)
        {
            foreach (var pair in toConnect)
            {
                foreach (var point in pair.Value)
                {
                    drawer.DrawLine(
                        vertices[pair.Key].Point + leftUpperCorner,
                        vertices[point].Point + leftUpperCorner,
                        rast,
                        color
                    );
                }
            }
        }

        public void FillFigure(IRasterization rast, ITriangleFiller triangleFiller, Vector2 leftUpperCorner)
        {
            foreach (var triangle in triangles)
            {
                triangleFiller.FillTriangle(
                    rast,
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
