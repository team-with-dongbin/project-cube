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
        //pixelArt 만드는 것처럼 외부에서 조합식을 받아와서 가공해서 쓰는게 더 보기 좋을듯.
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
        //start에서 result에 대입해버리면 블록을 조합할 때마다 랜덤이 아니게됨.
        ingredients.Clear(); ingredients.Add((1000, 3)); result = (Random.Range(1001, 1008), 2);
        CombinationList.Add(MakeCombination(ingredients, result));//B*3=> ?*2
    }

    public void TryCombination(Dictionary<int, int> ingredients)
    {
        (int, int) result = (-1, -1);
        //모든 조합식에 대해 체크.
        foreach ((Dictionary<int, int> combination, (int, int) output) in CombinationList)
        {
            bool ok = ingredients.Count == combination.Count;
            //해당 조합식의 모든 아이템을 정해진 개수 이상 가지고 있는 지 체크.
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
            //id/개수 가지고, 결과물아이템 넣어줘야됨.

            //initializeSlot() 함수 구현되면 그걸로 바꿔야함.
            combinationResultSlot.ClearSlot();
            //현재 picture에서 pictureCube 생성하는것 / PlayerController에서 GetItem 호출하는것과 비슷하지만,
            //미묘하게 상황이 달라서 직접 만들수밖에없었음. 코드 재활용이 잘 안됨.
            for(int i = 0; i < result.Item2; i++)
            {
                GameObject cube = ItemDictionary.instance.InstantiateWithData(Cube, ItemDictionary.instance.FindById(result.Item1));
                cube.GetComponent<Cube>().Drop();
                if (i == 0) combinationResultSlot.NewSlot(cube);
                else combinationResultSlot.AddCount(cube);
                cube.SetActive(false);
            }
        }
        else //큐브 이외의 다른 아이템을 만드는 조합식이 추가될 예정.
        {
            return;
        }

    } 
    // Update is called once per frame
    void Update()
    {
        
    }
}
