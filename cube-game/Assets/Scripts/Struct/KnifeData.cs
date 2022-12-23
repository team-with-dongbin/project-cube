using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/KnifeData", fileName = "Knife Data")]
public class KnifeData : WeaponData
{
    public AudioClip swingClip;
    public float attackRange = 0; // 칼의 사거리
}
