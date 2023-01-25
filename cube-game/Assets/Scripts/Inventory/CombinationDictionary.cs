using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using System.IO;

public class CombinationDictionary : MonoBehaviour
{
    public static CombinationDictionary instance;
    List<(Dictionary<int, int>, (int, int))> CombinationList = new();

    private void Awake()
    {
        instance = this;
        string path = Application.dataPath + "/Resources/CombinationList.txt";
        string[] combinationList_text = File.ReadAllLines(path);
        foreach(string s in combinationList_text)
        {
            string[] temp = s.Split('=');

            string[] combination_text = temp[0].Split('+');
            Dictionary<int, int> combination = new();
            foreach (var i in combination_text)
            {
                string[] ingredient_text = i.Split('*');
                int item_id = int.Parse(ingredient_text[0]), count = int.Parse(ingredient_text[1]);
                combination.Add(item_id, count);
            }

            string[] result_text = temp[1].Split('*');
            (int, int) result = (int.Parse(result_text[0]), int.Parse(result_text[1]));

            CombinationList.Add((combination, result));
        }
    }

    public List<(Dictionary<int, int>, (int, int))> GetCombinationList()
    {
        return CombinationList;
    }
}
