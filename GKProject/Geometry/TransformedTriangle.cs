using GKProject.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GKProject.Geometry
{
    public class TransformedTriangle
    {
        public Vector3 firstInModelSpace, secondInModelSpace, thirdInModelSpace;
        public Vector3 firstNormal, secondNormal, thirdNormal;
        public Vector4 firstInClippingSpace, secondInClippingSpace, thirdInClippingSpace;
        public Vector3 triangleNormal;

        public Material material;
    }
}
