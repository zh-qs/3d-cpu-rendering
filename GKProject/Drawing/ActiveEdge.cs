using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace GKProject.Drawing
{
    public class ActiveEdge
    {
        public Vector4 EdgeStart { get; }
        public Vector4 EdgeEnd { get; }
        public float X { get; set; }
        public float InverseM { get; set; }

        public ActiveEdge(Vector4 start, Vector4 end)
        {
            EdgeStart = start;
            EdgeEnd = end;
            X = start.Y > end.Y ? end.X : start.X;
            if (end.Y == start.Y) InverseM = 0;
            else InverseM = (end.X - start.X) / (end.Y - start.Y);
        }
    }
}
