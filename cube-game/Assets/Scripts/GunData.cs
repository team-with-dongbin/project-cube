using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/GunData",fileName = "Gun Data")]
public class GunData : ScriptableObject{
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public GameObject bullet;
    public float bulletSpeed = 0;
    public float damage = 0; // 공격력
    public int startAmmoRemain = 0; // 처음에 주어질 전체 탄약
    public int magCapacity = 0; // 탄창 용량
    public float timeBetFire = 0; // 총알 발사 간격
    public float reloadTime = 0; // 재장전 소요 시간
}