using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIControl : MonoBehaviour
{
    private static UIControl instance;

    public static UIControl Instance
    {
        get
        {
            if (instance==null)
            {
                instance=FindObjectOfType<UIControl>() as UIControl;
            }
            return instance;
        }
    }
    
    
    public TMP_Text LevelText;
    public Slider ExperienceSlider;

    public LevelupChoose[] LvlupButtons;//升级选项

    public GameObject LevelupPanel;
    
    public void UpgradeLevel()
    {
        LevelText.text =("Level "+ExperienceControl.Instance.CurrentLevel).ToString();
    }
    public void UpgradeExperience()
    {
        ExperienceSlider.maxValue = ExperienceControl.Instance.ExpLevels[ExperienceControl.Instance.CurrentLevel];
        ExperienceSlider.value = ExperienceControl.Instance.CurrentExperinece;
    }
    
    public void SkipUpgrade()
    {
        LevelupPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    
    
}
