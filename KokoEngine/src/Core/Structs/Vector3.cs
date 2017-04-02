using System;

namespace KokoEngine
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public static Vector3 Up => new Vector3(0, 1, 0);
        public static Vector3 Right => new Vector3(1, 0, 0);
        public static Vector3 Forward => new Vector3(1, 0, 1);
        public static Vector3 One => new Vector3(1, 1, 1);
        public static Vector3 Zero => new Vector3(0, 0, 0);
        public Vector3 Normalized => Normalize(this);
        public float magnitude => Magnitude(this);
        public float sqrMagnitude => SqrMagnitude(this);
        public void Normalize() => Normalize(this);
        public float Dot(Vector3 b) => Dot(this, b);

        public Vector3(float x, float y, float z = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.X, -a.Y, -a.Z);
        }

        public static Vector3 operator *(Vector3 a, float d)
        {
            return new Vector3(a.X * d, a.Y * d, a.Z * d);
        }

        public static Vector3 operator *(float d, Vector3 a)
        {
            return new Vector3(a.X * d, a.Y * d, a.Z * d);
        }

        public static Vector3 operator /(Vector3 a, float d)
        {
            return new Vector3(a.X / d, a.Y / d, a.Z / d);
        }

        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            return SqrMagnitude(lhs - rhs) < 9.99999943962493E-11;
        }

        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return !(lhs == rhs);
        }

        public static float Magnitude(Vector3 a)
        {
            return (float) Math.Sqrt(SqrMagnitude(a));
        }

        public static float SqrMagnitude(Vector3 a)
        {
            return a.X * a.X + a.Y * a.Y + a.Z * a.Z;
        }

        public static Vector3 Normalize(Vector3 a)
        {
            return a / Magnitude(a);
        }

        public static float Dot(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public override string ToString()
        {
            return $"Vector3 ({X}, {Y}, {Z})";
        }
    }
}
