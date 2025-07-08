using Microsoft.Xna.Framework;
using System;

namespace CameraControllerDemo
{
    internal class CameraController : IController
    {
        private CameraModel _camera;
        private bool _orbiting;

        public Vector3 Target => _camera.Target;

        public CameraController(Vector3 position, Vector3 target)
        {
            _camera = new CameraModel(position, target);
            EventDispatcher.OnMoveLeft += OnMoveLeft;
            EventDispatcher.OnMoveRight += OnMoveRight;
            EventDispatcher.OnMoveForward += OnMoveUp;
            EventDispatcher.OnMoveBackward += OnMoveDown;
            EventDispatcher.OnZoomIn += OnZoomIn;
            EventDispatcher.OnZoomOut += OnZoomOut;
            EventDispatcher.OnOrbit += OnSetOnOrbit;
        }

        public Matrix GetViewMatrix()
        {
            return Matrix.CreateLookAt(_camera.Position, _camera.Target, Vector3.Up);
        }

        public Matrix GetWorldMatrix()
        {
            return Matrix.CreateWorld(_camera.Target, Vector3.Forward, Vector3.Up);
        }

        public void Orbit()
        {
            OnOrbit(null, null);
        }

        private void OnMoveLeft(object sender, EventArgs e)
        {
            _camera.Position.X -= 1f;
            _camera.Target.X -= 1f;
        }

        private void OnMoveRight(object sender, EventArgs e)
        {
            _camera.Position.X += 1f;
            _camera.Target.X += 1f;
        }

        private void OnMoveUp(object sender, EventArgs e)
        {
            _camera.Position.Y -= 1f;
            _camera.Target.Y -= 1f;
        }

        private void OnMoveDown(object sender, EventArgs e)
        {
            _camera.Position.Y += 1f;
            _camera.Target.Y += 1f;
        }

        private void OnZoomIn(object sender, EventArgs e)
        {
            _camera.Position.Z += 1f;
        }

        private void OnZoomOut(object sender, EventArgs e)
        {
            _camera.Position.Z -= 1f;
        }
        private void OnSetOnOrbit(object sender, EventArgs e)
        {
            _orbiting = !_orbiting;
        }

        private void OnOrbit(object sender, EventArgs e)
        {
            Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(1f));
            _camera.Position = Vector3.Transform(_camera.Position - _camera.Target, Matrix.CreateRotationY(MathHelper.ToRadians(1f))) + _camera.Target;
        }

        public void Update(GameTime gameTime)
        {
            if (_orbiting)
            {
                OnOrbit(null, null);
            }
        }
    }
}
