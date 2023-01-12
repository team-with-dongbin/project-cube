using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
    public static Picture instance;
    [SerializeField]
    private GameObject Cube;

    private Color[,] pictureColors;
    private GameObject[,] picture;
    Dictionary<GameObject, (int, int)> picture_coordinates = new();
    Dictionary<GameObject, (int, int)> cube_coordinates = new();

    private int remainPicture;
    Material outline;

    private void Awake()
    {
        instance = this;
        pictureColors = PictureDictionary.instance.pictureDatas[Random.Range(0, PictureDictionary.instance.pictureDatas.Count)];
        picture = new GameObject[pictureColors.GetLength(0),pictureColors.GetLength(1)];
        remainPicture = pictureColors.GetLength(0) * pictureColors.GetLength(1);
        //outline = new Material(Shader.Find("Custom/Outline"));
        outline = new Material(Shader.Find("Draw/OutlineShader"));
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
                GameObject pixel = Instantiate(Cube, picturePos + new Vector3(i - x / 2, 1, j - y / 2), Quaternion.identity);
                picture_coordinates.Add(pixel, (i, j));
                Color color = pictureColors[i, j];
                color.a = 0.3f;
                pixel.GetComponents<Renderer>()[0].material.color = color;
                picture[i, j] = pixel;
                picture[i, j].transform.SetParent(transform);
            }
        transform.localScale /= 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainPicture == 0) 
        {
            //Gameover
        }
    }

    public void fitCube(GameObject pictureCube, GameObject cube)
    {
        pictureCube.SetActive(false);
        cube.SetActive(false);
        cube.transform.SetParent(transform);
        cube.transform.localScale = pictureCube.transform.localScale;
        cube.transform.position = pictureCube.transform.position;
        cube.transform.rotation = pictureCube.transform.rotation;
        cube.GetComponent<Cube>().hp = cube.GetComponent<Cube>().cubeData.initialHp * 5;
        cube.SetActive(true);
        remainPicture--;
    }

    public void restore(GameObject cube)
    {
        var coordinate = cube_coordinates[cube];
        cube.SetActive(false);
        cube.transform.localScale = Vector3.one;
        cube.transform.parent = null;
        cube_coordinates.Remove(cube);
        cube.SetActive(true);
        remainPicture++;
    }

    public void DrawOutline(GameObject pictureCube)
    {
        Renderer renderer = pictureCube.GetComponent<Renderer>();
        if (renderer == null) return;

        List<Material> materialList = new();
        materialList.AddRange(renderer.sharedMaterials);
        if (!materialList.Contains(outline))
            materialList.Add(outline);
        renderer.materials = materialList.ToArray();
    }

    public void EraseOutline(GameObject pictureCube)
    {
        Renderer renderer = pictureCube.GetComponent<Renderer>();
        if (renderer == null) return;

        List<Material> materialList = new();
        //materialList.Clear();
        materialList.AddRange(renderer.sharedMaterials);
        if (materialList.Contains(outline))
            materialList.Remove(outline);
        renderer.materials = materialList.ToArray();
    }
}
