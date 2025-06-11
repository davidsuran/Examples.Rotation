using Microsoft.Xna.Framework;

namespace CameraControllerDemo
{
    internal class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }
        public Matrix CameraRotationMatrix { get; set; }

        public Camera(Vector3 position, Vector3 target)
        {
            Position = position;
            Target = target;
            CameraRotationMatrix = Matrix.Identity;
        }
    }
}
