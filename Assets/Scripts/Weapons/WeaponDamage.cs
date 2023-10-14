using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponDamage : MonoBehaviour
{
    public float damage;
    public float SurviveTime,GrowSpeed;
    private Vector3 TargetSize;
    
    public bool ifDestoryParent;

    /// <summary>
    /// 持续伤害
    /// </summary>
    public bool ifDamageOverTime;
    public float TimeBetweenDamage;
    ///<summary>Time Between Damage Counter </summary>
    private float TBDcounter;
    //统计进入范围的敌人
    private List<EnemyControl> EnemyList = new List<EnemyControl>();


    /// <summary>
    /// 设置击退效果
    /// </summary>
    public bool ifKnockBackEnemy;
    public float SetHitStrength;
    private float currentHitStrength;

    /// <summary>
    /// 设置是否命中后销毁自身
    /// </summary>
    public bool ifDestorySelf;
    
    private void Start()
    {
        TargetSize = transform.localScale;
        transform.localScale = Vector3.zero;
        currentHitStrength = SetHitStrength;
        TBDcounter = TimeBetweenDamage;
    }

    private void Update()
    {
        //火球逐渐出现和消失
        transform.localScale = Vector3.MoveTowards(transform.localScale,TargetSize, GrowSpeed * Time.deltaTime);
        SurviveTime -= Time.deltaTime;
        if (SurviveTime<=0)
        {
            TargetSize = Vector3.zero;
            if (transform.localScale.x==0f)
            {
                Destroy(gameObject);
                if (ifDestoryParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
            
            
            
        }

        if (ifDamageOverTime==true)
        {
            if (TBDcounter>0)
            {
                TBDcounter -= Time.deltaTime;
            }
            else
            {
                TBDcounter = TimeBetweenDamage;
                for (int i = 0; i < EnemyList.Count; i++)
                {
                    if (EnemyList[i]!=null)
                    {
                        EnemyList[i].TakeDamage(damage,ifKnockBackEnemy,currentHitStrength);
                    }
                    else
                    {
                        EnemyList.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (ifDamageOverTime!=true)
        {
            //非持续伤害
            if (col.tag=="Enemy")
            {
                col.GetComponent<EnemyControl>().TakeDamage(damage,ifKnockBackEnemy,currentHitStrength);

                if (ifDestorySelf==true)
                {
                    Destroy(gameObject);
                }
                //Debug.Log("hit");
            }
        }
        else
        {
            //持续伤害
            if (col.tag=="Enemy")
            {
                EnemyList.Add(col.GetComponent<EnemyControl>());
            }
        }
        
    }


    public void OnTriggerExit2D(Collider2D other)
    {
        if (ifDamageOverTime==true)
        {
            if (other.tag=="Enemy")
            {
                EnemyList.Remove(other.GetComponent<EnemyControl>());
            }
        }
    }
}
