using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerControl : MonoBehaviour
{

    public static PlayerControl Instance;

    public float PlayerMoveSpeed = 3f;

    public Animator anim;//移动动画

    public GameObject Pickup;
    public float PickupRange = 1f;

    public List<Weapon> UnassignedWeapons, AssingedWeapons;

    public int maxWeapons;//最多拥有多少武器 
    private void Awake()
    {
       Instance =this;
    }

    private void Start()
    {
        if (AssingedWeapons.Count==0)
        {
            AddWeapon(Random.Range(0,UnassignedWeapons.Count));
        }
    }


    void Update()
    {
        Vector3 MoveInput = new Vector3(0f, 0f, 0f);
        MoveInput.x = Input.GetAxisRaw("Horizontal");
        MoveInput.y = Input.GetAxisRaw("Vertical");
       // Debug.Log(MoveInput);
       MoveInput.Normalize();
        transform.position += MoveInput * PlayerMoveSpeed * Time.deltaTime;

        if (MoveInput!=Vector3.zero)
        {
            anim.SetBool("IsMoving",true);
        }
        else
        {
            anim.SetBool("IsMoving",false);
        }

        
    }
    //设置玩家拾取物品的范围
    public void SetPickupRange()
    {
        Pickup.transform.localScale = Vector3.one * PickupRange;
    }

    public void AddWeapon(int WeaponNumber)
    {
        if (WeaponNumber<UnassignedWeapons.Count)
        {
            AssingedWeapons.Add(UnassignedWeapons[WeaponNumber]);
            UnassignedWeapons[WeaponNumber].gameObject.SetActive(true);
            UnassignedWeapons.RemoveAt(WeaponNumber);
        }
    }
    
    public void AddWeapon(Weapon weapon)
    {
        weapon.gameObject.SetActive(true);
        AssingedWeapons.Add(weapon);
        UnassignedWeapons.Remove(weapon);
    }
    
}
