using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureCube : MonoBehaviour
{
    //private GameObject pictureCube;
    private bool isActive = false;
    // Update is called once per frame

    void Update()
    {
        if (isActive)
        {
            Color color = GetComponents<Renderer>()[0].material.color;
            color.a = 1.0f;
            GetComponents<Renderer>()[0].material.color = color;
        }
    }

    public void makeActive()
    {
        isActive = true;
    }
}
