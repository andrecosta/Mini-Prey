using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KokoEngine
{
    public struct Vector2
    {
        public float X;
        public float Y;

        public static Vector2 Up => new Vector2(0, 1);
        public static Vector2 Down => new Vector2(0, -1);
        public static Vector2 Left => new Vector2(-1, 0);
        public static Vector2 Right => new Vector2(1, 0);
        public static Vector2 One => new Vector2(1, 1);
        public static Vector2 Zero => new Vector2(0, 0);
        public Vector2 Normalized => Normalize(this);

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.X, -a.Y);
        }

        public static Vector2 operator *(Vector2 a, float d)
        {
            return new Vector2(a.X * d, a.Y * d);
        }

        public static Vector2 operator *(float d, Vector2 a)
        {
            return new Vector2(a.X * d, a.Y * d);
        }

        public static Vector2 operator /(Vector2 a, float d)
        {
            return new Vector2(a.X / d, a.Y / d);
        }

        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return SqrMagnitude(lhs - rhs) < 9.9E-11;
        }

        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return !(lhs == rhs);
        }

        public static float Magnitude(Vector2 a)
        {
            return (float)Math.Sqrt(SqrMagnitude(a));
        }

        public static float SqrMagnitude(Vector2 a)
        {
            return a.X * a.X + a.Y * a.Y;
        }

        public static Vector2 Normalize(Vector2 a)
        {
            return a / Magnitude(a);
        }




    }
}

