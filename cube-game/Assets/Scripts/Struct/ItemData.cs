using UnityEngine;
using System.Reflection;

[CreateAssetMenu(menuName = "Scriptable/ItemData", fileName = "Item Data")]
public partial class ItemData : ScriptableObject
{
    public int id;
    public string itemName;
    public GameObject prefab;
    public Sprite icon;
    public ItemType itemType = ItemType.Default;

    [Multiline(2)]
    public string desription = "";

    // 이 함수는 성능적으로 불리하고, FieldNames를 관리하기 어렵게 만듭니다.
    // 이 함수를 남용하지 마세요.
    public T GetField<T>(FieldNames fieldName, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance)
    {
        return GetField<T>(fieldName.ToString(), bindingAttr);
    }

    // 필드의 이름을 수정하는 경우, 이 함수를 호출할 때의 fieldName도 반드시 같이 수정되어야 함에 주의하세요.
    private T GetField<T>(string fieldName, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance)
    {
        var fieldInfo = GetType().GetField(fieldName, bindingAttr);
        return (T)(fieldInfo.GetValue(this));
    }
}
