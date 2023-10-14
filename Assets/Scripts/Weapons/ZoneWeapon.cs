using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Weapon
{
   public WeaponDamage Zone;
   public float TimeBetweenSpawn;
   private float SpawnTimeCounter;
   
   private WeaponDamage _weaponDamage;

   private void Awake()
   {
      _weaponDamage = FindObjectOfType<WeaponDamage>();
   }

   private void Start()
   {
      SetStates();
   }

   private void Update()
   {
      SpawnTimeCounter -= Time.deltaTime;
      
      if (SpawnTimeCounter <= 0)
      {
         SpawnTimeCounter = TimeBetweenSpawn;

         Instantiate(Zone,Zone.transform.position,Quaternion.identity,transform).gameObject.SetActive(true);
         
      }
      
      if (StatesUpdated==true)
      {
         StatesUpdated = false;
         SetStates();
      }
   }

   //TODO
   //有bug
   private void SetStates()
   {
      SpawnTimeCounter = 0f;
      
      TimeBetweenSpawn = WeaponStatesList[WeaponLevel].IntervalTime;
      
      Zone.transform.localScale = Vector3.one * WeaponStatesList[WeaponLevel].Range;
      
      Zone.damage = WeaponStatesList[WeaponLevel].Damage;
      
      Zone.SurviveTime = WeaponStatesList[WeaponLevel].SurviveTime;

      Zone.TimeBetweenDamage = WeaponStatesList[WeaponLevel].Speed;
      
   }
}
