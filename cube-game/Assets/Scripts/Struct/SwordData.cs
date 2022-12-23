using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/SwordData", fileName = "Sword Data")]
public class SwordData : WeaponData
{
    public AudioClip swingClip;
    public float attackRange = 0; // 칼의 사거리
}
