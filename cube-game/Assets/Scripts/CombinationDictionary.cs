using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using System.IO;

public class CombinationDictionary : MonoBehaviour
{
    [SerializeField]
    private GameObject Cube;
    public static CombinationDictionary instance;
    List<(Dictionary<int, int>, (int, int))> CombinationList = new();
    public Slot combinationResultSlot;

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

    void Start()
    {

    }

    public void TryCombination(Dictionary<int, int> ingredients)
    {
        (int, int) result = (-1, -1);
        //��� ���սĿ� ���� üũ.
        foreach ((Dictionary<int, int> combination, (int, int) output) in CombinationList)
        {
            bool ok = ingredients.Count == combination.Count;
            //�ش� ���ս��� ��� �������� ������ ���� �̻� ������ �ִ� �� üũ.
            if (!ok) continue;
            foreach ((int item,int need) in combination)
            {
                if (!ingredients.ContainsKey(item) || need > ingredients[item])
                {
                    ok = false;
                    break;
                }
            }
            if (ok)
            {
                result = output;
                break;
            }
        }
        if (result.Item1 == -1000) result.Item1 = Random.Range(1001, 1008);

        if (result.Item1 == -1)
        {
            combinationResultSlot.ClearSlot();
            return;
        }
        else if (combinationResultSlot.item.Any())
        {
            return;
        }
        else if (result.Item1 >= 1000 && result.Item1 <= 1007)
        {
            //id/���� ������, ����������� �־���ߵ�.
            //initializeSlot() �Լ� �����Ǹ� �װɷ� �ٲ����.
            combinationResultSlot.ClearSlot();
            //���� picture���� pictureCube �����ϴ°� / PlayerController���� GetItem ȣ���ϴ°Ͱ� ���������,
            //�̹��ϰ� ��Ȳ�� �޶� ���� ������ۿ�������. �ڵ� ��Ȱ���� �� �ȵ�.
            for(int i = 0; i < result.Item2; i++)
            {
                GameObject cube = ItemDictionary.instance.InstantiateWithData(Cube, ItemDictionary.instance.FindById(result.Item1));
                cube.GetComponent<Cube>().Drop();
                if (i == 0) combinationResultSlot.NewSlot(cube);
                else combinationResultSlot.AddCount(cube);
                cube.SetActive(false);
            }
        }
        else //ť�� �̿��� �ٸ� �������� ����� ���ս��� �߰��� ����.
        {
            return;
        }

    } 
    // Update is called once per frame
    void Update()
    {
        
    }
}
