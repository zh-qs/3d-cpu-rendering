using GKProject.Drawing;
using GKProject.Drawing.Shading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GKProject.Geometry
{
    public abstract class Solid
    {
        protected Matrix4x4 modelMatrix = Matrix4x4.Identity;
        protected Matrix4x4 MTransposed = Matrix4x4.Identity;
        protected Matrix4x4 MInverted = Matrix4x4.Identity;

        protected Vector4 translation = Vector4.Zero;
        private Vector3 modelDirection = new Vector3(1, 0, 0);

        public bool Visible { get; set; } = true;

        public float Height { get; protected set; }
        public Vector3 ModelDirection { get => modelDirection; set => modelDirection = Vector3.Normalize(value); }
        public Vector3 CameraDirection { get => Vector3.Transform(Height * modelDirection - new Vector3(0, 2 * Height, 0), MTransposed); }
        public Vector3 CameraTarget { get => Vector3.Transform(new Vector3(0, Height + 0.01f, 0), MTransposed) + new Vector3(translation.X, translation.Y, translation.Z); }

        public Vector3 Translation { get => new Vector3(translation.X, translation.Y, translation.Z); }

        public void TransformByMatrix(Matrix4x4 transformationMatrix)
        {
            modelMatrix = modelMatrix * transformationMatrix;
            TransposeAndInvertModelMatrix();
        }

        public void Translate(Vector3 translation)
        {
            this.translation += new Vector4(translation, 0);
        }

        public void SetTranslation(Vector3 translation)
        {
            this.translation = new Vector4(translation, 0);
        }

        public void SetModelMatrix(Matrix4x4 modelMatrix)
        {
            this.modelMatrix = modelMatrix;
            TransposeAndInvertModelMatrix();
        }

        private void TransposeAndInvertModelMatrix()
        {
            MTransposed = Matrix4x4.Transpose(modelMatrix);
            Matrix4x4.Invert(modelMatrix, out MInverted);
        }

        public abstract void RenderTo(DirectBufferedBitmap bitmap, Matrix4x4 PV, ShadingMethod method);
    }
}