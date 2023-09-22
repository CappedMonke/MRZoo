using UnityEngine;

public static class Utilities
{
    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angles)
    {
        var direction = point - pivot;
        direction = angles * direction;
        point = direction + pivot;
        return point;
    }
}
