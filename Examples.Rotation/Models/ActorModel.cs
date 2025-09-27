
namespace CameraControllerDemo.Models
{
    public abstract class ActorModel
    {
        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }

        public ActorModel(Vector3 position, Vector3 target)
        {
            Position = position;
            Target = target;
        }
    }
}
