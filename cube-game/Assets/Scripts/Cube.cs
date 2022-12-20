using System;
using UnityEngine;
using UnityEngine.UI;

public class Cube : Item, IDamageable{

    private float hp = 100f;
    [SerializeField]
    private GameObject cube;

    [SerializeField]
    private AudioClip strikeSound,destroySound;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    public Sprite[] cubeSprites;

    public enum Type {Health, Speed, Damage, Ammo, Size}
    public Type type;

    Color[] c = { Color.red, Color.magenta, Color.yellow, Color.green };

    public void Awake() {
        type = (Type)UnityEngine.Random.Range(0, (int)Type.Size);
        GetComponent<Renderer>().material.SetColor("_Color", c[(int)type]);
        if (type.Equals(Type.Health)) data.Health = 3;
        else if (type.Equals(Type.Speed)) data.Speed = 2;
        else if (type.Equals(Type.Damage)) data.Damage = 3;
        else if (type.Equals(Type.Ammo)) data.Ammo = 1;
        data.ItemSprite = cubeSprites[(int)type];
        data.ItemName = "Cube";
    }

    public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal) {
        hp -= damage;
        if (hp <= 0){
            if(!dropped)
                Destruction();
        }
        else{
            audioSource.clip = strikeSound;
            audioSource.Play();
        }
    }

    private void Destruction(){
        audioSource.clip = destroySound;
        audioSource.Play();
        Drop();
    }
}