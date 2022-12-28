using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Gun : Weapon, IReloadable
{
    public enum State { Ready, Empty, Reloading }
    private State state;
    public Transform fireTransform; // 탄알이 발사될 위치
    private AudioSource audioSource;
    private Transform cameraTransform;
    private GunData _gunData;

    public int ammoRemain; // 남은 전체 탄알
    public int magAmmo; // 현재 탄알집에 남아 있는 탄알

    private float lastFireTime; // 총을 마지막으로 발사한 시점

    private void Awake()
    {
        // 사용할 컴포넌트의 참조 가져오기
    }

    private void Start()
    {
        _gunData = data as GunData;
        lastFireTime = 0;
        ammoRemain = _gunData.initialAmmoRemain;
        magAmmo = _gunData.magCapacity;

        state = State.Ready;
        cameraTransform = GetFirstViewCameraTransform();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Attack(float basePower)
    {
        Fire(basePower);
    }

    public override WeaponType GetWeaponType()
    {
        return WeaponType.Pistol;
    }

    // 발사 시도
    public void Fire(float basePower)
    {
        if (state == State.Ready && Time.time >= lastFireTime + _gunData.timeBetFire)
        {
            lastFireTime = Time.time;
            Shot(basePower);
        }
    }

    public override float CalculateDamage(float basePower)
    {
        return basePower + _gunData.damage;
    }

    // 실제 발사 처리
    private void Shot(float basePower)
    {
        GameObject instantBullet = Instantiate(_gunData.bullet, cameraTransform.position, cameraTransform.rotation);
        instantBullet.GetComponent<Bullet>().ValueSetting(CalculateDamage(basePower), _gunData.bulletSpeed);

        audioSource.PlayOneShot(_gunData.shotClip);
        magAmmo--;
        if (magAmmo <= 0)
            state = State.Empty;
    }

    // 재장전 시도
    public bool Reload()
    {
        if (state == State.Reloading || ammoRemain <= 0 || magAmmo >= _gunData.magCapacity)
            return false;
        StartCoroutine(ReloadRoutine());
        return true;
    }

    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;
        audioSource.PlayOneShot(_gunData.reloadClip);

        yield return new WaitForSeconds(_gunData.reloadTime);

        int ammoToFill = Mathf.Min(_gunData.magCapacity - magAmmo, ammoRemain);
        magAmmo += ammoToFill;
        ammoRemain -= ammoToFill;
        state = State.Ready;
    }
}
