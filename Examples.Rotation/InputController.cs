using CameraControllerDemo;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static CameraControllerDemo.KeyBinder;

namespace Examples.Rotation
{
    public class InputController : IController
    {
        public static bool LeftClicked = false;

        private static MouseState ms = new MouseState(), oms;

        private static Dictionary<InputEventType, EventHandler<EventArgs>> _eventsDictionary;

        private static InputController _instance;
        public static InputController Instance
        {
            get
            {
                _instance ??= new InputController();
                return _instance;
            }
            private set => _instance = value;
        }


        public void Update(GameTime gameTime)
        {
            oms = ms;
            ms = Mouse.GetState();
            LeftClicked = ms.LeftButton != ButtonState.Pressed && oms.LeftButton == ButtonState.Pressed;
            // true On left release like Windows buttons


            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] pressedKeys = currentKeyboardState.GetPressedKeys();
            HandlePressedKeys(pressedKeys);
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

        private static void HandlePressedKeys(Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                KeyBinder.InputEventType inputEventType = KeyBinder.Mapper(key);

                EventDispatcher.Instance.Raise(inputEventType);
                //EventHandler<EventArgs> eventHandler = _eventsDictionary[inputEventType];
                //if (eventHandler != null)
                //{
                //    eventHandler?.Invoke(inputEventType, null);
                //}
            }
        }


    }
}
