using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationDictionary : MonoBehaviour
{
    [SerializeField]
    private GameObject Cube;
    [SerializeField]
    private GameObject Slot;

    public static CombinationDictionary instance;
    List<(Dictionary<int, int>,(int,int))> CombinationList = new();
    void Start()
    {
        instance = this;
        Dictionary<int, int> isValid = new();
        (int, int) result;
        isValid.Add(1001, 1);isValid.Add(1002, 1);result = (1004, 2);
        CombinationList.Add((isValid, result));//R+G=Y
        isValid.Add(1001, 1); isValid.Add(1003, 1); result = (1005, 2);
        CombinationList.Add((isValid, result));//R+B=M
        isValid.Add(1002, 1); isValid.Add(1003, 1); result = (1006, 2);
        CombinationList.Add((isValid, result));//G+B=C
        isValid.Add(1001, 1); isValid.Add(1006, 2); result = (1007, 3);
        CombinationList.Add((isValid, result));//R+C=W
        isValid.Add(1002, 1); isValid.Add(1005, 2); result = (1007, 3);
        CombinationList.Add((isValid, result));//G+M=W
        isValid.Add(1003, 1); isValid.Add(1004, 2); result = (1007, 3);
        CombinationList.Add((isValid, result));//B+Y=W
        isValid.Add(1001, 1); isValid.Add(1002, 1); isValid.Add(1003, 1); result = (1007, 3);
        CombinationList.Add((isValid, result));//R+G+B=W
        isValid.Add(1000, 3); result = (Random.Range(1001,1008), 2);
        CombinationList.Add((isValid, result));//B*3=> ?*2
    }

    public GameObject TryCombination(Dictionary<int, int> ingredients)
    {
        (int, int) result = (-1, -1);
        //모든 조합식에 대해 체크.
        foreach(var check in CombinationList)
        {
            bool ok = true;
            //해당 조합식의 모든 아이템을 정해진 개수 이상 가지고 있는 지 체크.
            foreach(var ingredient in ingredients)
            {
                if (!check.Item1.ContainsKey(ingredient.Key) || check.Item1[ingredient.Key] < ingredient.Value)
                    ok = false;
            }
            if (ok)
            {
                result = check.Item2;
                break;
            }
        }

        if (result.Item1 == -1)
        {
            return null;
        }
        else
        {
            GameObject slot = Instantiate(Slot, new Vector3(1000,1000,1000), Quaternion.identity);
            if (result.Item1 > 1007) return null;
            GameObject cube = Instantiate(Cube, new Vector3(1000, 1000, 1000), Quaternion.identity);
            //id/개수 가지고, 결과물아이템 넣어줘야됨.
            slot.GetComponent<Slot>().ClearSlot();
            //initializeSlot() 함수 구현되면 그걸로 바꿔야함.
            //현재 picture에서 pictureCube 생성하는것과 같은 방식.
            for(int i = 0; i < result.Item2; i++)
            {
                cube.GetComponent<Cube>().cubeData = ItemDictionary.instance.dataById[result.Item1] as CubeData;
                cube.GetComponent<Renderer>().material.SetColor("_Color", cube.GetComponent<Cube>().color);
                if (i == 0) slot.GetComponent<Slot>().NewSlot(cube);
                else slot.GetComponent<Slot>().AddCount(cube);
            }
            return slot;
        }
    } 
    // Update is called once per frame
    void Update()
    {
        
    }
}
