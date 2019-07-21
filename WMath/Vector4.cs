using System;

namespace WMath {
    public struct Vector4 {
        public float x;
        public float y;
        public float z;
        public float w;

        public static Vector4 zero => new Vector4(0, 0, 0, 0);
        public static Vector4 one => new Vector4(1, 1, 1, 1);

        public float Magnitude {
            get {
                return (float)WMath.Sqrt(WMath.Pow(x, 2) + WMath.Pow(y, 2) + WMath.Pow(z, 2) + WMath.Pow(w, 2));
            }
        }

        public Vector4 Normalized {
            get {
                float magnitude = Magnitude;
                return magnitude == 0 ? zero : this / magnitude;
            }
        }

        public Vector4(float x, float y, float z, float w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static implicit operator Vector4(Vector3 v) {
            return new Vector4(v.x, v.y, v.z, 1.0f);
        }

        public static float Dot(Vector4 lhs, Vector4 rhs) {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z + lhs.w * rhs.w;
        }

        public static Vector4 Lerp(Vector4 a, Vector4 b, float t) {
            return a * t + (b * (1 - t));
        }

        public static float Angle(Vector4 lhs, Vector4 rhs) {
            return WMath.RadToDeg((float)Math.Acos(WMath.Clamp(Dot(lhs, rhs) / (lhs.Magnitude * rhs.Magnitude), -1.0f, 1.0f)));
        }

        public static Vector4 operator +(Vector4 lhs, Vector4 rhs) {
            return new Vector4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
        }

        public static Vector4 operator -(Vector4 lhs, Vector4 rhs) {
            return new Vector4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
        }

        public static Vector4 operator -(Vector4 v) {
            return new Vector4(-v.x, -v.y, -v.z, -v.w);
        }

        public static Vector4 operator *(Vector4 lhs, float scalar) {
            return new Vector4(lhs.x * scalar, lhs.y * scalar, lhs.z * scalar, lhs.w * scalar);
        }

        public static Vector4 operator /(Vector4 lhs, float scalar) {
            return new Vector4(lhs.x / scalar, lhs.y / scalar, lhs.z / scalar, lhs.w / scalar);
        }
    }
}
