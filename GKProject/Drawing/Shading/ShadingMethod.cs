using GKProject.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKProject.Drawing.Shading
{
    public abstract class ShadingMethod
    {
        protected Scene scene;
        public ShadingMethod(Scene scene)
        {
            this.scene = scene;
        }
        public abstract Shader GetShaderForTriangle(TransformedTriangle triangle);
        
    }
}
