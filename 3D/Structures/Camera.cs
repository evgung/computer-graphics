using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphics
{
    public class Camera
    {
        private readonly Vector3 upIdentity = new(0, 1, 0);

        public Camera(Vector3 position, Vector3 target)
        {
            Position = position;
            Target = target;
        }

        public Vector3 Position { get; }
        public Vector3 Target { get; }
        public Vector3 Direction => Target - Position;
        public Vector3 Right => Vector3.Cross(upIdentity, Direction);
        public Vector3 Up => Vector3.Cross(Direction, Right);

        public float Angle { get; private set; } = 0;
        public float AngleStep { get; set; } = 0.1f;

        public void RotateLeft()
        {
            Angle -= AngleStep;
        }

        public void RotateRight()
        {
            Angle += AngleStep;
        }
    }
}
