using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<WeaponStates> WeaponStatesList;
    public int WeaponLevel;
    
    [HideInInspector]
    public bool StatesUpdated;

    public Sprite WeaponSprite;
    

    public void LevelUp()
    {
        
        if (WeaponLevel<WeaponStatesList.Count-1)
        {
            WeaponLevel += 1;
            StatesUpdated = true;
        }
    }
    
    
}

[System.Serializable]

public class WeaponStates
{   
    public float Speed,
        Damage,
        Range,//范围
        IntervalTime,//冷却时间
        Amount,//数量
        SurviveTime;//持续时间

    public string UpgradeText;
}
