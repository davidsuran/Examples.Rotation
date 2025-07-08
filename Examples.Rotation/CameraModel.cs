using Microsoft.Xna.Framework;

namespace CameraControllerDemo
{
    internal class CameraModel
    {
        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }
        public Matrix CameraRotationMatrix { get; set; }

        public CameraModel(Vector3 position, Vector3 target)
        {
            Position = position;
            Target = target;
            CameraRotationMatrix = Matrix.Identity;
        }
    }
}
