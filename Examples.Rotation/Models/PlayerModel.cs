
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CameraControllerDemo.Models
{
    internal class PlayerModel : ActorModel
    {
        public PlayerModel(Vector3 position, Vector3 target) : base(position, target)
        {
            Position = new Vector3(4, 6, 1);
        }

        public IEnumerable<VertexPositionColor> GetModel3d()
        {
            return Primitives.MakeBox(Position, Color.Pink);
        }
    }
}
