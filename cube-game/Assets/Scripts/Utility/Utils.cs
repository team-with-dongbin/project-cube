using UnityEngine;
using System;

public static class Utils
{
    const int FieldLayer = 0;
    public static bool IsGrounded(Vector3 position)
    {
        var ray = new Ray(position + Vector3.up * 0.1f, Vector3.down);
        const float maxDistance = 0.2f;
        return Physics.Raycast(ray, maxDistance);
    }

    public static bool IsGrounded(Vector3 position, int layerMask = 1 << FieldLayer)
    {
        var ray = new Ray(position + Vector3.up * 0.1f, Vector3.down);
        const float maxDistance = 0.2f;
        return Physics.Raycast(ray, maxDistance, layerMask);
    }

    public static T RandomEnumValue<T>()
    {
        var values = Enum.GetValues(typeof(T));
        int random = UnityEngine.Random.Range(0, values.Length);
        return (T)values.GetValue(random);
    }

    public static Transform GetFirstViewCameraTransform()
    {
        return CameraController.instance.firstViewCamera.transform;
    }
}
