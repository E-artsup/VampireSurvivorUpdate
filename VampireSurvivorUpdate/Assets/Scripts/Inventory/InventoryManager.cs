using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add new weapon to the UI and keep a ref to them
/// </summary>
public static class InventoryManager
{
    //===========
    //VARIABLE
    //===========

    private static InventoryUIViewport inventoryUI;
    private static List<WeaponSC> allWeaponInInventory = new();
    private static List<WeaponSC> allPassifInInventory = new();
    private static int passifInventoryLimit = 6;
    private static int weaponInventoryLimit = 6;

    //===========
    //FONCTION
    //===========
    public static void AddWeaponToInventory(WeaponSC newWeapon, WeaponSpeciality specialty)
    {
        if (specialty == WeaponSpeciality.Weapon)
        {
            inventoryUI.AddWeaponToTheUI(newWeapon);
            allWeaponInInventory.Add(newWeapon);
        }
        else 
        { 
            inventoryUI.AddPassifToTheUI(newWeapon);
            allPassifInInventory.Add(newWeapon);
        }
        Debug.Log(newWeapon.nameOfWeapon + " is added to the inventory");
    }
    public static void SetInventoryUIViewport(InventoryUIViewport newInventoryUI)
    {
        inventoryUI = newInventoryUI;
    }
    /// <summary>
    /// Return true if the slot for this speciality is full
    /// </summary>
    /// <returns></returns>
    public static bool IsInventoryOfThisSpecialityComplete(WeaponSpeciality speciality)
    {
        bool value;
        if (speciality == WeaponSpeciality.Weapon)
        {
            value = allWeaponInInventory.Count >= weaponInventoryLimit;
        }
        else
        {
            value = allPassifInInventory.Count >= passifInventoryLimit;
        }

        return value;
    }
    /// <summary>
    /// Clear the static inventory of the weapon
    /// </summary>
    public static void ClearInventory()
    {
        allWeaponInInventory.Clear();
        allPassifInInventory.Clear();
    }
}
