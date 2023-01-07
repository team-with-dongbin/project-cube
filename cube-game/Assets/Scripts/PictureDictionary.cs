using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class PictureDictionary : MonoBehaviour
{
    public static PictureDictionary instance;
    Dictionary<char, Color> Manufacture = new Dictionary<char, Color>();
    public List<Color[,]> pictureDatas = new List<Color[,]>();
    void Awake()
    {
        instance = this;
        Manufacture['R'] = Color.red;
        Manufacture['Y'] = Color.yellow;
        Manufacture['G'] = Color.green;
        Manufacture['B'] = Color.blue;
        Manufacture['W'] = Color.white;
        Manufacture['X'] = Color.black;
        Manufacture['M'] = Color.magenta;
        Manufacture['C'] = Color.cyan;

        string path = Application.dataPath + "/Pictures";
        string[] pictures = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

        foreach (string dir in pictures)
        {
            if (!dir.Contains(".meta"))
            {
                string[] dots = File.ReadAllText(dir).Split('\n', ' ');
                Color[,] picture = new Color[dots.Length, dots[0].Length];
                for (int i = 0; i < dots.Length; i++)
                {
                    for (int j = 0; j < dots[i].Length; j++)
                    {
                        if (Manufacture.ContainsKey(dots[i][j]))
                            picture[i, j] = Manufacture[dots[i][j]];
                    }
                }
                pictureDatas.Add(picture);
            }
        }
    }
}
