using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyWeapon : MonoBehaviour
{
    public float MoveSpeed;
    void Update()
    {
        gameObject.transform.position += transform.up * MoveSpeed * Time.deltaTime;
    }
}
