using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour{
    private bool dropped = false;
    public enum ItemType
    {
        Equipment, Cube, Gun, Knife, potion, size
    }

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
        boxCollider.size *=2;
        UpdateStat();
        dropped = true;
        GetComponent<Rigidbody>().constraints =
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
}
