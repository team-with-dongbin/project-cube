using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider), typeof(BoxCollider), typeof(Rigidbody))]
public abstract class Item : MonoBehaviour
{
    protected bool dropped = false;
    [field: SerializeField]
    public ItemData data { get; protected set; }

    protected virtual void Update()
    {
        if (dropped)
            transform.Rotate(Vector3.up * 100 * Time.deltaTime);
    }

    // data = itemData와 함께 시작해야 함.
    public abstract void InitializeData(ItemData itemData);

    float correction()
    {
        return transform.localScale.magnitude / Mathf.Sqrt(3);
    }
    protected virtual void Awake()
    {
        //OnEnable에 있으면 아이템이 drop될때 자동으로 꺼져서 안됨.
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<SphereCollider>().radius = 5 / correction();
    }

    protected virtual void Start()
    {

    }
    protected virtual void OnEnable()
    {
        float sz = 0;
        foreach (var mr in transform.GetComponentsInChildren<MeshRenderer>())
            sz = Mathf.Max(sz, mr.localBounds.size.magnitude) / Mathf.Sqrt(3);
        transform.localScale /= sz;
    }

    public virtual void Drop()
    {
        transform.localScale /= 3;
        transform.rotation = Quaternion.identity;
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (data.itemType != ItemType.Cube)
        {
            boxCollider.enabled = true;
            boxCollider.size = Vector3.one / 1000;
            GetComponent<Rigidbody>().useGravity = true;
            boxCollider.center = Vector3.down / 2 / correction();
        }
        GetComponent<SphereCollider>().enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Item");
        gameObject.tag = "Item";

        dropped = true;
        GetComponent<Rigidbody>().constraints =
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }
    public virtual void put()
    {
        transform.localScale *= 3;
        transform.rotation = Quaternion.identity;
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (data.itemType != ItemType.Cube)
        {
            boxCollider.enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            boxCollider.center = Vector3.zero;
        }
        GetComponent<SphereCollider>().enabled = false;
        //아이템 종류에 맞는 레이어로 설정하도록 변경해야함.
        gameObject.layer = LayerMask.NameToLayer("Cube");
        gameObject.tag = "Cube";
        dropped = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
