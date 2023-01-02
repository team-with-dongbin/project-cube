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
            Color newColor = GetComponent<Renderer>().material.color;
            newColor.a = 1f;
            GetComponent<Renderer>().material.color = newColor;
        }
    }

    public void makeActive()
    {
        isActive = true;
    }
}
