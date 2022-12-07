using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
    private static int pictureSize = 5;
    private GameObject[,] picture = new GameObject[pictureSize, pictureSize];
    public GameObject Cube;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < pictureSize; i++)
            for(int j = 0; j < pictureSize; j++)
            {
                Vector3 pos = this.transform.position;
                picture[i, j] = Instantiate(Cube, pos + new Vector3(i, j, 0), Quaternion.identity);
                Color c = picture[i, j].GetComponent<Renderer>().material.color;
                c.a = 0.3f;
                picture[i, j].GetComponent<Renderer>().material.color = c;
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
