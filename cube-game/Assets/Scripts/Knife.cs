using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(AudioSource))]
public class Knife : Weapon
{
    public enum State { Idle, Swing }
    private State state;
    private AudioSource audioSource;
    private Transform cameraTransform;
    private KnifeData _knifeData;

    public override void InitializeData(ItemData itemData)
    {
        data = itemData;
        _knifeData = data as KnifeData;
        _knifeData.attackRange = gameObject.GetComponent<CapsuleCollider>().height + 1.0f;
    }

    protected override void Start()
    {
        base.Start();
        if (data)
        {
            InitializeData(data);
        }

        state = State.Idle;
        cameraTransform = Utils.GetFirstViewCameraTransform();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        base.Update();
        // Debug.DrawRay(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward) * _knifeData.attackRange, Color.red);
    }

    public override float CalculateDamage(float basePower)
    {
        return basePower + _knifeData.damage;
    }

    public override void Attack(float basePower)
    {
        if (state == State.Idle)
        {
            int layerMask = int.MaxValue & 1 << LayerMask.NameToLayer("Cube");

            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, _knifeData.attackRange, layerMask))
            {
                IDamageable target = hit.collider.GetComponent<IDamageable>();
                target?.OnDamage(CalculateDamage(basePower), hit.point, hit.normal);
            }
            audioSource.PlayOneShot(_knifeData.swingClip);
        }
    }

    public override WeaponType GetWeaponType()
    {
        return WeaponType.Knife;
    }
}
