using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Create A New Weapon")]

public class WeaponSC : ScriptableObject
{
    //===========
    //VARIABLE
    //===========

    [Header("Weapon Info")]
    public Sprite iconImage;
    public string nameOfWeapon;
    public string descriptionOfTheWeapon;
    /// <summary>
    /// Give The Description of the next Level 
    /// </summary>
    public string descriptionOfTheLevelUp;
    public WeaponSpeciality speciality;

    [Header("Weapon Ref")]
    [SerializeField] private Power prefabsOfTheWeapon;

    //===========
    //FONCTION
    //===========

    /// <summary>
    /// Spawn the Weapon in the scene and Get a ref
    /// </summary>
    public void AddTheWeaponToTheSceneAndTheInventory()
    {
        if(IsThisWeaponInTheScene(out Power power))
        {
            power.LevelUp();
            return;
        }
        else
        {
            //We add the Weapon to the Inventory
            InventoryManager.AddWeaponToInventory(this, speciality);
            //We spawn the Power In The Scene
            Instantiate(prefabsOfTheWeapon.gameObject);
            //We give a ref to the power manager
            PowersManager.instance.RegisterPower(prefabsOfTheWeapon);
        }
    }
    /// <summary>
    /// Return true if the weapon is level max
    /// </summary>
    /// <returns></returns>
    public bool IsTheWeaponMaxLevel()
    {
        if(IsThisWeaponInTheScene(out Power power))
        {
            if(power.PowerData.MaxLevel >= 0)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Did the spell spawn in the Scene
    /// </summary>
    public bool IsThisWeaponInTheScene(out Power searchedPower)
    {
        //List<Power> powersList = PowersManager.instance.getPowers();
        Power[] powersList = FindObjectsOfType<Power>();

        searchedPower = null;
        foreach(Power power in powersList)
        {
            if (power.GetType() == prefabsOfTheWeapon.GetType())
            {
                searchedPower = power;
            }
        }
        return searchedPower != null;
    }
}

public enum WeaponSpeciality
{
    Weapon, Passif
}