using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GKProject.Geometry;
using GKProject.Drawing.Shading;

namespace GKProject.Drawing
{
    public class Model
    {
        Dictionary<string, Solid> solids;

        Scene scene;
        ShadingMethod shadingMethod;

        Matrix4x4 PV;

        public ShadingMethod ShadingMethod { get => shadingMethod; set => shadingMethod = value; }

        public Model(Scene scene, ShadingMethod shadingMethod)
        {
            this.scene = scene;
            solids = new Dictionary<string, Solid>();

            this.shadingMethod = shadingMethod;
        }

        public void AddSolid(string solidName, Solid solid)
        {
            if (!solids.TryAdd(solidName, solid))
            {
                throw new ArgumentException($"A solid with name {solidName} already exists.");
            }
        }

        public void AddSolids(Dictionary<string, Solid> solids)
        {
            foreach(var pair in solids)
            {
                AddSolid(pair.Key, pair.Value);
            }
        }

        public void RemoveSolid(string name)
        {
            solids.Remove(name);
        }

        public void RenderTo(DirectBufferedBitmap bitmap)
        {
            PV = scene.GetProjection() * scene.GetView();

            foreach (var pair in solids)
            {
                pair.Value.RenderTo(bitmap, PV, shadingMethod);
            }
        }

        public Solid GetSolid(string solidName)
        {
            if (!solids.TryGetValue(solidName, out Solid solid))
                throw new ArgumentException($"Mesh with name {solidName} doesn't exist.");
            return solid;
        }
    }
}
