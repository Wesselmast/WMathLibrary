using System;

namespace WMath {
    public struct Vector3 {
        public float x;
        public float y;
        public float z;

        public static Vector3 zero => new Vector3(0, 0, 0);
        public static Vector3 one => new Vector3(1, 1, 1);

        public float Magnitude {
            get {
                return (float)WMath.Sqrt(WMath.Pow(x, 2) + WMath.Pow(y, 2) + WMath.Pow(z, 2));
            }
        }

        public Vector3 Normalized {
            get {
                float magnitude = Magnitude;
                return magnitude == 0 ? zero : this / magnitude;
            }
        }

        public Vector3(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static implicit operator Vector3(Vector2 v) {
            return new Vector3(v.x, v.y, 1.0f);
        }

        public static float Dot(Vector3 lhs, Vector3 rhs) {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }

        public static Vector3 Cross(Vector3 lhs, Vector3 rhs) {
            float x = lhs.y * rhs.z - lhs.z * rhs.y;
            float y = lhs.z * rhs.x - lhs.x * rhs.z;
            float z = lhs.x * rhs.y - lhs.y * rhs.x;
            return new Vector3(x, y, z);
        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t) {
            return a * t + (b * (1 - t));
        }

        public static float Angle(Vector3 lhs, Vector3 rhs) {
            return WMath.RadToDeg((float)Math.Acos(WMath.Clamp(Dot(lhs, rhs) / (lhs.Magnitude * rhs.Magnitude), -1.0f, 1.0f)));
        }

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs) {
            return new Vector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs) {
            return new Vector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }

        public static Vector3 operator -(Vector3 v) {
            return new Vector3(-v.x, -v.y, -v.z);
        }

        public static Vector3 operator *(Vector3 lhs, float scalar) {
            return new Vector3(lhs.x * scalar, lhs.y * scalar, lhs.z * scalar);
        }

        public static Vector3 operator /(Vector3 lhs, float scalar) {
            return new Vector3(lhs.x / scalar, lhs.y / scalar, lhs.z / scalar);
        }
    }
}
