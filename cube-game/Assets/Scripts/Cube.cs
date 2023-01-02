using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Cube : Item, IDamageable
{
    private float hp = 100f;
    private CubeData _cubeData;
    private AudioSource audioSource;
    void Start()
    {
        if (data == null)
        {
            data = ItemDictionary.instance.GetRandomDataOfType(ItemType.Cube);
        }
        _cubeData = data as CubeData;
        GetComponent<Renderer>().material.SetColor("_Color", _cubeData.color);
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
            audioSource.clip = _cubeData.strikeSound;
            audioSource.Play();
        }
    }

    private void Destruction()
    {
        audioSource.clip = _cubeData.destroySound;
        audioSource.Play();
        Drop();
    }
}
