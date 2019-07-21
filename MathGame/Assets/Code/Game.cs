using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Game : MonoBehaviour {
    public static Game Instance {
        get; private set;
    }

    private Player player;

    private List<Enemy> enemies;

    private List<Projectile> projectiles;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        player = new Player();

        enemies = new List<Enemy>();
        for (int i = 0; i < 10;) {
            Enemy enemy = new Enemy(new WMath.Vector2(Random.Range(.0f, Screen.width), Random.Range(.0f, Screen.height)));
            if (!player.AntiSpawnCircle.CollidesWith(enemy.Circle)) {
                i++;
                enemies.Add(enemy);
                continue;
            }
        }

        projectiles = new List<Projectile>();
    }

    private void OnGUI() {
        player?.Render();
        enemies.ForEach(e => e.Render());
        projectiles.ForEach(p => p.Render());

        if (player == null) {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = new Color(Mathf.Sin(Time.time * 2), Mathf.Sin(Time.time * 1.5f), Mathf.Sin(Time.time), WMath.WMath.Lerp(0.5f, 1.0f, Mathf.Sin(Time.time)));
            style.fontSize = 75;
            style.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(Screen.width * .5f - 50.0f, Screen.height * .5f - 75.0f, 100.0f, 100.0f), "YOU LOSE!", style);
        }
    }

    public void CreateProjectile(WMath.Vector2 position, WMath.Vector2 direction, float startVelocity, float acceleration) {
        projectiles.Add(new Projectile(position, direction, startVelocity, acceleration));
    }

    private void Update() {
        if (player == null) return;

        player.Update();

        enemies.ForEach(e => e.Update(player));

        for (int i = projectiles.Count - 1; i >= 0; i--) {
            projectiles[i].Update();
            if (projectiles[i].ShouldDie) {
                projectiles.RemoveAt(i);
            }
        }

        foreach (Enemy e in enemies) {
            if (e.Circle.CollidesWith(player.Circle)) {
                player = null;
                return;
            }
        }

        for (int i = projectiles.Count - 1; i >= 0; i--) {
            for (int j = enemies.Count - 1; j >= 0; --j) {
                if (projectiles[i].Circle.CollidesWith(enemies[j].Circle)) {
                    enemies.RemoveAt(j);
                    projectiles.RemoveAt(i);
                    break;
                }
            }
        }

        player.Intersecting = enemies.Any(e => player.RayCast2D.IntersectsWith(e.Circle));
    }
}
