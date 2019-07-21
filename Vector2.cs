using System;

namespace WMath {
    public struct Vector2 {
        public float x;
        public float y;

        public static Vector2 zero => new Vector2(0, 0);
        public static Vector2 one => new Vector2(1, 1);

        public float Magnitude {
            get {
                return (float)WMath.Sqrt(WMath.Pow(x, 2) + WMath.Pow(y, 2));
            }
        }

        public Vector2 Normalized {
            get {
                float magnitude = Magnitude;
                return magnitude == 0 ? zero : this / Magnitude;
            }
        }

        public Vector2(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public static float Dot(Vector2 lhs, Vector2 rhs) {
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float p) {
            return a * p + (b * (1 - p));
        }

        public static float Angle(Vector2 lhs, Vector2 rhs) {
            return (float)Math.Atan2(rhs.y - lhs.y, rhs.x - lhs.x);
        }

        public static float Distance(Vector2 lhs, Vector2 rhs) {
            return (float)WMath.Sqrt(WMath.Pow(lhs.x - rhs.x, 2) + WMath.Pow(lhs.y - rhs.y, 2));
        }

        public static Vector2 DirectionFromAngle(float angle) {
            float x = (float)Math.Cos(WMath.DegToRad(angle));
            float y = (float)Math.Sin(WMath.DegToRad(angle));
            return new Vector2(x, y);
        }

        public static Vector2 operator +(Vector2 lhs, Vector2 rhs) {
            return new Vector2(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        public static Vector2 operator -(Vector2 lhs, Vector2 rhs) {
            return new Vector2(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        public static Vector2 operator -(Vector2 v) {
            return new Vector2(-v.x, -v.y);
        }

        public static Vector2 operator *(Vector2 lhs, float scalar) {
            return new Vector2(lhs.x * scalar, lhs.y * scalar);
        }

        public static Vector2 operator *(float scalar, Vector2 lhs) {
            return new Vector2(lhs.x * scalar, lhs.y * scalar);
        }

        public static Vector2 operator /(Vector2 lhs, float scalar) {
            return new Vector2(lhs.x / scalar, lhs.y / scalar);
        }

        public override string ToString() {
            return x.ToString() + ", " + y.ToString();
        }
    }
}
