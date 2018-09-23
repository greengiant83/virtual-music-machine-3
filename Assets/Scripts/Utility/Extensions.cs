using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2, bool clamp = false)
    {
        var v = (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        if (clamp) v = Mathf.Clamp(v, from2, to2);
        return v;
    }

    public static float Remap(this int value, float from1, float to1, float from2, float to2, bool clamp = false)
    {
        return ((float)value).Remap(from1, to1, from2, to2, clamp);
    }

    public static bool IsPointInBounds(this Transform transform, Vector3 point, float boxSize = 1)
    {
        float halfSize = boxSize / 2;
        point = transform.InverseTransformPoint(point);

        float halfX = halfSize;
        float halfY = halfSize;
        float halfZ = halfSize;

        if (point.x < halfX && point.x > -halfX &&
           point.y < halfY && point.y > -halfY &&
           point.z < halfZ && point.z > -halfZ)
            return true;
        else
            return false;
    }

}