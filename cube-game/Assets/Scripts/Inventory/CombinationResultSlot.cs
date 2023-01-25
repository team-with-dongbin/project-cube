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
        //��� ���սĿ� ���� üũ.
        foreach ((Dictionary<int, int> combination, (int, int) output) in CombinationDictionary.instance.GetCombinationList())
        {
            bool ok = ingredients.Count == combination.Count;
            //�ش� ���ս��� ��� �������� ������ ���� �̻� ������ �ִ� �� üũ.
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
            //id/���� ������, ����������� �־���ߵ�.
            //initializeSlot() �Լ� �����Ǹ� �װɷ� �ٲ����.
            combinationResultSlot.ClearSlot();
            //���� picture���� pictureCube �����ϴ°� / PlayerController���� GetItem ȣ���ϴ°Ͱ� ���������,
            //�̹��ϰ� ��Ȳ�� �޶� ���� ������ۿ�������. �ڵ� ��Ȱ���� �� �ȵ�.
            for (int i = 0; i < result.Item2; i++)
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
}
