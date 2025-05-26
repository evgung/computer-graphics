using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphics
{
    public interface ITriangleFiller
    {
        void FillTriangle(IRasterization rast, ColoredPoint point1, ColoredPoint point2, ColoredPoint point3);
    }
}
