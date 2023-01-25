using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CombinationResultSlot : Slot
{
    public static CombinationResultSlot instance;
    [SerializeField]
    private GameObject Cube;
    CombinationResultSlot combinationResultSlot;

    private void Awake()
    {
        instance = this;
        combinationResultSlot = GetComponents<CombinationResultSlot>()[0];
    }

    public void TryCombination(Dictionary<int, int> ingredients)
    {
        (int, int) result = (-1, -1);
        //모든 조합식에 대해 체크.
        foreach ((Dictionary<int, int> combination, (int, int) output) in CombinationDictionary.instance.GetCombinationList())
        {
            bool ok = ingredients.Count == combination.Count;
            //해당 조합식의 모든 아이템을 정해진 개수 이상 가지고 있는 지 체크.
            if (!ok) continue;
            foreach ((int item, int need) in combination)
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
            //id/개수 가지고, 결과물아이템 넣어줘야됨.
            //initializeSlot() 함수 구현되면 그걸로 바꿔야함.
            combinationResultSlot.ClearSlot();
            //현재 picture에서 pictureCube 생성하는것 / PlayerController에서 GetItem 호출하는것과 비슷하지만,
            //미묘하게 상황이 달라서 직접 만들수밖에없었음. 코드 재활용이 잘 안됨.
            for (int i = 0; i < result.Item2; i++)
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
}
