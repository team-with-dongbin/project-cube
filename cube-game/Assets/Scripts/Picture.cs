using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
    public GameObject cube;
    private GameObject[,] picture = new GameObject[5, 5];

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++)
            for(int j = 0; j < 5; j++)
            {
                picture[i, j] = Instantiate(cube, new Vector3(i, j, 0), Quaternion.identity);
                Color c = picture[i, j].GetComponent<Renderer>().material.color;
                c.a = 0.3f;
                picture[i, j].GetComponent<Renderer>().material.color = c;
                picture[i, j].transform.SetParent(this.transform);
                picture[i, j].name = new string("Picture (" + i + "," + j + ")");
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
