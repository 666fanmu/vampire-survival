using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelupChoose : MonoBehaviour
{
    public Text WeaponDescriptionText, WeaponNameText;
    public Image WeaponImage;

    private Weapon assignedWeapon;

    public void UpdateButtonText(Weapon weapon)
    {
        assignedWeapon = weapon;
        if (weapon.gameObject.activeSelf==true)
        {
            WeaponDescriptionText.text = weapon.WeaponStatesList[weapon.WeaponLevel].UpgradeText;
            WeaponImage.sprite = weapon.WeaponSprite;
            WeaponNameText.text = weapon.name + " lvl " +( weapon.WeaponLevel+1).ToString();
            
        }
        else
        {
            WeaponDescriptionText.text = "Unlock " + weapon.name;
            WeaponImage.sprite = weapon.WeaponSprite;
            WeaponNameText.text = weapon.name;
            
        }
    }

    public void SelectUpgrade()
    {
        if (assignedWeapon != null)
        {
            if (assignedWeapon.gameObject.activeSelf == true)
            {
                assignedWeapon.LevelUp();
                
            }
            else
            {
                PlayerControl.Instance.AddWeapon(assignedWeapon);
            }

            UIControl.Instance.LevelupPanel.SetActive(false);
            Time.timeScale = 1f;
                        
        }
    }

    
}
