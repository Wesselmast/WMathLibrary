namespace WMath {
    public class Circle {

        public Circle() {
        }

        public Circle(Vector2 position, float radius) {
            this.Position = position;
            this.Radius = radius;
        }

        public Vector2 Position {
            get; set;
        } = Vector2.zero;

        public float Radius {
            get; set;
        } = 0;
        
        public bool CollidesWith(Circle circle) {
            return WMath.Sqrt(WMath.Pow(circle.Position.x - Position.x, 2)  +
                              WMath.Pow(circle.Position.y - Position.y, 2)) - circle.Radius - Radius < 0;
        }
    }
}
