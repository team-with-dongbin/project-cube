using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Cube : Item, IDamageable
{
    public float hp = 100f;
    private float range = 2.0f;
    public CubeData cubeData;
    private AudioSource audioSource;
    private Transform cameraTransform;

    void Start()
    {
        if (data == null)
        {
            data = ItemDictionary.instance.GetRandomDataOfType(ItemType.Cube);
        }
        cubeData = data as CubeData;
        GetComponent<Renderer>().material.SetColor("_Color", cubeData.color);
        audioSource = GetComponent<AudioSource>();
        cameraTransform = Utils.GetFirstViewCameraTransform();
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
        //이 큐브가 그림판에 fix된 상태였는데 부서진거였다면,
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
