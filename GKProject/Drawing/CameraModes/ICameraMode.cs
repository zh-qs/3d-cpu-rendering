using GKProject.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKProject.Drawing.CameraModes
{
    public interface ICameraMode
    {
        void MoveCameraAfterSolidHasMoved(Scene scene, Solid solid);
    }
}
