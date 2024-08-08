using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] Weapon[] initialWeaponPrefabs;
    [SerializeField] Transform[] weaponAttachSlotArray;
    [SerializeField] Transform defaultWeaponAttachSlot;

    List<Weapon> weapons ;

    int currentWeaponIndex = -1;

    private void Start()
    {
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        weapons = new List<Weapon>();
        foreach (var weaponPrefab in initialWeaponPrefabs)
        {
            Transform weaponSlot = defaultWeaponAttachSlot;
            foreach (Transform slot in weaponAttachSlotArray)
            {
                if (slot.gameObject.tag == weaponPrefab.GetAttachSlotTag() )
                {
                    weaponSlot = slot;
                    
                }
            }
            Weapon newWeapon = Instantiate(weaponPrefab, weaponSlot );
            newWeapon.Init(gameObject);
            weapons.Add(newWeapon);
            
        }
        NextWeapon();
    }

    public void NextWeapon()
    {
        int nextWeaponIndex = currentWeaponIndex + 1;
        if (nextWeaponIndex >= weapons.Count)
        {
            nextWeaponIndex = 0;
        }
        EquipWeapon(nextWeaponIndex);
    }
    internal Weapon GetActiveWeapon()
    {
        return weapons[currentWeaponIndex];
    }
    private void EquipWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weapons.Count)
        {
            return;
        }
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
        {
            weapons[currentWeaponIndex].Unequip();
        }

        weapons[weaponIndex].Equip();
        currentWeaponIndex = weaponIndex;
    }

   
}
