using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ThrowWeapon : Weapon
{
    public WeaponDamage _WeaponDamage;
    public FlyWeapon _FlyWeapon;

    public float TimeBetweenSpawn;
    private float SpawnTimeCounter;

    public int ShotNumber;//射出数量

    public float WeaponRange;//武器索敌范围
    public int PenetrateNum;//穿透敌人的数量
    public LayerMask whatisEnemy;
    
    void Start()
    {
        SetStates();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimeCounter -= Time.deltaTime;
        if (SpawnTimeCounter<=0)
        {
            SpawnTimeCounter = TimeBetweenSpawn;
            //索敌出现问题，范围不明
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position,WeaponRange * WeaponStatesList[WeaponLevel].Range, whatisEnemy);

            if (enemies.Length>0)
            {
                Debug.Log("get target");
                
                for (int i = 0; i < ShotNumber; i++)
                {
                    //让飞刀转向敌人
                    Vector3 TargetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;
                    Vector3 TargetRotation = TargetPosition-this.transform.position;
                    float angle = Mathf.Atan2(TargetPosition.y, TargetPosition.x) * Mathf.Rad2Deg;
                    angle -= 90;
                    
                    
                    Instantiate(_FlyWeapon, gameObject.transform.position, Quaternion.AngleAxis(angle,Vector3.forward),transform).gameObject.SetActive(true);
                    Debug.Log("born knife");
                }
            }

            if (PenetrateNum == 0)
            {
                _WeaponDamage.ifDestorySelf = true;
            }
            
        }
        
        
        
        
        if (StatesUpdated==true)
        {
            StatesUpdated = false;
            SetStates();
        }
    }

    public void SetStates()
    {
        SpawnTimeCounter = 0f;
        
        _FlyWeapon.MoveSpeed = WeaponStatesList[WeaponLevel].Speed;
        _WeaponDamage.damage = WeaponStatesList[WeaponLevel].Damage;
        
        WeaponRange = WeaponStatesList[WeaponLevel].Range;
        TimeBetweenSpawn = WeaponStatesList[WeaponLevel].IntervalTime;
        ShotNumber = (int)WeaponStatesList[WeaponLevel].Amount;
        
        PenetrateNum=(int)WeaponStatesList[WeaponLevel].Amount;

        //用时间代替穿透数


    }


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag=="Enemy")
        {
            PenetrateNum -= 1;
        }
    }
}
