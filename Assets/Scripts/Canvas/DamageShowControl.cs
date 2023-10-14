using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageShowControl : MonoBehaviour
{
    public static DamageShowControl instance;

    public static DamageShowControl Instance
    {
        get
        {
            if (instance==null)
            {
                instance=FindObjectOfType<DamageShowControl>() as DamageShowControl;
            }
            return instance;
        }
    }

    public DamageNumber NumberToSpawm;
    public Transform NumberCanvas;

    private List<DamageNumber> NumberPool = new List<DamageNumber>();

    /// <summary>
    /// 显示玩家造成的伤害数字
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="location"></param>
    public void ShowDamage(float damage,Vector3 location)
    {
        int i = Mathf.RoundToInt(damage);
        //DamageNumber newdamage = Instantiate(NumberToSpawm,location, Quaternion.identity,NumberCanvas);
        DamageNumber newdamage = GetFromPool();
        newdamage.SetDamageShow(i);
        newdamage.gameObject.SetActive(true);
        newdamage.gameObject.transform.position = location;
    }

    public DamageNumber GetFromPool()
    {
        DamageNumber NumberToOutput = null;

        if (NumberPool.Count==0)
        {
            NumberToOutput = Instantiate(NumberToSpawm, NumberCanvas);
        }
        else
        {
            NumberToOutput = NumberPool[0];
            NumberPool.RemoveAt(0);
        }
        return NumberToOutput;
    }

    public void GoBackPool(DamageNumber num)
    {
        num.gameObject.SetActive(false);
        NumberPool.Add(num);
    }
    
}

