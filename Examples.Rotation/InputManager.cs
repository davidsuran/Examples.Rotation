using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Examples.Rotation
{
    internal static class InputManager
    {
        public static bool LeftClicked = false;

        private static MouseState ms = new MouseState(), oms;

        public static void Update()
        {
            oms = ms;
            ms = Mouse.GetState();
            LeftClicked = ms.LeftButton != ButtonState.Pressed && oms.LeftButton == ButtonState.Pressed;
            // true On left release like Windows buttons
        }

        public static bool Hover(Rectangle r)
        {
            return r.Contains(new Vector2(ms.X, ms.Y));
        }

        public static Vector2 LastMouseCoordsNormalized(Rectangle r)
        {
            // Top Left is 0, 0 Bottom Right is N+, N+
            // Convert to -1, 1 and -1, 1
            // between a and b [a + (((x-min)*(b-a))/(max - min))]

            int xMin = r.Left;
            int xMax = r.Right;
            int yMin = r.Top;
            int yMax = r.Bottom;
            int a = -1;
            int b = 1;

            float Rescale(int mouse, int min, int max)
            {
                return a + (((mouse - min) * (b - a)) / (float)(max - min));
            }

            return new Vector2(Rescale(ms.X, xMin, xMax), Rescale(ms.Y, yMin, yMax));
        }
    }
}
