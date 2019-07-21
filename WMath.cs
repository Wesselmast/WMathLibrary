namespace WMath {
    public partial class WMath {
        public static float Lerp(float a, float b, float t) {
            return a * t + (b * (1 - t));
        }

        public static float DistanceTraveled(float startVelocity, float acceleration, float time) {
            return (startVelocity * time) + (0.5f * acceleration * time * time);
        }

        public static float Clamp(float n, float lower, float upper) {
            return n < lower ? lower : (n > upper ? upper : n);
        }

        public static float RadToDeg(float angle) {
            return (float)(angle * (180 / WMath.PI));
        }

        public static float DegToRad(float angle) {
            return (float)(angle * (WMath.PI / 180));
        }
    }
}
