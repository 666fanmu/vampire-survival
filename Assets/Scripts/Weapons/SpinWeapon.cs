using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpinWeapon : Weapon
{
    public float RotateSpeed;
    public Transform Rotater;
    public Transform FireBallToSpawn;

    public float TimeBetweenSpawn;//冷却时间
    private float timeBetweenSpawnCounter;

    public WeaponDamage _weaponDamage;
    
    private void Start()
    {
        SetStates();

        //UIControl.Instance.LvlupButtons[0].UpdateButtonText(this);
    }

    private void Update()
    {
        Rotater.transform.rotation = Quaternion.Euler(0f,
            0f,
            Rotater.transform.rotation.eulerAngles.z+(WeaponStatesList[WeaponLevel].Speed* RotateSpeed * Time.deltaTime));

        
        timeBetweenSpawnCounter -= Time.deltaTime;
        if (timeBetweenSpawnCounter<=0)
        {
            timeBetweenSpawnCounter = TimeBetweenSpawn;
            /*
             Instantiate(FireBallToSpawn, FireBallToSpawn.transform.position,
                FireBallToSpawn.transform.rotation,
                Rotater).gameObject.SetActive(true);
            */
            for (int i = 0; i < WeaponStatesList[WeaponLevel].Amount; i++)
            {
                float rot = (360 / WeaponStatesList[WeaponLevel].Amount)*i;
                Instantiate(FireBallToSpawn, FireBallToSpawn.transform.position,
                    Quaternion.Euler(0f,0f,rot),
                    Rotater).gameObject.SetActive(true);
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
        timeBetweenSpawnCounter = 0f;
        
        _weaponDamage.damage = WeaponStatesList[WeaponLevel].Damage;
        
        transform.localScale = Vector3.one * WeaponStatesList[WeaponLevel].Range;
        
        TimeBetweenSpawn = WeaponStatesList[WeaponLevel].IntervalTime;
        
        _weaponDamage.SurviveTime = WeaponStatesList[WeaponLevel].SurviveTime;
        
    }
}
