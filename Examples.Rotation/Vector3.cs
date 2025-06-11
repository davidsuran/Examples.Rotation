using Microsoft.Xna.Framework;
using System;

namespace CameraControllerDemo
{
    public class Vector3
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

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator *(Vector3 a, float b)
        {
            return new Vector3(a.X * b, a.Y * b, a.Z * b);
        }

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

        public Vector3() : this(Microsoft.Xna.Framework.Vector3.Zero)
        {
        }

        internal static Vector3 Transform(Vector3 position, Matrix rotationMatrix)
        {
            return (Vector3)Microsoft.Xna.Framework.Vector3.Transform(position.Struct, rotationMatrix);
        }

        internal static float Distance(Vector3 a, Vector3 b)
        {
            return Microsoft.Xna.Framework.Vector3.Distance(a.Struct, b.Struct);
        }

        internal void Normalize()
        {
            float num = MathF.Sqrt(X * X + Y * Y + Z * Z);
            num = 1f / num;
            X *= num;
            Y *= num;
            Z *= num;
        }

        public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
        {
            Cross(ref vector1, ref vector2, out vector1);
            return vector1;
        }

        public static void Cross(ref Vector3 vector1, ref Vector3 vector2, out Vector3 result)
        {
            float x = vector1.Y * vector2.Z - vector2.Y * vector1.Z;
            float y = 0f - (vector1.X * vector2.Z - vector2.X * vector1.Z);
            float z = vector1.X * vector2.Y - vector2.X * vector1.Y;
            
            result = new Vector3();
            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        public static float Dot(Vector3 value1, Vector3 value2)
        {
            return value1.X * value2.X + value1.Y * value2.Y + value1.Z * value2.Z;
        }

        public static void Dot(ref Vector3 value1, ref Vector3 value2, out float result)
        {
            result = value1.X * value2.X + value1.Y * value2.Y + value1.Z * value2.Z;
        }

        public override string ToString()
        {
            return base.ToString(); 
        }
    }
}
