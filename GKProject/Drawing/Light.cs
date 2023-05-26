using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GKProject.Drawing
{
    public class Light
    {
        public Vector3 Color { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }
        public float IntensityCoefficient { get; set; }

        public void SetColor(Color color)
        {
            Color = new Vector3(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f);
        }
    }
}
