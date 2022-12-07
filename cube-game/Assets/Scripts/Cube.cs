using System;
using UnityEngine;

public class Cube : Item, IDamageable{

    private float hp = 100f;
    [SerializeField]
    private GameObject cube;

    [SerializeField]
    private AudioClip strikeSound,destroySound;
    [SerializeField]
    private AudioSource audioSource;



    public enum Type {Health, Speed, Damage, Ammo, Size}
    public Type type;

    Color[] c = { Color.red, Color.magenta, Color.yellow, Color.green };

    public void OnEnable() {
        type = (Type)UnityEngine.Random.Range(0, (int)Type.Size);
        this.GetComponent<Renderer>().material.SetColor("_Color", c[(int)type]);
        //this.GetComponent<Renderer>().material.SetColor("_EdgeColor", Color.black);
        OnDamage((float)150.0, Vector3.zero, Vector3.zero);
    }

    public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal) {
        hp -= damage;
        if (hp <= 0)
            Destruction();
        else{
            audioSource.clip = strikeSound;
            audioSource.Play();
        }
    }

    private void Destruction(){
        audioSource.clip = destroySound;
        audioSource.Play();
        SpreadItem();
        //Destroy(cube,destroySound.length);
    }
}