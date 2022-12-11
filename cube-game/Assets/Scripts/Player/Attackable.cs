using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{
    const int CubeLayer = 9;
    public float baseDamage = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == CubeLayer)
        {
            Debug.Log("Attack Cube !!");
        }
    }
}
