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
    public class GoraudShader : Shader
    {
        Vector3 firstColor, secondColor, thirdColor;
        public GoraudShader(TransformedTriangle triangle, Scene scene) : base(triangle, scene)
        {
            firstColor = triangle.material.Ka * scene.AmbientColor;
            secondColor = triangle.material.Ka * scene.AmbientColor;
            thirdColor = triangle.material.Ka * scene.AmbientColor;
            foreach (var light in scene.Lights)
            {
                firstColor += GetPhongColorAtPoint(triangle.firstInModelSpace, triangle.firstNormal, light);
                secondColor += GetPhongColorAtPoint(triangle.secondInModelSpace, triangle.secondNormal, light);
                thirdColor += GetPhongColorAtPoint(triangle.thirdInModelSpace, triangle.thirdNormal, light);
            }
            firstColor = Vector3.Min(firstColor, new Vector3(1, 1, 1));
            secondColor = Vector3.Min(secondColor, new Vector3(1, 1, 1));
            thirdColor = Vector3.Min(thirdColor, new Vector3(1, 1, 1));
        }
        public override Vector3 GetColorAtPointByInterpolationCoefficients(float a1, float a2, float a3, float a)
        { 
            return (firstColor * a1 + secondColor * a2 + thirdColor * a3) / a;
        }
    }
}
