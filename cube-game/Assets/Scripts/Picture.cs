using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
    [SerializeField]
    private GameObject Cube;

    private Color[,] pictureColors;
    private GameObject[,] picture;

    private void Awake()
    {
        pictureColors = PictureDictionary.instance.pictureDatas[Random.Range(0, PictureDictionary.instance.pictureDatas.Count)];
        picture = new GameObject[pictureColors.GetLength(0),pictureColors.GetLength(1)];
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 picturePos = transform.position;

        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.transform.position = picturePos;
        floor.transform.localScale = new Vector3(picture.GetLength(0) + 2, 1, picture.GetLength(1) + 2);
        floor.transform.SetParent(transform);

        for (int i = 0, x = picture.GetLength(0); i < x; i++)
            for (int j = 0, y = picture.GetLength(1); j < y; j++)
            {
                GameObject dot = Instantiate(Cube, picturePos + new Vector3(i - x / 2, 1, j - y / 2), Quaternion.identity);
                Color dotColor = pictureColors[i, j];
                dotColor.a = 0.3f;
                dot.GetComponent<Renderer>().material.color = dotColor;
                picture[i, j] = dot;
                picture[i, j].transform.SetParent(transform);
            }
        transform.localScale /= 2;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
