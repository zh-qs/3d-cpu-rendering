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
    public class PhongShader : Shader
    {
        public PhongShader(TransformedTriangle triangle, Scene scene) : base(triangle, scene)
        {

        }

        public override Vector3 GetColorAtPointByInterpolationCoefficients(float a1, float a2, float a3, float a)
        {
            Vector3 normal = (triangle.firstNormal * a1 + triangle.secondNormal * a2 + triangle.thirdNormal * a3) / a;
            Vector3 point = (triangle.firstInModelSpace * a1 + triangle.secondInModelSpace * a2 + triangle.thirdInModelSpace * a3) / a;

            Vector3 color = triangle.material.Ka * scene.AmbientColor;

            foreach (var light in scene.Lights)
            {
                color += GetPhongColorAtPoint(point, normal, light);
            }
            color = Vector3.Min(color, new Vector3(1, 1, 1));
            return color;
        }
    }
}
