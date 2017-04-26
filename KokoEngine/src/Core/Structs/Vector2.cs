using System;

namespace KokoEngine
{
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        #region Static Properties
        public static Vector2 Up => new Vector2(0, 1);
        public static Vector2 Down => new Vector2(0, -1);
        public static Vector2 Left => new Vector2(-1, 0);
        public static Vector2 Right => new Vector2(1, 0);
        public static Vector2 One => new Vector2(1, 1);
        public static Vector2 Zero => new Vector2(0, 0);
        #endregion

        #region Instance Properties
        public float Magnitude => (float)Math.Sqrt(SqrMagnitude);
        public Vector2 Normalized => Normalize(this);
        public float SqrMagnitude => X * X + Y * Y;
        #endregion

        #region Static Methods
        public static float Dot(Vector2 a, Vector2 b) => a.X * b.X + a.Y * b.Y;
        public static float Distance(Vector2 a, Vector2 b) => (a - b).Magnitude;
        public static Vector2 Normalize(Vector2 a) => a / a.Magnitude;
        #endregion
        
        #region Operators
        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator -(Vector2 a) => new Vector2(-a.X, -a.Y);
        public static Vector2 operator *(Vector2 a, float d) => new Vector2(a.X * d, a.Y * d);
        public static Vector2 operator *(float d, Vector2 a) => new Vector2(a.X * d, a.Y * d);
        public static Vector2 operator /(Vector2 a, float d) => new Vector2(a.X / d, a.Y / d);
        public static bool operator ==(Vector2 a, Vector2 b) => (a - b).SqrMagnitude < 9.9E-11;
        public static bool operator !=(Vector2 a, Vector2 b) => !(a == b);
        public static implicit operator Vector2(Vector3 v) => new Vector2(v.X, v.Y);
        #endregion

        #region Overrides
        public override string ToString() => $"Vector2 ({X}, {Y})";
        #endregion
    }
}

