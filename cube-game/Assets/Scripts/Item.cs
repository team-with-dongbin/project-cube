using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour{
    public string itemName;
    public ItemType itemType;

    public enum ItemType
    {
        Equipment, Cube, Gun, Knife, potion, size
    }

    public void UpdateStat()
    {

    }

    public void SpreadItem() {
        Transform transform = GetComponent<Transform>();
        transform.localScale /= 3;
        //BoxCollider boxCollider = GetComponent<BoxCollider>();
        //boxCollider.size/=3;
        UpdateStat();


        GetComponent<Rigidbody>().constraints =
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        transform.Rotate(Vector3.up * 10, Time.deltaTime);
    }
}