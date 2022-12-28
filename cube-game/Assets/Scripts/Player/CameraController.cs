using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera _mainCamera;

    [SerializeField]
    internal CinemachineVirtualCamera firstViewCamera;

    [SerializeField]
    internal CinemachineVirtualCamera thirdViewCamera;
    float xRotationDegreeLimit = 87f;
    float xRotationOfHead = 0f;
    // float thirdViewCameraDistance = 5;

    public CinemachineVirtualCamera MainCamera
    {
        get { return _mainCamera; }
        private set
        {
            if (_mainCamera != value)
            {
                if (value == firstViewCamera)
                {
                    firstViewCamera.Priority = 100;
                    thirdViewCamera.Priority = 0;
                }
                else
                {
                    firstViewCamera.Priority = 0;
                    thirdViewCamera.Priority = 100;
                }

                _mainCamera = value;
            }
        }
    }

    void Awake()
    {
        _mainCamera = firstViewCamera;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void ChangeCameraViewToFirst()
    {
        MainCamera = firstViewCamera;
    }

    public void ChangeCameraViewToThird()
    {
        // MainCamera = thirdViewCamera;
    }

    internal void RotateX(float xDegree)
    {
        xRotationOfHead -= xDegree;
        xRotationOfHead = Mathf.Clamp(xRotationOfHead, -xRotationDegreeLimit, +xRotationDegreeLimit);

        if (MainCamera == firstViewCamera)
        {
            MainCamera.transform.localRotation = Quaternion.Euler(xRotationOfHead, 0f, 0f);
        }
        else
        {
            // MainCamera.transform.localPosition = angle * MainCamera.transform.localPosition;
        }
    }
}
