namespace WMath {
    public class Rigidbody {
        public Vector2 Velocity {
            get; private set;
        } = Vector2.zero;

        public float mass = 1.0f;
        public float force = 150.0f;
        public float dragCoefficient = .47f;

        public void AddForce(Vector2 forceDirection, float deltaTime) {
            Vector2 acceleration = forceDirection * force / mass * deltaTime;
            Velocity = (1 / dragCoefficient) * (float)WMath.Exp(-dragCoefficient / mass * deltaTime) * 
                       (dragCoefficient * Velocity + mass * acceleration) - (mass * acceleration / dragCoefficient);
        }
    }
}
