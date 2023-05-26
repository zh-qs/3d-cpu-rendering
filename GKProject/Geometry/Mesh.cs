using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GKProject.Drawing;
using System.Drawing;
using GKProject.Drawing.Shading;

namespace GKProject.Geometry
{
    public class Mesh : Solid
    {
        List<Triangle> triangles;

        public Mesh(List<Triangle> triangles) : base()
        {
            this.triangles = triangles;

            float h = float.NegativeInfinity;
            foreach (var triangle in triangles)
            {
                float triangleMaxY = triangle.GetMaxYCoordinate();
                h = MathF.Max(h, triangleMaxY);
            }
            Height = h;
        }

        public override void RenderTo(DirectBufferedBitmap bitmap, Matrix4x4 PV, ShadingMethod method)
        {
            if (!Visible) return;

            Matrix4x4 PVTransposed = Matrix4x4.Transpose(PV);

            foreach (var triangle in triangles)
            {
                var shader = method.GetShaderForTriangle(triangle.Transform(PVTransposed, MTransposed, MInverted, translation));
                shader.DrawTo(bitmap);
            }
        }
    }
}
