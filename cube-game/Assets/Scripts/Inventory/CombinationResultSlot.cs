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

    public void printResult(int itemId, int count)
    {
        if (itemId == -1)
        {
            combinationResultSlot.ClearSlot();
            return;
        }
        else if (combinationResultSlot.item.Any())
        {
            return;
        }
        //itemid�� ������ ��������� �������̵� instantiate�ϵ��� �����ؾ���.
        else if (itemId >= 1000 && itemId <= 1007)
        {
            combinationResultSlot.ClearSlot();
            for (int i = 0; i < count; i++)
            {
                GameObject cube = ItemDictionary.instance.InstantiateWithData(Cube, ItemDictionary.instance.FindById(itemId));
                cube.GetComponent<Cube>().Drop();
                if (i == 0) combinationResultSlot.NewSlot(cube);
                else combinationResultSlot.AddCount(cube);
                cube.SetActive(false);
            }
        }
    }
}
