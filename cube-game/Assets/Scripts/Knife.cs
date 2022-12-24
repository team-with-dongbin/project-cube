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

    private void Start()
    {
        _knifeData = data as KnifeData;
        state = State.Idle;
        cameraTransform = GetFirstViewCameraTransform();
        audioSource = GetComponent<AudioSource>();
        _knifeData.attackRange = gameObject.GetComponent<CapsuleCollider>().height+10f;
    }

    private void Update()
    {
        Debug.DrawRay(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward)*_knifeData.attackRange, Color.red);
    }

    public override void Attack(float baseDamage)
    {
        if (state == State.Idle)
        {
            int layerMask = int.MaxValue & 1 << LayerMask.NameToLayer("Cube");

            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, _knifeData.attackRange, layerMask))
            {
                IDamageable target = hit.collider.GetComponent<IDamageable>();
                target?.OnDamage(_knifeData.damage, hit.point, hit.normal);
            }
            audioSource.PlayOneShot(_knifeData.swingClip);
        }
    }

    public override WeaponType GetWeaponType()
    {
        return WeaponType.Knife;
    }
}
