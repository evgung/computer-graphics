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
    public interface ILineDrawer
    {
        void DrawLine(Vector2 from, Vector2 to, IRasterization rast, Color color);
    }
}
