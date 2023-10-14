using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ExperienceControl : MonoBehaviour
{

    private static ExperienceControl instance;
    public static ExperienceControl Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ExperienceControl>() as ExperienceControl;
            }

            return instance;
        }
    }

    public int CurrentExperinece;

    public ExpPickups _ExpPickups;

    public List<int> ExpLevels;
    public int CurrentLevel = 1,LevelCount;

    public List<Weapon> WeaponToUpgrade;


    private void Start()
    {
        CurrentExperinece = 0;
        UIControl.Instance.UpgradeLevel();
        UIControl.Instance.UpgradeExperience();
    }

    public void GetExperience(int experience)
    {
        CurrentExperinece += experience;
        UIControl.Instance.UpgradeExperience();
        if (CurrentExperinece>=ExpLevels[CurrentLevel])
        {
            LevelUp();
        }
    }

    public void SpawnExpPickup(Vector3 where,int Exp)
    {
        Instantiate(_ExpPickups, where, Quaternion.identity).ExpValue = Exp;
    }

    public void LevelUp()
    {
        CurrentExperinece -= ExpLevels[CurrentLevel];
        CurrentLevel+=1;
        if (CurrentLevel>=LevelCount)
        {
            CurrentLevel = ExpLevels.Count - 1;
        }
        UIControl.Instance.UpgradeLevel();
        UIControl.Instance.UpgradeExperience();

       
        UIControl.Instance.LevelupPanel.SetActive(true);
        Time.timeScale = 0f;
        
        WeaponToUpgrade.Clear();
        List<Weapon> availableWeapon = new List<Weapon>();
        availableWeapon.AddRange(PlayerControl.Instance.AssingedWeapons);

        if (availableWeapon.Count>0)
        {
            int i = Random.Range(0, availableWeapon.Count);
            WeaponToUpgrade.Add(availableWeapon[i]);
            availableWeapon.RemoveAt(i);
        }
        
        if (PlayerControl.Instance.AssingedWeapons.Count < PlayerControl.Instance.maxWeapons)
        {
            availableWeapon.AddRange(PlayerControl.Instance.UnassignedWeapons);
        }
        for (int j = WeaponToUpgrade.Count ;j<3 ;j++)
        {
            if (availableWeapon.Count>0)
            {
                int i = Random.Range(0, availableWeapon.Count);
                WeaponToUpgrade.Add(availableWeapon[i]);
                availableWeapon.RemoveAt(i);
            }
        }

        for (int i = 0; i < WeaponToUpgrade.Count; i++)
        {
            UIControl.Instance.LvlupButtons[i].UpdateButtonText(WeaponToUpgrade[i]);
        }
    }
}
