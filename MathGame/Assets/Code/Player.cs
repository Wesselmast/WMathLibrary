using UnityEngine;

public class Player {
    private Texture2D visual;
    public WMath.Vector2 Position {
        get { return Circle.Position; }
        set { Circle.Position = value; }
    }

    public WMath.Vector2 Direction => WMath.Vector2.DirectionFromAngle(Rotation);

    public bool Intersecting {
        get {
            return false;
        }
        set {
            if (value) {
                pixel.SetPixel(0, 0, Color.red);
            }
            else pixel.SetPixel(0, 0, Color.white);
            pixel.Apply();
        }
    }

    public WMath.RayCast2D RayCast2D {
        get; private set;
    } = new WMath.RayCast2D(WMath.Vector2.zero, WMath.Vector2.zero, 250);

    public WMath.Circle Circle {
        get; private set;
    } = new WMath.Circle();

    public WMath.Circle AntiSpawnCircle {
        get; private set;
    } = new WMath.Circle();

    private Texture2D pixel;

    public float Rotation {
        get; private set;
    }

    private readonly float moveSpeed = 500.0f;

    private const float maxChargeTime = 1.0f;
    private const float minProjectileStartVelocity = .0f;
    private const float maxProjectileStartVelocity = 10.0f;
    private const float minProjectileStartAcceleration = 10.0f;
    private const float maxProjectileStartAcceleration = 20.0f;

    private float chargeTime;

    private WMath.Rigidbody rigidbody;

    public Player() {
        visual = Resources.Load<Texture2D>("pacman");
        Circle.Radius = visual.width * .5f;
        AntiSpawnCircle.Position = Position;
        AntiSpawnCircle.Radius = Screen.width / 2;

        pixel = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        Position = new WMath.Vector2(Screen.width * .5f - visual.width * .5f, Screen.height * .5f - visual.height * .5f);

        rigidbody = new WMath.Rigidbody() {
            mass = 1.0f,
            force = 150.0f,
            dragCoefficient = .47f
        };
    }

    public void Render() {
        GUI.matrix = Matrix4x4.identity;
        Matrix4x4 pivot = Matrix4x4.Translate(Position.ToUnity());
        Matrix4x4 rotation = Matrix4x4.Rotate(Quaternion.Euler(0, 0, Rotation));
        GUI.matrix = pivot * rotation * pivot.inverse;

        GUI.DrawTexture(new Rect(Position.x - Circle.Radius, Position.y - Circle.Radius, visual.width, visual.height), visual);

        float p = WMath.WMath.Clamp(chargeTime, .0f, maxChargeTime) / maxChargeTime;
        float fireVelocity = WMath.WMath.Lerp(minProjectileStartVelocity, maxProjectileStartVelocity, p);
        float fireAcceleration = WMath.WMath.Lerp(minProjectileStartAcceleration, maxProjectileStartAcceleration, p);

        float distanceTraveled = WMath.WMath.DistanceTraveled(fireVelocity, fireAcceleration, Projectile.LIFETIME);

        RayCast2D.Position = Position;
        RayCast2D.Direction = WMath.Vector2.DirectionFromAngle(Rotation);
        RayCast2D.Length = distanceTraveled;

        GUI.DrawTexture(new Rect(RayCast2D.Position.x, RayCast2D.Position.y, distanceTraveled, 2.0f), pixel);
    }

    private void UpdatePhysics() {
        WMath.Vector2 forceDirection = new WMath.Vector2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidbody.AddForce(forceDirection, Time.deltaTime);

        Position += rigidbody.Velocity;
    }

    public void Update() {
        UpdatePhysics();

        var mousePos = new WMath.Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        var mouseDir = (mousePos - Position).Normalized;

        Rotation = WMath.WMath.RadToDeg(WMath.Vector2.Angle(new WMath.Vector2(.0f, .0f), mouseDir));

        if (Input.GetKey(KeyCode.Mouse0)) {
            chargeTime += Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)) {
            float p = WMath.WMath.Clamp(chargeTime, .0f, maxChargeTime) / maxChargeTime;
            Game.Instance.CreateProjectile(Position, Direction, WMath.WMath.Lerp(minProjectileStartVelocity, maxProjectileStartVelocity, p), 
                                                                WMath.WMath.Lerp(minProjectileStartAcceleration, maxProjectileStartAcceleration, p));

            chargeTime = .0f;
        }
    }
}
