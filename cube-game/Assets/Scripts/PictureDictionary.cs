using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class PictureDictionary : MonoBehaviour
{
    public static PictureDictionary instance;
    Dictionary<char,Color> manufacture= new Dictionary<char,Color>();
    List<Color[,]> pictureDatas = new List<Color[,]>();
    void Awake()
    {
        instance = this;
        manufacture['R'] = Color.red;
        manufacture['Y'] = Color.yellow;
        manufacture['G'] = Color.green;
        manufacture['B'] = Color.blue;
        manufacture['W'] = Color.white;
        manufacture['X'] = Color.black;
        manufacture['M'] = Color.magenta;
        manufacture['C'] = Color.cyan;
        string path = Application.dataPath + "/Pictures";
        string[] pictures = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

        foreach (string dir in pictures)
        {
            if (!dir.Contains(".meta"))
            {
                string[] dots = File.ReadAllText(dir).Split('\n',' ');
                Color[,] picture = new Color[dots.Length, dots[0].Length];
                for (int i = 0; i < dots.Length; i++)
                {
                    for (int j = 0; j < dots[i].Length; j++)
                    {
                        if (manufacture.ContainsKey(dots[i][j]))
                            picture[i, j] = manufacture[dots[i][j]];
                    }
                }
                pictureDatas.Add(picture);
            }
        }
    }

    public List<Color[,]> GetPictureDatas()
    {
        return pictureDatas;
    }
}
