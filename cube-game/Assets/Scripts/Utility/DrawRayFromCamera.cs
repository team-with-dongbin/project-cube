using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRayFromCamera : MonoBehaviour
{
    // Start is called before the first frame update
    Transform _camera;
    void Start()
    {
        _camera = CameraController.instance.firstViewCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(_camera.position, _camera.position + _camera.forward * 100f, Color.red);
    }
}
