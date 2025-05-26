using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace ComputerGraphics
{
    public partial class Form3D : Form
    {
        private readonly Stack<Color> colors = new(new[]
        {
            Color.Red,
            Color.Green,
            Color.Blue,
        });

        private readonly ObjLoader objLoader;
        private readonly List<Obj> objs;
        private readonly Vector3 light = new(0.2f, 0.7f, 1f);
        private readonly Camera camera = new(
            position: new Vector3(0.4f, 0.4f, 1f), 
            target: new Vector3(0, 0, 0)
        );

        private readonly ITriangleFiller triangleFiller = new TriangleFiller(true);
        private Rasterization3D rast;
        private Matrix mainMatrix;

        public Form3D()
        {
            InitializeComponent();

            rast = new(pictureBox.Width, pictureBox.Height);
            objLoader = new(colors);
            objs = objLoader.LoadFromFile("Scene.obj");
            FillTransMatrix();

            KeyDown += OnKeyDown;
            pictureBox.Paint += OnPaint;
        }

        private void FillTransMatrix()
        {
            var projection = TransformationMatrices.ProjOrto(
                objLoader.MinX, 
                objLoader.MaxX, 
                objLoader.MinY, 
                objLoader.MaxY,
                objLoader.MinZ,
                objLoader.MaxZ
            );
            
            var scale = TransformationMatrices.Scale(0.3f, 0.3f, 0.3f);

            var lookAt = TransformationMatrices.LookAt(camera);

            var depth = objLoader.MaxZ - objLoader.MinZ;
            var viewport = TransformationMatrices.Viewport(-100, -200, pictureBox.Width, pictureBox.Height, depth);

            mainMatrix = viewport * projection * lookAt * scale;
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            rast.ClearBuffers();

            foreach (var obj in objs)
            {
                var newObj = RotateObj(obj);
                DrawObj(newObj);
            }

            g.DrawImage(rast.Bitmap, 0, 0);
        }

        private Obj RotateObj(Obj obj)
        {
            var rotation = TransformationMatrices.YRotation(camera.Angle);
            var finalMatrix = mainMatrix * rotation;
            return obj.Transform(finalMatrix);
        }

        private void DrawObj(Obj obj)
        {
            foreach (var surface in obj.Surfaces)
            {
                var points = surface.Vertices
                    .Select(vertex => vertex.Coordinates.ToColoredPoint(
                        CalculateColor(vertex.Normal.ToVector3(), obj.Color))
                    )
                    .ToArray();

                triangleFiller.FillTriangle(
                    rast,
                    points[0],
                    points[1],
                    points[2]
                );
            }
        }

        private Color CalculateColor(Vector3 normal, Color color)
        {
            var dot = Vector3.Dot(normal, light);
            var intensity = dot < 0.0f ? 0.0f : (dot > 1f ? 1f : dot);

            return Color.FromArgb(
                (int)(color.R * intensity),
                (int)(color.G * intensity),
                (int)(color.B * intensity)
            );
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                camera.RotateRight();
                Refresh();
            }
            else if (e.KeyCode == Keys.Left)
            {
                camera.RotateLeft();
                Refresh();
            }
        }
    }
}
