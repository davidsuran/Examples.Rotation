using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CameraControllerDemo
{
    public static class Vector3Extension
    { 
        public static Vector3 Normalized(this Vector3 v)
        {
            float num = MathF.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
            num = 1f / num;
            v.X *= num;
            v.Y *= num;
            v.Z *= num;

            return v;
        }
    }
}
