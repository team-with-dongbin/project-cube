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

    void Start()
    {
        if (data == null)
        {
            data = ItemDictionary.instance.GetRandomDataOfType(ItemType.Cube);
        }

        InitializeData(data);
        audioSource = GetComponent<AudioSource>();
    }

    public void OnEnable()
    {
        sphereCollider.enabled = false;
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
            audioSource.clip = cubeData.strikeSound;
            audioSource.Play();
        }
    }

    private void Destruction()
    {
        audioSource.clip = cubeData.destroySound;
        audioSource.Play();
        if (transform.parent != null)
        {
            if (transform.parent.GetComponent<Picture>() != null)
            {
                Picture.instance.restore(gameObject);
            }
        }
        Drop();
    }
    protected override void Update()
    {
        base.Update();
    }
}
