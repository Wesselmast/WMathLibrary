using UnityEngine;

public class Enemy {
    private const float CHASE_DISTANCE = 250.0f;
    private readonly float moveSpeed = 300.0f;

    private Texture2D visual;

    public WMath.Vector2 Position {
        get { return Circle.Position; }
        set { Circle.Position = value; }
    }

    public WMath.Circle Circle {
        get; private set;
    }

    public float Rotation {
        get; private set;
    }

    public Enemy(WMath.Vector2 position) {
        visual = Resources.Load<Texture2D>("pacman");

        Circle = new WMath.Circle();
        Circle.Radius = visual.width * .5f;

        Position = position;
    }

    public void Render() {
        GUI.matrix = Matrix4x4.identity;
        Matrix4x4 pivot = Matrix4x4.Translate(Position.ToUnity());
        Matrix4x4 rotation = Matrix4x4.Rotate(Quaternion.Euler(0, 0, Rotation));
        GUI.matrix = pivot * rotation * pivot.inverse;

        GUI.color = Color.red;
        GUI.DrawTexture(new Rect(Position.x - Circle.Radius, Position.y - Circle.Radius, visual.width, visual.height), visual);
        GUI.color = Color.white;
    }

    public void Update(Player player) {
        var directionToPlayer = player.Position - Position;
        float distanceToPlayer = directionToPlayer.Magnitude;

        if (distanceToPlayer < CHASE_DISTANCE) {
            float playerFacing = WMath.Vector2.Dot(directionToPlayer.Normalized, player.Direction);

            WMath.Vector2 moveDirection;
            if (playerFacing > .0f) {
                moveDirection = directionToPlayer.Normalized;
            }
            else {
                moveDirection = -directionToPlayer.Normalized;
            }

            Position += moveDirection * moveSpeed * Time.deltaTime;

            Rotation = WMath.WMath.RadToDeg(WMath.Vector2.Angle(new WMath.Vector2(.0f, .0f), moveDirection));
        }
    }
}
