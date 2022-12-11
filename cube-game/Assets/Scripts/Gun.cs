﻿using System.Collections;
using UnityEngine;

// 총을 구현
public class Gun : Item {

    public enum State { Ready,Empty,Reloading }
    private State state;
    public Transform fireTransform; // 탄알이 발사될 위치
    private AudioSource audioSource;
    public GunData gunData;

    private float range = 50f;
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
    }

    private void OnEnable() {
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