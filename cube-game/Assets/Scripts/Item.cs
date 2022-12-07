using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour{
    private bool IsSpread = false;
    public enum ItemType
    {
        Equipment, Cube, Gun, Knife, potion, size
    }

    public void Update(){
        if(IsSpread)
            transform.Rotate(Vector3.up * 100 * Time.deltaTime);
    }

    public void UpdateStat()
    {

    }

    public void SpreadItem() {
        Transform transform = GetComponent<Transform>();
        transform.localScale /= 3;
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.size *=2;
        UpdateStat();
        IsSpread = true;
        GetComponent<Rigidbody>().constraints =
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
}
