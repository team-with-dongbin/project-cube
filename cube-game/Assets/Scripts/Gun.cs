using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gun : Item {

    public enum State { Ready,Empty,Reloading }
    private State state;
    public Transform fireTransform; // 탄알이 발사될 위치
    private AudioSource audioSource;
    [SerializeField]
    private GunData gunData;
    [SerializeField]
    private Image gunImage;

    public int ammoRemain = 100; // 남은 전체 탄알
    public int magAmmo; // 현재 탄알집에 남아 있는 탄알

    private float lastFireTime; // 총을 마지막으로 발사한 시점

    private void Awake() {
        // 사용할 컴포넌트의 참조 가져오기
        audioSource = GetComponent<AudioSource>();
        ammoRemain = gunData.startAmmoRemain;
        magAmmo = gunData.magCapacity;
        state = State.Ready;
        lastFireTime = 0;
        data.Damage = gunData.damage;
        data.ItemName = "Gun";
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.R))
            Reload();
    }

    // 발사 시도
    public void Fire() {
        if(state==State.Ready && Time.time >= lastFireTime + gunData.timeBetFire){
            lastFireTime = Time.time;
            Shot();
        }
    }

    // 실제 발사 처리
    private void Shot() {
        GameObject instantBullet = Instantiate(gunData.bullet, fireTransform.position, fireTransform.rotation);
        instantBullet.GetComponent<Bullet>().ValueSetting(gunData.damage,gunData.bulletSpeed);

        audioSource.PlayOneShot(gunData.shotClip);
        magAmmo--;
        if (magAmmo <= 0)
            state = State.Empty;
    }

    // 재장전 시도
    public bool Reload() {
        if(state == State.Reloading || ammoRemain <= 0 || magAmmo >= gunData.magCapacity)
            return false;
        StartCoroutine(ReloadRoutine());
        return true;
    }

    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRoutine() {
        state = State.Reloading;
        audioSource.PlayOneShot(gunData.reloadClip);

        yield return new WaitForSeconds(gunData.reloadTime);

        int ammoToFill = Mathf.Min(gunData.magCapacity - magAmmo, ammoRemain);
        magAmmo += ammoToFill;
        ammoRemain -= ammoToFill;
        state = State.Ready;
    }
}