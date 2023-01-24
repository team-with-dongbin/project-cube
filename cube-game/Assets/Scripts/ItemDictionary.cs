using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemDictionary : MonoBehaviour
{
    [SerializeField]
    List<ItemData> itemDatas = new List<ItemData>();

    // Dictionary
    Dictionary<string, ItemData> dataByName = new Dictionary<string, ItemData>();
    Dictionary<int, ItemData> dataById = new Dictionary<int, ItemData>();
    Dictionary<ItemType, List<ItemData>> dataListByTypes = new Dictionary<ItemType, List<ItemData>>();

    public static ItemDictionary instance;

    void Awake()
    {
        InitializeDict();
        instance = this;
    }

    private void InitializeDict()
    {
        foreach (var data in itemDatas)
        {
            dataByName[data.itemName] = data;
            dataById[data.id] = data;
        }

        foreach (var data in itemDatas)
        {
            if (dataListByTypes.ContainsKey(data.itemType) == false)
            {
                dataListByTypes[data.itemType] = new List<ItemData>();
            }

            dataListByTypes[data.itemType].Add(data);
        }
    }

    public List<ItemData> GetDatasOfType(ItemType type)
    {
        return dataListByTypes[type];
    }

    public ItemData GetRandomDataOfType(ItemType type)
    {
        var datas = GetDatasOfType(type);
        return datas[UnityEngine.Random.Range(0, datas.Count)];
    }

    public ItemData FindByName(string itemName)
    {
        return dataByName[itemName];
    }

    public ItemData FindById(int id)
    {
        return dataById[id];
    }

    public GameObject InstantiateWithData(GameObject go, ItemData data)
    {
        var newGo = Instantiate(go);
        newGo.GetComponent<Item>().data = data;
        return newGo;
    }
}
