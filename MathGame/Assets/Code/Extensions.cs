using System;
using UnityEngine;

public static class Extensions {
    public static Vector2 ToUnity(this WMath.Vector2 v) {
        return new Vector2(v.x, v.y);
    }

    public static WMath.Vector2 ToWMath(this Vector2 v) {
        return new WMath.Vector2(v.x, v.y);
    }
}
