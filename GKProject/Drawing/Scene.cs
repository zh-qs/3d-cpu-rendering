using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GKProject.Drawing
{
    public class Scene
    {
        public static Vector3 WorldUp => Vector3.UnitY;

        public List<Light> Lights { get; set; } = new List<Light>();
        public Vector3 AmbientColor { get; set; } = new Vector3(1, 1, 1);
        public Vector3 Observer { get; set; }
        public Vector3 Target { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public float Far { get; set; }
        public float Near { get; set; }
        public float Fov { get; set; }
        public Vector3 FogColor { get; set; }
        public float FogDensity { get; set; }


        public void SetAmbientColor(Color color)
        {
            AmbientColor = new Vector3(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f);
        }

        public Matrix4x4 GetView()
        {
            Vector3 direction = Observer - Target,
                right = Vector3.Cross(WorldUp, direction);

            if (right == Vector3.Zero) right = Vector3.UnitX;

            Vector3 up = Vector3.Cross(direction, right);

            direction = Vector3.Normalize(direction);
            right = Vector3.Normalize(right);
            up = Vector3.Normalize(up);

            return new Matrix4x4(
                right.X, right.Y, right.Z, 0,
                up.X, up.Y, up.Z, 0,
                direction.X, direction.Y, direction.Z, 0,
                0, 0, 0, 1
                ) * new Matrix4x4(
                    1, 0, 0, -Observer.X,
                    0, 1, 0, -Observer.Y,
                    0, 0, 1, -Observer.Z,
                    0, 0, 0, 1
                    );
        }

        public Matrix4x4 GetProjection()
        {
            float ctg = 1 / MathF.Tan(Fov / 2);
            return new Matrix4x4(
                ctg * ScreenHeight / ScreenWidth, 0, 0, 0,
                0, ctg, 0, 0,
                0, 0, -(Far + Near) / (Far - Near), - 2 * Far * Near / (Far - Near),
                0, 0, -1, 0
                );
        }

    }
}
