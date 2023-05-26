using GKProject.Drawing;
using GKProject.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GKProject
{
    // class containing initial configuration of an application
    public static class InitialParameters
    {
        public static Vector3 GetDefaultObserver() => new Vector3(0, 60, 60);

        public static Vector3 GetDefaultTarget() => new Vector3(0, 0, 0);

        public static Scene GetScene(Panel panel)
        {
            return new Scene()
            {
                Observer = GetDefaultObserver(),
                Target = GetDefaultTarget(),
                ScreenHeight = panel.Height,
                ScreenWidth = panel.Width,
                Near = 10,
                Far = 250,
                Fov = MathF.PI / 5,
                FogColor = new Vector3(0.553f, 0.584f, 0.667f),
                FogDensity = 0.01f,
            };
        }

        public static void AddLightsToScene(Scene scene)
        {
            scene.Lights.Add(new Light() { Color = new Vector3(0, 0.5f, 1), Position = new Vector3(0, 50, 50), Direction = new Vector3(0, -1, -1) });
            scene.Lights.Add(new Light() { Color = new Vector3(1, 0.5f, 0), Position = new Vector3(0, 50, -50), Direction = new Vector3(0, -1, 1) });
        }

        public static SphereParameters GetSphereParameters()
        {
            return new SphereParameters() { LatSteps = 10, LngSteps = 10 };
        }

        public static float GetSphereRadius() => 6;

        public static Material GetSphereMaterial()
        {
            return new Material() { Ka = Vector3.Zero, Kd = new Vector3(0, 0, 0.7f), Ks = new Vector3(0.7f, 0.7f, 0.7f), Ns = 15 };
        }

        public static float GetPieceRotationAngle() => MathF.PI / 4;

        public static Light GetPoliceLight(Vector3 initialPosition)
        {
            return new Light() { Color = new Vector3(1, 0, 0), Direction = GetPoliceLightDirection(), Position = initialPosition, IntensityCoefficient = 50 };
        }

        public static Vector3 GetPoliceLightDirection() => Vector3.Normalize(new Vector3(0, -1, -2));
    }
}
