using GKProject.Drawing;
using GKProject.Drawing.Shading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GKProject.Geometry
{
    public class Sphere : Solid
    {
        float radius;
        SphereParameters parameters;

        Material material;
        
        public Sphere(float radius, Material material, SphereParameters parameters) : base()
        {
            this.parameters = parameters;
            this.radius = radius;
            this.material = material;

            Height = radius;
        }

        public override void RenderTo(DirectBufferedBitmap bitmap, Matrix4x4 PV, ShadingMethod method)
        {
            if (!Visible) return;

            int localLngSteps = parameters.LngSteps, localLatSteps = parameters.LatSteps; // we assign to local values, because scene renders asynchronously

            float dLng = MathF.PI * 2 / localLngSteps;
            float dLat = MathF.PI / localLatSteps;

            Matrix4x4 PVTransposed = Matrix4x4.Transpose(PV);

            Vector3[] prevPoints = new Vector3[localLngSteps];
            Vector3[] nextPoints = new Vector3[localLngSteps];

            float lng = 0, lat = dLat;
            Triangle triangle;
            Shader shader;

            for (int i=0;i<localLngSteps;++i)
            {
                prevPoints[i] = new Vector3(radius * MathF.Sin(dLat) * MathF.Cos(lng), radius * MathF.Cos(dLat), radius * MathF.Sin(dLat) * MathF.Sin(lng));
                lng += dLng;
            }
            Vector3 northPole = new Vector3(0, radius, 0);
            for (int i=0;i<localLngSteps - 1;++i)
            {
                triangle = new Triangle(northPole, prevPoints[i + 1], prevPoints[i], new Vector3(0, 1, 0), Vector3.Normalize(prevPoints[i + 1]), Vector3.Normalize(prevPoints[i]), material);
                shader = method.GetShaderForTriangle(triangle.Transform(PVTransposed, MTransposed, MInverted, translation));
                shader.DrawTo(bitmap);
            }
            triangle = new Triangle(northPole, prevPoints[0], prevPoints[prevPoints.Length - 1], new Vector3(0, 1, 0), Vector3.Normalize(prevPoints[0]), Vector3.Normalize(prevPoints[prevPoints.Length - 1]), material);
            shader = method.GetShaderForTriangle(triangle.Transform(PVTransposed, MTransposed, MInverted, translation));
            shader.DrawTo(bitmap);

            for (int j=1; j < localLatSteps - 1;++j)
            {
                lng = 0;
                lat += dLat;
                for (int i = 0; i < localLngSteps; ++i)
                {

                    nextPoints[i] = new Vector3(radius * MathF.Sin(lat) * MathF.Cos(lng), radius * MathF.Cos(lat), radius * MathF.Sin(lat) * MathF.Sin(lng));
                    lng += dLng;
                }
                for (int i = 0; i < localLngSteps - 1; ++i)
                {
                    triangle = new Triangle(prevPoints[i], nextPoints[i + 1], nextPoints[i], Vector3.Normalize(prevPoints[i]), Vector3.Normalize(nextPoints[i + 1]), Vector3.Normalize(nextPoints[i]), material);
                    shader = method.GetShaderForTriangle(triangle.Transform(PVTransposed, MTransposed, MInverted, translation));
                    shader.DrawTo(bitmap);

                    triangle = new Triangle(prevPoints[i], prevPoints[i+1], nextPoints[i + 1], Vector3.Normalize(prevPoints[i]), Vector3.Normalize(prevPoints[i+1]), Vector3.Normalize(nextPoints[i + 1]), material);
                    shader = method.GetShaderForTriangle(triangle.Transform(PVTransposed, MTransposed, MInverted, translation));
                    shader.DrawTo(bitmap);

                }
                triangle = new Triangle(prevPoints[prevPoints.Length-1], nextPoints[0], nextPoints[prevPoints.Length - 1], Vector3.Normalize(prevPoints[prevPoints.Length - 1]), Vector3.Normalize(nextPoints[0]), Vector3.Normalize(nextPoints[prevPoints.Length - 1]), material);
                shader = method.GetShaderForTriangle(triangle.Transform(PVTransposed, MTransposed, MInverted, translation));
                shader.DrawTo(bitmap);

                triangle = new Triangle(prevPoints[prevPoints.Length - 1], prevPoints[0], nextPoints[0], Vector3.Normalize(prevPoints[prevPoints.Length - 1]), Vector3.Normalize(prevPoints[0]), Vector3.Normalize(nextPoints[0]), material);
                shader = method.GetShaderForTriangle(triangle.Transform(PVTransposed, MTransposed, MInverted, translation));
                shader.DrawTo(bitmap);
                for (int i=0;i<localLngSteps;++i)
                {
                    prevPoints[i] = nextPoints[i];
                }
            }

            Vector3 southPole = new Vector3(0, -radius, 0);
            for (int i = 0; i < localLngSteps - 1; ++i)
            {
                triangle = new Triangle(southPole, prevPoints[i], prevPoints[i + 1], new Vector3(0, -1, 0), Vector3.Normalize(prevPoints[i]), Vector3.Normalize(prevPoints[i + 1]), material);
                shader = method.GetShaderForTriangle(triangle.Transform(PVTransposed, MTransposed, MInverted, translation));
                shader.DrawTo(bitmap);
            }
            triangle = new Triangle(southPole, prevPoints[prevPoints.Length - 1], prevPoints[0], new Vector3(0, -1, 0), Vector3.Normalize(prevPoints[prevPoints.Length - 1]), Vector3.Normalize(prevPoints[0]), material);
            shader = method.GetShaderForTriangle(triangle.Transform(PVTransposed, MTransposed, MInverted, translation));
            shader.DrawTo(bitmap);
        }
    }
}
