using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    protected bool dropped = false;
    [field: SerializeField]
    public ItemData data { get; protected set; }
    protected SphereCollider sphereCollider;

    private void Awake()
    {
        sphereCollider = gameObject.GetComponent<SphereCollider>();
    }

    protected virtual void Update()
    {
        if (dropped)
            transform.Rotate(Vector3.up * 100 * Time.deltaTime);
    }

    // data = itemData와 함께 시작해야 함.
    public abstract void InitializeData(ItemData itemData);

    public virtual void Drop()
    {
        transform.localScale /= 3;
        transform.rotation = Quaternion.identity;
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        sphereCollider.enabled = true;
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
    public virtual void put()
    {
        transform.localScale *= 3;
        transform.rotation = Quaternion.identity;
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        sphereCollider.enabled = false;
        //boxCollider.size /= 1000;
        boxCollider.center = Vector3.zero;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        gameObject.layer = LayerMask.NameToLayer("Cube");
        gameObject.tag = "Cube";
        dropped = false;
        rigidbody.constraints = RigidbodyConstraints.None;
    }
}
