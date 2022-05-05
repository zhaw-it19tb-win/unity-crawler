using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers {
  private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

  public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);


  public static float GetIsoDistance(Vector2 from, Vector2 to) {
    return Mathf.Sqrt(Mathf.Pow((from.x - to.x) / 2, 2) + Mathf.Pow((from.y - to.y), 2));
  }

  /*
  public static Vector2 LimitToRange(Vector2 from, Vector2 to, float maxRange) {
    var range = GetIsoDistance(from, to);
    if (range > maxRange) {
      var target = Vector2.MoveTowards(from, to, maxRange);
      var test = target.normalized * target.magnitude
    }
    return to;
  }*/
  public static Vector2 ToIso(this Vector2 v) {
    return Quaternion.Euler(0, 0, 45) * v;
  }

  public static Vector2 Rotate(this Vector2 vector, float degrees) {
    float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
    float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

    float vectorX = vector.x;
    float vectorY = vector.y;

    vector.x = (cos * vectorX) - (sin * vectorY);
    vector.y = (sin * vectorX) + (cos * vectorY);

    return vector;
  }

}
