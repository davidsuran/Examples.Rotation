using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraControllerDemo
{
    public class KeyBinder
    {
        public enum InputEventScheme
        {
            Menu,
            PlayerController
        }

        public enum InputEventType
        {
            None,
            MoveLeft,
            MoveRight,
            MoveForward,
            MoveBackward,
            ZoomIn,
            ZoomOut,
            Orbit
        }

        //public static void TriggerKey(Keys key)
        //{
        //    InputEventType inputEventType = Mapper(key);
        //    if (inputEventType != InputEventType.None)
        //    { 
            
        //    }
        //}

        public static InputEventType Mapper(Keys key) => key switch
        {
            Keys.Left => InputEventType.MoveLeft,
            Keys.Right => InputEventType.MoveRight,
            Keys.Up => InputEventType.MoveForward,
            Keys.Down => InputEventType.MoveBackward,
            Keys.OemPlus => InputEventType.ZoomIn,
            Keys.OemMinus => InputEventType.ZoomOut,
            Keys.Space => InputEventType.Orbit,
            _ => InputEventType.None,
        };
    }
}
