using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using GKProject.Drawing;

namespace GKProject.Geometry
{
    public class Triangle
    {
        Vector4 first, second, third;
        Vector4 normal1, normal2, normal3;
        Vector3 triangleNormal;
        Material material;

        public Triangle(Vector3 first, Vector3 second, Vector3 third, Vector3 normal1, Vector3 normal2, Vector3 normal3, Material material)
        {
            this.first = new Vector4(first, 1);
            this.second = new Vector4(second, 1);
            this.third = new Vector4(third, 1);

            this.normal1 = new Vector4(normal1, 0);
            this.normal2 = new Vector4(normal2, 0);
            this.normal3 = new Vector4(normal3, 0);

            triangleNormal = Vector3.Cross(second - first, third - second);

            this.material = material;
        }

        public TransformedTriangle Transform(Matrix4x4 PVTransposed, Matrix4x4 MTransposed, Matrix4x4 MInverted, Vector4 translation)
        {
            Vector4 fm = Vector4.Transform(first, MTransposed) + translation,
                sm = Vector4.Transform(second, MTransposed) + translation,
                tm = Vector4.Transform(third, MTransposed) + translation;
            Vector4 fpvm = Vector4.Transform(fm, PVTransposed),
                spvm = Vector4.Transform(sm, PVTransposed),
                tpvm = Vector4.Transform(tm, PVTransposed);
            Vector4 n1m = Vector4.Transform(normal1, MInverted),
                n2m = Vector4.Transform(normal2, MInverted),
                n3m = Vector4.Transform(normal3, MInverted),
                nt = Vector4.Transform(triangleNormal, MInverted);
            return new TransformedTriangle()
            {
                firstInClippingSpace = fpvm,
                secondInClippingSpace = spvm,
                thirdInClippingSpace = tpvm,
                firstInModelSpace = new Vector3(fm.X, fm.Y, fm.Z),
                secondInModelSpace = new Vector3(sm.X, sm.Y, sm.Z),
                thirdInModelSpace = new Vector3(tm.X, tm.Y, tm.Z),
                firstNormal = new Vector3(n1m.X, n1m.Y, n1m.Z),
                secondNormal = new Vector3(n2m.X, n2m.Y, n2m.Z),
                thirdNormal = new Vector3(n3m.X, n3m.Y, n3m.Z),
                triangleNormal = new Vector3(nt.X, nt.Y, nt.Z),
                material = material
            };
        }

        public float GetMaxYCoordinate()
        {
            return MathF.Max(first.Y, MathF.Max(second.Y, third.Y));
        }
    }
}
