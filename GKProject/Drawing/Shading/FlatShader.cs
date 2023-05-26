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
    public class FlatShader : Shader
    {
        Vector3 color;
        public FlatShader(TransformedTriangle triangle, Scene scene) : base(triangle, scene)
        {

            var center = (triangle.firstInModelSpace + triangle.secondInModelSpace + triangle.thirdInModelSpace) / 3;

            var normalInCenter = (triangle.firstNormal / triangle.firstInClippingSpace.W + triangle.secondNormal / triangle.secondInClippingSpace.W + triangle.thirdNormal / triangle.thirdInClippingSpace.W)
                / (1 / triangle.firstInClippingSpace.W + 1 / triangle.secondInClippingSpace.W + 1 / triangle.thirdInClippingSpace.W);
            normalInCenter = Vector3.Normalize(normalInCenter);

            color = triangle.material.Ka * scene.AmbientColor;

            foreach (var light in scene.Lights)
            {
                color += GetPhongColorAtPoint(center, normalInCenter, light);
            }
            color = Vector3.Min(color, new Vector3(1, 1, 1));
        }

        public override Vector3 GetColorAtPointByInterpolationCoefficients(float a1, float a2, float a3, float a) => color;

        
    }
}
