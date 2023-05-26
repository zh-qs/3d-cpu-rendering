using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GKProject.Drawing
{
    public class Material
    {
        public Vector3 Ka { get; set; }
        public Vector3 Kd { get; set; }
        public Vector3 Ks { get; set; }
        public float Ns { get; set; }

        public static Material WhiteDull => new Material() { Ka = new Vector3(0, 0, 0), Kd = new Vector3(1, 1, 1), Ks = new Vector3(0, 0, 0), Ns = 1 };
    }
}
