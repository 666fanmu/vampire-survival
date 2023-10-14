using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyControl : MonoBehaviour
{
    public Rigidbody2D body;

    public float SetMoveSpeed;
    private float CurrentMoveSpeed;
    public float Damage;

    /// <summary>
    /// 冷却时间
    /// </summary>
    public float HitWaitTime = 1f;
    private float HitCounter;
    
    
    public Transform Targrt;//player

    public float EnemyHealth;

    /// <summary>
    /// 击退效果
    /// </summary>
    public float KnockBackTime;
    private float KnockBackTimeCounter;

    public int Exp=1;
    

    private void Awake()
    {
        Targrt = FindObjectOfType<PlayerControl>().transform;
    }

    private void Start()
    {
        CurrentMoveSpeed = SetMoveSpeed;
    }


    void Update()
    {
        body.velocity = (Targrt.position - transform.position).normalized * CurrentMoveSpeed;
        if (HitCounter>0)
        {
            HitCounter -= Time.deltaTime;
        }

        if (KnockBackTimeCounter>0)
        {
            KnockBackTimeCounter -= Time.deltaTime;
        }
        else
        {
            if (CurrentMoveSpeed<0)
            {
                CurrentMoveSpeed = SetMoveSpeed;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)//敌人对玩家造成伤害并冷却
    {
        if (col.gameObject.tag=="Player"&&HitCounter<=0)
        {
            PlayerHealth.Instance.GetDamage(Damage);
            HitCounter = HitWaitTime;
        }
        
       
    }

    public void TakeDamage(float damage)//玩家对敌人造成伤害
    {
        EnemyHealth -= damage;
        if (EnemyHealth<=0)
        {
            Destroy(gameObject);
            ExperienceControl.Instance.SpawnExpPickup(transform.position,Exp);
        }
        DamageShowControl.Instance.ShowDamage(damage,transform.position);
    }
    
    public void TakeDamage(float damage,bool ifKnockBack,float HitStrength)
    {
        TakeDamage(damage);
        if (ifKnockBack)
        {
            KnockBackTimeCounter = KnockBackTime;
            if (CurrentMoveSpeed>0)
            {
                CurrentMoveSpeed = -CurrentMoveSpeed*HitStrength;
            }
            
        }
    }
    
}
