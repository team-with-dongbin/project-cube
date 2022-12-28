using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRayFromCamera : MonoBehaviour
{
    // Start is called before the first frame update
    Transform camera;
    void Start()
    {
        camera = CameraController.instance.firstViewCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(camera.position, camera.position + camera.forward * 100f, Color.red);
    }
}
