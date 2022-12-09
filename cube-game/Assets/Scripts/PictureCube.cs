using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureCube : MonoBehaviour
{
    //private GameObject pictureCube;
    private bool isActive = false;

    Color[] c = { Color.red, Color.magenta, Color.yellow, Color.green };
    // Start is called before the first frame update
    void Start()
    {
        Color newColor = c[Random.Range(0, c.Length)];
        newColor.a = 0.3f;
        this.GetComponent<Renderer>().material.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            Color newColor = this.GetComponent<Renderer>().material.color;
            newColor.a = 1f;
            this.GetComponent<Renderer>().material.color = newColor;
        }
    }

    public void makeActive()
    {
        isActive = true;
    }
}
