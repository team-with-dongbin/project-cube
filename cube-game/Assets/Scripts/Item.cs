using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    protected bool dropped = false;
    public ItemData data;

    void Start()
    {
    }
    public ItemType itemType;

    public virtual void Update()
    {
        if (dropped)
            transform.Rotate(Vector3.up * 100 * Time.deltaTime);
    }

    public virtual void Drop()
    {
        transform.localScale /= 3;
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        //boxCollider.size /= 1000;
        boxCollider.center = Vector3.down / 2;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        gameObject.layer = LayerMask.NameToLayer("Item");
        gameObject.tag = "Item";

        dropped = true;
        rigidbody.constraints =
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }
}
