using CameraControllerDemo.Models;
using Microsoft.Xna.Framework;
using System;

namespace CameraControllerDemo.Controllers
{
    internal class CameraController : IController
    {
        private CameraModel _cameraModel;
        private bool _orbiting;

        public Vector3 Target => Vector3.Zero;// _camera.Target;
        public Vector3 Position => _cameraModel.Position;

        public CameraController(Vector3 position, Vector3 target)
        {
            _cameraModel = new CameraModel(position, target);
            //EventDispatcher.OnMoveLeft += OnMoveLeft;
            //EventDispatcher.OnMoveRight += OnMoveRight;
            //EventDispatcher.OnMoveForward += OnMoveUp;
            //EventDispatcher.OnMoveBackward += OnMoveDown;
            EventDispatcher.OnZoomIn += OnZoomIn;
            EventDispatcher.OnZoomOut += OnZoomOut;
            EventDispatcher.OnOrbit += OnSetOnOrbit;
        }

        public Matrix GetViewMatrix()
        {
            return Matrix.CreateLookAt(_cameraModel.Position, _cameraModel.Target, Vector3.Up);
        }

        public Matrix GetWorldMatrix()
        {
            return Matrix.CreateWorld(_cameraModel.Target, Vector3.Forward, Vector3.Up);
        }

        public void Orbit()
        {
            //OnOrbit(null, null);
        }

        private void OnMoveLeft(object sender, EventArgs e)
        {
            _cameraModel.Position.X -= 1f;
            _cameraModel.Target.X -= 1f;
        }

        private void OnMoveRight(object sender, EventArgs e)
        {
            _cameraModel.Position.X += 1f;
            _cameraModel.Target.X += 1f;
        }

        private void OnMoveUp(object sender, EventArgs e)
        {
            _cameraModel.Position.Y -= 1f;
            _cameraModel.Target.Y -= 1f;
        }

        private void OnMoveDown(object sender, EventArgs e)
        {
            _cameraModel.Position.Y += 1f;
            _cameraModel.Target.Y += 1f;
        }

        private void OnZoomIn(object sender, EventArgs e)
        {
            _cameraModel.Position.Z += 1f;
        }

        private void OnZoomOut(object sender, EventArgs e)
        {
            _cameraModel.Position.Z -= 1f;
        }
        private void OnSetOnOrbit(object sender, EventArgs e)
        {
            _orbiting = !_orbiting;
        }

        private void OnOrbit(GameTime gameTime, Vector3 target)
        {
            Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(1f));
            _cameraModel.Position = Vector3.Transform(_cameraModel.Position - target, Matrix.CreateRotationY(MathHelper.ToRadians(1f))) + target;
        }

        public void Update(GameTime gameTime, Vector3 target)
        {
            target = MoveToTarget(target);

            if (_orbiting)
            {
                OnOrbit(gameTime, target);
            }
        }

        internal Vector3 MoveToTarget(Vector3 target)
        {
            _cameraModel.Target = target;
            var targetPos = _cameraModel.Target + _cameraModel.PositionSettings.TargetPositionOffset;
            //var destination = Quaternion.

            //destination = Quaternion.FromEuler(new Vector3(orbitSettings.xRotation, orbitSettings.yRotation + Target.Rotation.Y, 0)).Normalized() * -Vector3.Forward * posSettings.distanceFromTarget;

            //destination += targetPos;
            //Position = destination;
            _cameraModel.Position = targetPos;
            return targetPos;
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        //private void MoveToTarget()
        //{
        //    targetPos = Target.GlobalPosition + posSettings.targetPosOffset;
        //    destination = Quaternion.FromEuler(new Vector3(orbitSettings.xRotation, orbitSettings.yRotation + Target.Rotation.Y, 0)).Normalized() * -Vector3.Forward * posSettings.distanceFromTarget;

        //    destination += targetPos;
        //    Position = destination;
        //}

        //private void LookAtTarget(double delta)
        //{
        //    LookAt(Target.GlobalPosition);
        //}
    }
}
