using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Cube : Item, IDamageable
{
    public float hp = 100f;
    public CubeData cubeData;
    private AudioSource audioSource;
    private Transform cameraTransform;
    public Color color
    {
        get
        {
            return cubeData.color;
        }
    }

    public override void InitializeData(ItemData itemData)
    {
        data = itemData;
        cubeData = itemData as CubeData;
        GetComponent<Renderer>().material.SetColor("_Color", cubeData.color);
    }

    protected override void Start()
    {
        base.Start();
        if (data == null)
        {
            data = ItemDictionary.instance.GetRandomDataOfType(ItemType.Cube);
        }
        InitializeData(data);
        audioSource = GetComponent<AudioSource>();
    }

    public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        hp -= damage;
        if (hp <= 0)
        {
            if (!dropped)
                Destruction();
        }
        else
        {
            audioSource.PlayOneShot(cubeData.strikeSound);
        }
    }

    private void Destruction()
    {
        audioSource.PlayOneShot(cubeData.destroySound);
        transform.parent?.GetComponent<Picture>()?.restore(gameObject);
        Drop();
    }
    protected override void Update()
    {
        base.Update();
    }
}
