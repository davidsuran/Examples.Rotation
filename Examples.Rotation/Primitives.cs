using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CameraControllerDemo
{
    internal class Primitives
    {
        /// <summary>
        /// Makes the floor.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<VertexPositionColor> MakeFloor()
        {
            yield return MakeVertex(0, 0, 300, Color.Red);
            yield return MakeVertex(0, 0, 0, Color.Green);
            yield return MakeVertex(300, 0, 0, Color.Blue);

            yield return MakeVertex(300, 0, 0, Color.Yellow);
            yield return MakeVertex(0, 0, 0, Color.Wheat);
            yield return MakeVertex(0, 0, -300, Color.WhiteSmoke);

            yield return MakeVertex(0, 0, -300, Color.Orange);
            yield return MakeVertex(0, 0, 0, Color.Orchid);
            yield return MakeVertex(-300, 0, 0, Color.Olive);

            yield return MakeVertex(-300, 0, 0, Color.Salmon);
            yield return MakeVertex(0, 0, 0, Color.Sienna);
            yield return MakeVertex(0, 0, 300, Color.Aqua);
        }

        /// <summary>
        /// Makes the box.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<VertexPositionColor> MakeBox()
        {
            // Front back
            yield return MakeVertex(-30, 0, -30);
            yield return MakeVertex(30, 0, -30);
            yield return MakeVertex(30, 60, -30);

            yield return MakeVertex(-30, 60, -30);
            yield return MakeVertex(30, 60, -30);
            yield return MakeVertex(-30, 0, -30);

            // Box back
            yield return MakeVertex(-30, 0, 30);
            yield return MakeVertex(30, 0, 30);
            yield return MakeVertex(30, 60, 30);

            yield return MakeVertex(-30, 60, 30);
            yield return MakeVertex(30, 60, 30);
            yield return MakeVertex(-30, 0, 30);
        }

        /// <summary>
        /// Makes the camera target.
        /// </summary>
        /// <param name="cameraTarget">The camera target.</param>
        /// <returns></returns>
        public static IEnumerable<VertexPositionColor> MakeCameraTarget(Vector3 cameraTarget)
        {
            float height = 600f;
            // Front back
            yield return MakeVertex(cameraTarget.X - 10, -height, cameraTarget.Z, Color.Red);
            yield return MakeVertex(cameraTarget.X + 10, -height, cameraTarget.Z, Color.Red);
            yield return MakeVertex(cameraTarget.X + 10, height, cameraTarget.Z, Color.Red);

            yield return MakeVertex(cameraTarget.X - 10, height, cameraTarget.Z, Color.Red);
            yield return MakeVertex(cameraTarget.X + 10, height, cameraTarget.Z, Color.Red);
            yield return MakeVertex(cameraTarget.X - 10, -height, cameraTarget.Z, Color.Red);

            // Box back
            yield return MakeVertex(cameraTarget.X, -height, cameraTarget.Z + 10, Color.BlueViolet);
            yield return MakeVertex(cameraTarget.X, -height, cameraTarget.Z - 10, Color.BlueViolet);
            yield return MakeVertex(cameraTarget.X, height, cameraTarget.Z - 10, Color.BlueViolet);

            yield return MakeVertex(cameraTarget.X, height, cameraTarget.Z - 10, Color.BlueViolet);
            yield return MakeVertex(cameraTarget.X, height, cameraTarget.Z + 10, Color.BlueViolet);
            yield return MakeVertex(cameraTarget.X, -height, cameraTarget.Z + 10, Color.BlueViolet);
        }

        /// <summary>
        /// Makes the vertex.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public static VertexPositionColor MakeVertex(float x, float y, float z, Color? color = null)
        {
            return new VertexPositionColor(new Vector3(x, y, z), color.HasValue ? color.Value : Color.Gray);
        }
    }
}
