using System;
using System.Collections.Generic;
using CameraControllerDemo.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CameraControllerDemo.Controllers
{
    internal class PlayerController : IController
    {
        private PlayerModel _model;

        public Vector3 Target => _model.Target;
        public Vector3 Position => _model.Position;
        public IEnumerable<VertexPositionColor> GetModel3d => _model.GetModel3d();

        public PlayerController(Vector3 position, Vector3 target)
        {
            _model = new PlayerModel(position, target);
            EventDispatcher.OnMoveLeft += OnMoveLeft;
            EventDispatcher.OnMoveRight += OnMoveRight;
            EventDispatcher.OnMoveForward += OnMoveUp;
            EventDispatcher.OnMoveBackward += OnMoveDown;
        }

        private void OnMoveLeft(object sender, EventArgs e)
        {
            _model.Position.X -= 1.5f;
        }

        private void OnMoveRight(object sender, EventArgs e)
        {
            _model.Position.X += 1.5f;
        }

        private void OnMoveUp(object sender, EventArgs e)
        {
            _model.Position.Y -= 1.5f;
        }

        private void OnMoveDown(object sender, EventArgs e)
        {
            _model.Position.Y += 1.5f;
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
