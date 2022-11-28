using System;
using UnityEngine;

public class Cube : MonoBehaviour, IDamageable{
    public enum Type {Health, Speed, Damage, Ammo, Size}
    public Type type;
    public float hp = 100f;
    public bool isBroken = false;

    Color[] c = { Color.red, Color.magenta, Color.yellow, Color.green };

    public void OnEnable() {
        type = (Type)UnityEngine.Random.Range(0, (int)Type.Size);
        this.GetComponent<Renderer>().material.SetColor("_Color", c[(int)type]);
        //this.GetComponent<Renderer>().material.SetColor("_EdgeColor", Color.black);
    }

    public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal) {
        hp -= damage;
        if (hp <= 0) {
            isBroken = true;
        }
    }
}