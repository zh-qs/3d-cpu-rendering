using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKProject.Drawing
{
    static class DrawingMethods
    {
        public static void PutPixel(DirectBufferedBitmap bitmap, int x, int y, float z, Color color)
        {
            if (x >= 0 && x < bitmap.Width && y >= 0 && y < bitmap.Height) bitmap.SetPixel(x, y, z, color);
        }
    }
}
