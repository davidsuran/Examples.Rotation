using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraControllerDemo
{
    internal class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Microsoft.Xna.Framework.Vector3 Struct => new Microsoft.Xna.Framework.Vector3(X, Y, Z);

        public static Microsoft.Xna.Framework.Vector3 Forward => Microsoft.Xna.Framework.Vector3.Forward;

        //public static Vector3Struct Zero => Vector3Struct.Zero;

        public static Microsoft.Xna.Framework.Vector3 Up => Microsoft.Xna.Framework.Vector3.Up;

        public static Vector3 Zero => new Vector3(0f, 0f, 0f);

        public static Microsoft.Xna.Framework.Vector3 UnitZ => Microsoft.Xna.Framework.Vector3.UnitZ;


        public static implicit operator Microsoft.Xna.Framework.Vector3(Vector3 v)
        {
            return v.Struct;
        }

        public static explicit operator Vector3(Microsoft.Xna.Framework.Vector3 v)
        {
            return new Vector3(v);
        }

        public Vector3(Microsoft.Xna.Framework.Vector3 vector3) : this(vector3.X, vector3.Y, vector3.Z)
        {
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        internal static Vector3 Transform(Vector3 position, Matrix rotationMatrix)
        {
            return (Vector3)Microsoft.Xna.Framework.Vector3.Transform(position.Struct, rotationMatrix);
        }
    }

}
