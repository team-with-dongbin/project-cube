using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ItemData", fileName = "Item Data")]
public class ItemData : ScriptableObject
{
    public int id;
    public string itemName;
    // public GameObject prefab;
    public Sprite icon;
    public ItemType itemType = ItemType.Default;

    [Multiline(2)]
    public string desription = "";
}
