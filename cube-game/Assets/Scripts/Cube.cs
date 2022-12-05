using System;
using UnityEngine;

public class Cube : MonoBehaviour, IDamageable{

    [SerializeField]
    public float hp = 100f;

    [SerializeField]
    private BoxCollider boxCollider;


    [SerializeField]
    private GameObject cube;
    //[SerializeField]
    //private GameObject cubeItemPrefab;

    [SerializeField]
    private string strikeSound;
    [SerializeField]
    private string destroySound;



    public enum Type {Health, Speed, Damage, Ammo, Size}
    public Type type;

    Color[] c = { Color.red, Color.magenta, Color.yellow, Color.green };

    public void OnEnable() {
        type = (Type)UnityEngine.Random.Range(0, (int)Type.Size);
        this.GetComponent<Renderer>().material.SetColor("_Color", c[(int)type]);
        //this.GetComponent<Renderer>().material.SetColor("_EdgeColor", Color.black);
        OnDamage((float)1111.0, Vector3.zero, Vector3.zero);
    }

    public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal) {
        hp -= damage;
        if (hp <= 0)
            Destruction();
        else
            SoundManager.instance.PlaySE(strikeSound);
    }

    private void Destruction(){
        SoundManager.instance.PlaySE(destroySound);
        boxCollider.enabled = false;
        //Instantiate(cubeItemPrefab, cube.transform.position, Quaternion.identity);
        Destroy(cube);
    }
}