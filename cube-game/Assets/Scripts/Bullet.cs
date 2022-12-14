using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage, bulletSpeed;
    public void ValueSetting(float damage, float bulletSpeed){
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
    }
    
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable target = collision.collider.GetComponent<IDamageable>();
        if (target != null)
            Hit(target);
        Destroy(gameObject);
    }
    void Update()
    {
        
    }

    private void Hit(IDamageable target)
    {
        target.OnDamage(damage, transform.position, -1 * transform.rotation.eulerAngles);
        //총알 맞는소리 기능 추가?
    }
}
