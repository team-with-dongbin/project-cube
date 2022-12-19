using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Item : MonoBehaviour{
    public bool dropped = false;
    public enum ItemType
    {
        Equipment, Cube, Gun, Knife, potion, size
    }

    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;

    public void Update(){
        if(dropped)
            transform.Rotate(Vector3.up * 100 * Time.deltaTime);
    }

    public void UpdateStat()
    {

    }

    public void Drop() {
        Transform transform = GetComponent<Transform>();
        transform.localScale /= 3;
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        boxCollider.size *= 2;
        //gameObject.tag = "Item";
        UpdateStat();
        //�÷��̾ ȹ���Ҷ� �ٽ� Ǯ�������.
        dropped = true;
        GetComponent<Rigidbody>().constraints =
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
}
