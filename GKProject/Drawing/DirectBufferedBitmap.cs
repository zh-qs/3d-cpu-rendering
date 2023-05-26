using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GKProject.Drawing
{

    // https://stackoverflow.com/questions/24701703/c-sharp-faster-alternatives-to-setpixel-and-getpixel-for-bitmaps-for-windows-f
    public class DirectBufferedBitmap : IDisposable
    {
        public Bitmap Bitmap { get; private set; }
        public Int32[] Bits { get; private set; }
        public bool Disposed { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        protected float[] zBuffer;
        protected GCHandle zBufferHandle;
        protected Bitmap zBufferBitmap;
        protected Graphics zBG;

        protected GCHandle BitsHandle { get; private set; }
        protected Graphics Graphics { get; private set; }

        public DirectBufferedBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new Int32[width * height];
            zBuffer = new float[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            zBufferHandle = GCHandle.Alloc(zBuffer, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
            zBufferBitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, zBufferHandle.AddrOfPinnedObject());
            zBG = Graphics.FromImage(zBufferBitmap);
            Graphics = Graphics.FromImage(Bitmap);
        }

        public void SetPixel(int x, int y, float z, Color colour)
        {
            int index = x + (y * Width);
            int col = colour.ToArgb();

            if (z < zBuffer[index])
            {
                zBuffer[index] = z;
                Bits[index] = col;
            }
        }

        public bool CanSetPixel(int x, int y, float z)
        {
            return z < zBuffer[x + (y * Width)];
        }

        public Color GetPixel(int x, int y)
        {
            int index = x + (y * Width);
            int col = Bits[index];
            Color result = Color.FromArgb(col);

            return result;
        }

        public void Clear(Color backgroundColor)
        {
            Graphics.Clear(backgroundColor);
            zBG.Clear(Color.FromArgb(0b0_1111_1111_00000_00000_00000_00000_000)); // float positive infinity 
        }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Graphics.Dispose();
            Bitmap.Dispose();
            BitsHandle.Free();

            zBG.Dispose();
            zBufferBitmap.Dispose();
            zBufferHandle.Free();
        }
    }
}
