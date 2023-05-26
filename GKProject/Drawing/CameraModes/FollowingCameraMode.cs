using GKProject.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKProject.Drawing.CameraModes
{
    public class FollowingCameraMode : ICameraMode
    {
        public void MoveCameraAfterSolidHasMoved(Scene scene, Solid solid)
        {
            scene.Observer = InitialParameters.GetDefaultObserver();
            scene.Target = solid.CameraTarget;
        }
    }
}
