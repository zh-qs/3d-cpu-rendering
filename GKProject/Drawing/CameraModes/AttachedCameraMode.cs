using GKProject.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GKProject.Drawing.CameraModes
{
    public class AttachedCameraMode : ICameraMode
    {
        public void MoveCameraAfterSolidHasMoved(Scene scene, Solid solid)
        {
            scene.Observer = solid.CameraTarget - solid.CameraDirection;
            scene.Target = solid.CameraTarget;
        }
    }
}
