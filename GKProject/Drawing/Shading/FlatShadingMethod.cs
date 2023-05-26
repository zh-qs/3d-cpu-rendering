using GKProject.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKProject.Drawing.Shading
{
    public class FlatShadingMethod : ShadingMethod
    {
        public FlatShadingMethod(Scene scene) : base(scene)
        {
        }

        public override Shader GetShaderForTriangle(TransformedTriangle triangle)
        {
            return new FlatShader(triangle, scene);
        }
    }
}
