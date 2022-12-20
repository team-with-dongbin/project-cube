using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour{
    protected bool dropped = false;

    public struct Data{
        public string ItemName;
        public float Health, Speed, Damage, Ammo;
        public Image ItemImage;
        public int ItemNumber;

        public Data(string itemName, float health,float speed,float damage,float ammo,Image image, int itemNumber){
            ItemName = itemName;
            Health = health;
            Speed = speed;
            Damage = damage;
            Ammo = ammo;
            ItemImage = image;
            ItemNumber = itemNumber;
        }
    }
    public Data data = new Data("",0, 0, 0, 0, null, 1000);

    public enum ItemType{
        Equipment, Cube, Gun, Knife, potion, size
    }
    public ItemType itemType;

    public void Update(){
        if (dropped)
            transform.Rotate(Vector3.up * 100 * Time.deltaTime);
    }

    public void UpdateStat()
    {

    }

    public void Drop() {
        Transform transform = GetComponent<Transform>();
        transform.localScale /= 3;
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        //boxCollider.size /= 1000;
        boxCollider.center = Vector3.down / 2;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        gameObject.layer = LayerMask.NameToLayer("Item");
        gameObject.tag = "Item";

        UpdateStat();
        //플레이어가 획득할때 다시 풀어줘야함.
        dropped = true;
        rigidbody.constraints =
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | 
            RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }
}
