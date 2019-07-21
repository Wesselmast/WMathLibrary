namespace WMath {
    public class RayCast2D {

        public RayCast2D() {
        }

        public RayCast2D(Vector2 position, Vector2 direction) {
            this.Position = position;
            this.Direction = direction;
            this.Length = float.MaxValue;
        }

        public RayCast2D(Vector2 position, Vector2 direction, float length) {
            this.Position = position;
            this.Direction = direction;
            this.Length = length;
        }

        public Vector2 Position {
            get; set;
        } = Vector2.zero;

        public Vector2 Direction {
            get; set;
        } = Vector2.zero;

        public float Length {
            get; set;
        } = 0.0f;

        public bool IntersectsWith(Circle circle) {
            Vector2 endPoint = Position + (Direction * Length);

            if (Vector2.Distance(circle.Position, Position) <= circle.Radius || 
                Vector2.Distance(circle.Position, endPoint) <= circle.Radius) {
                return true;
            }

            float dot = (circle.Position.x - Position.x) * (endPoint.x - Position.x) + 
                        (circle.Position.y - Position.y) * (endPoint.y - Position.y);

            dot /= (float)WMath.Pow(Length, 2);
            Vector2 closest = new Vector2(Position.x + (dot * (endPoint.x - Position.x)), 
                                          Position.y + (dot * (endPoint.y - Position.y)));

            float d1 = Vector2.Distance(Position, closest);
            float d2 = Vector2.Distance(endPoint, closest);

            if (d1 + d2 <= Length + 0.1 && d1 + d2 >= Length - 0.1f) {
                return Vector2.Distance(circle.Position, closest) <= circle.Radius;
            }
            return false;
        }
    }
}