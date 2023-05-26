using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GKProject.Drawing
{
    public static class Vector3Extender
    {
        public static Color ToColor(this Vector3 color)
        {
            return Color.FromArgb((int)MathF.Round(color.X * 255), (int)MathF.Round(color.Y * 255), (int)MathF.Round(color.Z * 255));
        }
    }
}
