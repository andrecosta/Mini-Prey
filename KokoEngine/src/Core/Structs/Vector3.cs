using System;

namespace KokoEngine
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        #region Static Properties
        public static Vector3 Up => new Vector3(0, 1, 0);
        public static Vector3 Right => new Vector3(1, 0, 0);
        public static Vector3 Forward => new Vector3(1, 0, 1);
        public static Vector3 One => new Vector3(1, 1, 1);
        public static Vector3 Zero => new Vector3(0, 0, 0);
        #endregion

        #region Instance Properties
        public float Magnitude => (float)Math.Sqrt(SqrMagnitude);
        public Vector3 Normalized => Normalize(this);
        public float SqrMagnitude => X * X + Y * Y + Z * Z;
        #endregion

        #region Static Methods
        public static float Dot(Vector3 a, Vector3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        public static float Distance(Vector3 a, Vector3 b) => (a - b).Magnitude;
        public static Vector3 Normalize(Vector3 a) => a / a.Magnitude;
        #endregion

        #region Operators
        public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Vector3 operator -(Vector3 a) => new Vector3(-a.X, -a.Y, -a.Z);
        public static Vector3 operator *(Vector3 a, float d) => new Vector3(a.X * d, a.Y * d, a.Z * d);
        public static Vector3 operator *(float d, Vector3 a) => new Vector3(a.X * d, a.Y * d, a.Z * d);
        public static Vector3 operator /(Vector3 a, float d) => new Vector3(a.X / d, a.Y / d, a.Z / d);
        public static bool operator ==(Vector3 a, Vector3 b) => (a - b).SqrMagnitude < 9.9E-11;
        public static bool operator !=(Vector3 a, Vector3 b) => !(a == b);
        public static implicit operator Vector3(Vector2 v) => new Vector3(v.X, v.Y);
        #endregion

        #region Overrides
        public override string ToString() => $"Vector3 ({X}, {Y}, {Z})";
        #endregion
    }
}
