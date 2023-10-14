using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageNumber: MonoBehaviour
{
    public TMP_Text DamageText;
    public float LifeTime;
    private float LiftTimeCounter;

    public float FloatSpeed=1f;

    private void Start()
    {
        LiftTimeCounter = LifeTime;
    }

    private void Update()
    {
        if (LiftTimeCounter>0)
        {
            LiftTimeCounter -= Time.deltaTime;
            if (LiftTimeCounter<=0)
            {
                DamageShowControl.Instance.GoBackPool(this);
            }
        }

        transform.position += Vector3.up * FloatSpeed * Time.deltaTime;
    }

    public void SetDamageShow(int damage)
    {
        LiftTimeCounter = LifeTime;
        DamageText.text = damage.ToString();
    }
}
