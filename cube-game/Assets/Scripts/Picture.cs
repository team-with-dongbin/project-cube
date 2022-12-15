using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
    [SerializeField]
    private GameObject Cube;

    public static int pictureSize = 5;
    private GameObject[,] picture = new GameObject[pictureSize, pictureSize];

    // Start is called before the first frame update
    void Start()
    {
        Vector3 picturePos = this.transform.position;

        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.transform.position = picturePos;
        floor.transform.localScale = new Vector3(pictureSize + 2, 1, pictureSize + 2);
        floor.transform.SetParent(this.transform);

        for (int i = 0; i < pictureSize; i++)
            for (int j = 0; j < pictureSize; j++)
            {
                picture[i, j] = Instantiate(Cube, picturePos + new Vector3(i - 2, 1, j - 2), Quaternion.identity);
                picture[i, j].transform.SetParent(this.transform);
            }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
