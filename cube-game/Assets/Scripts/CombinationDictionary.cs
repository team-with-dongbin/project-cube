using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class CombinationDictionary : MonoBehaviour
{
    [SerializeField]
    private GameObject Cube;

    public static CombinationDictionary instance;
    List<(Dictionary<int, int>, (int, int))> CombinationList = new();

    public Slot combinationResultSlot;

    (Dictionary<int, int>, (int, int)) MakeCombination(List<(int,int)> ingredients,(int,int) result)
    {
        Dictionary<int, int> combination = new();
        foreach (var ingredient in ingredients)
            combination.Add(ingredient.Item1, ingredient.Item2);
        return (combination,result);
    }
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //pixelArt ����� ��ó�� �ܺο��� ���ս��� �޾ƿͼ� �����ؼ� ���°� �� ���� ������.
        List<(int,int)> ingredients = new();(int, int) result;
        ingredients.Clear(); ingredients.Add((1001, 1)); ingredients.Add((1002, 1));
        result = (1004, 2);CombinationList.Add(MakeCombination(ingredients,result));//R+G=Y
        ingredients.Clear(); ingredients.Add((1001, 1)); ingredients.Add((1003, 1));
        result = (1005, 2); CombinationList.Add(MakeCombination(ingredients, result));//R+B=M
        ingredients.Clear(); ingredients.Add((1002, 1)); ingredients.Add((1003, 1));
        result = (1006, 2); CombinationList.Add(MakeCombination(ingredients, result));//G+B=C
        ingredients.Clear(); ingredients.Add((1001, 1)); ingredients.Add((1006, 2));
        result = (1007, 3); CombinationList.Add(MakeCombination(ingredients, result));//R+C=W
        ingredients.Clear(); ingredients.Add((1002, 1)); ingredients.Add((1005, 2));
        result = (1007, 3); CombinationList.Add(MakeCombination(ingredients, result));//G+M=W
        ingredients.Clear(); ingredients.Add((1003, 1)); ingredients.Add((1004, 2));
        result = (1007, 3); CombinationList.Add(MakeCombination(ingredients, result));//B+Y=W
        ingredients.Clear(); ingredients.Add((1001, 1)); ingredients.Add((1002, 1)); ingredients.Add((1003, 1));
        result = (1007, 3); CombinationList.Add(MakeCombination(ingredients, result));//R+G+B=W
        //start���� result�� �����ع����� ����� ������ ������ ������ �ƴϰԵ�.
        ingredients.Clear(); ingredients.Add((1000, 3)); result = (Random.Range(1001, 1008), 2);
        CombinationList.Add(MakeCombination(ingredients, result));//B*3=> ?*2
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
