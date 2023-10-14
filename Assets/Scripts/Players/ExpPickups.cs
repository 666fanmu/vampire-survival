using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickups : MonoBehaviour
{
    public int ExpValue;
    
    private bool ifMoveToPlayer;
    public float MoveSpeed;
    private PlayerControl _playerControl;

    private void Awake()
    {
        _playerControl = FindObjectOfType<PlayerControl>();
    }

    private void Update()
    {
        if (ifMoveToPlayer == true)
        {
            MoveSpeed += _playerControl.PlayerMoveSpeed;
            transform.position = Vector3.MoveTowards(transform.position, 
                PlayerHealth.Instance.transform.position,
                MoveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag=="Player")
        {
            ExperienceControl.Instance.GetExperience(ExpValue);
            Destroy(gameObject);
        }
        else if(col.tag=="PickupRange")
        {
            ifMoveToPlayer = true;
            //Debug.Log(ifMoveToPlayer);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag=="PickupRange")
        {
            ifMoveToPlayer = false;
            //Debug.Log(ifMoveToPlayer);
        }
    }
}
