using GKProject.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GKProject.Drawing.Shading
{
    public abstract class Shader
    {
        protected Scene scene;
        protected TransformedTriangle triangle;

        Vector4 initial_current, initial_prev, initial_next;
        bool draw = true;

        // first, second, third coordinated in NDC
        protected float fx, sx, tx, fy, sy, ty;

        void SetDraw(Vector4 v)
        {
            if (v.X < -1 || v.X > 1 || v.Y < -1 || v.Y > 1 || v.Z < -1 || v.Z > 1) draw = false;
        }

        public Shader(TransformedTriangle triangle, Scene scene)
        {
            this.scene = scene;
            this.triangle = triangle;

            // transform into NDC
            initial_current = triangle.firstInClippingSpace / triangle.firstInClippingSpace.W;
            initial_prev = triangle.secondInClippingSpace / triangle.secondInClippingSpace.W;
            initial_next = triangle.thirdInClippingSpace / triangle.thirdInClippingSpace.W;

            // check if triangle fits in cube [-1,1]^3
            SetDraw(initial_prev);
            SetDraw(initial_next);
            SetDraw(initial_current);

            // cull backfaces
            if (Vector3.Dot(triangle.triangleNormal, triangle.firstInModelSpace - scene.Observer) >= 0)
            {
                draw = false;
            }

            if (!draw) return;

            // transform to screen coordinates and sort by Y coordinate
            initial_current.X = (initial_current.X + 1) * scene.ScreenWidth / 2;
            initial_prev.X = (initial_prev.X + 1) * scene.ScreenWidth / 2;
            initial_next.X = (initial_next.X + 1) * scene.ScreenWidth / 2;

            initial_current.Y = (-initial_current.Y + 1) * scene.ScreenHeight / 2;
            initial_prev.Y = (-initial_prev.Y + 1) * scene.ScreenHeight / 2;
            initial_next.Y = (-initial_next.Y + 1) * scene.ScreenHeight / 2;

            initial_current.Z = (initial_current.Z + 1) / 2;
            initial_prev.Z = (initial_prev.Z + 1) / 2;
            initial_next.Z = (initial_next.Z + 1) / 2;

            fx = initial_current.X;
            fy = initial_current.Y;

            sx = initial_prev.X;
            sy = initial_prev.Y;

            tx = initial_next.X;
            ty = initial_next.Y;

            if (initial_prev.Y < initial_next.Y)
            {
                var v = initial_next;
                initial_next = initial_prev;
                initial_prev = v;
            }

            if (initial_current.Y > initial_prev.Y)
            {
                var v = initial_current;
                initial_current = initial_next;
                initial_next = initial_prev;
                initial_prev = v;
            }
            else if (initial_current.Y > initial_next.Y)
            {
                var v = initial_next;
                initial_next = initial_current;
                initial_current = v;
            }
        }
        public abstract Vector3 GetColorAtPointByInterpolationCoefficients(float a1, float a2, float a3, float a);

        // scan-line algorithm for triangles
        public void DrawTo(DirectBufferedBitmap bitmap)
        {
            if (!draw) return;
            ActiveEdge ae1 = null, ae2 = null;
            int ymin = (int)MathF.Round(initial_current.Y), ymax = (int)MathF.Round(Math.Max(initial_next.Y, initial_prev.Y));
            Vector4 current = initial_current, prev = initial_prev, next = initial_next;
            for (int y = ymin; y <= ymax; y++)
            {
                while (y - 1 == (int)MathF.Round(current.Y))
                {
                    if (prev.Y > current.Y)
                    {
                        if (ae1 == null)
                            ae1 = new ActiveEdge(prev, current);
                        else
                            ae2 = new ActiveEdge(prev, current);
                    }
                    else if (prev.Y < current.Y)
                    {
                        if (ae1.EdgeStart == prev)
                        {
                            ae1 = ae2;
                            ae2 = null;
                        }
                        else if (ae2.EdgeStart == prev)
                        {
                            ae2 = null;
                        }
                        
                    }
                    if (next.Y > current.Y)
                    {
                        if (ae1 == null)
                            ae1 = new ActiveEdge(current, next);
                        else
                            ae2 = new ActiveEdge(current, next);
                    }
                    else if (next.Y < current.Y)
                    {
                        if (ae1.EdgeStart == current)
                        {
                            ae1 = ae2;
                            ae2 = null;
                        }
                        else if (ae2.EdgeStart == current)
                        {
                            ae2 = null;
                        }
                    }
                    Vector4 new_prev = current;
                    current = next;
                    next = prev;
                    prev = new_prev;
                }
                if (ae2 != null && ae1.X > ae2.X)
                {
                    ActiveEdge ae = ae1;
                    ae1 = ae2;
                    ae2 = ae;
                }

                if (ae1 != null && ae2 != null)
                {
                    for (int x = (int)MathF.Round(ae1.X); x < (int)MathF.Round(ae2.X); ++x)
                    {
                        float depth = GetInterpolatedDepthAtPoint(x, y);

                        (float a1, float a2, float a3, float a) = GetInterpolationCoefficientsAtPoint(x, y);
                        Vector3 point = (triangle.firstInModelSpace * a1 + triangle.secondInModelSpace * a2 + triangle.thirdInModelSpace * a3) / a;
                        Vector3 color = GetColorAtPointByInterpolationCoefficients(a1, a2, a3, a);

                        DrawingMethods.PutPixel(bitmap, x, y, depth, MakeFog(color, point));
                    }
                }
                if (ae1 != null) ae1.X += ae1.InverseM;
                if (ae2 != null) ae2.X += ae2.InverseM;
            }
        }

      
        public float GetInterpolatedDepthAtPoint(int x, int y)
        {
            float area1 = Area(x, y, initial_current.X, initial_current.Y, initial_next.X, initial_next.Y),
                area2 = Area(x, y, initial_prev.X, initial_prev.Y, initial_next.X, initial_next.Y),
                area3 = Area(x, y, initial_current.X, initial_current.Y, initial_prev.X, initial_prev.Y),
                a = area1 + area2 + area3;

            area1 /= a;
            area2 /= a;
            area3 /= a;

            return initial_prev.Z * area1 + initial_current.Z * area2 + initial_next.Z * area3;
        }

        (float a1, float a2, float a3, float a) GetInterpolationCoefficientsAtPoint(int x, int y)
        {
            float area1 = Area(x, y, sx, sy, tx, ty),
               area2 = Area(x, y, fx, fy, tx, ty),
               area3 = Area(x, y, fx, fy, sx, sy),
               a = area1 + area2 + area3;

            area1 /= a * triangle.firstInClippingSpace.W;
            area2 /= a * triangle.secondInClippingSpace.W;
            area3 /= a * triangle.thirdInClippingSpace.W;

            a = area1 + area2 + area3;

            return (area1, area2, area3, a);
        }

        protected float Area(float x1, float y1, float x2, float y2, float x3, float y3)
        {
            return MathF.Abs((x2 - x1) * (y3 - y1) - (y2 - y1) * (x3 - x1)) / 2;
        }

        protected Vector3 GetPhongColorAtPoint(Vector3 point, Vector3 normal, Light light)
        {
            Vector3 L = Vector3.Normalize(light.Position - point);
            float prodNL = Vector3.Dot(L, normal);
            Vector3 R = Vector3.Normalize(2 * prodNL * normal - L);
            Vector3 V = Vector3.Normalize(scene.Observer - point);
            float intensity = MathF.Pow(MathF.Max(0, Vector3.Dot(-light.Direction, L)), light.IntensityCoefficient);
            return (triangle.material.Kd * MathF.Max(prodNL, 0) + triangle.material.Ks * MathF.Pow(MathF.Max(0, Vector3.Dot(V, R)), triangle.material.Ns)) * light.Color * intensity;
        }
        Color MakeFog(Vector3 color, Vector3 point)
        {
            float distance = (scene.Observer - point).Length();
            float percent = MathF.Exp(-(scene.FogDensity * distance) * (scene.FogDensity * distance));
            return (color * percent + scene.FogColor * (1 - percent)).ToColor();
        }

    }
}
