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
    private int actualLevelOfTheUpdate = 0;
    public int maxLevelOfTheUpdate = 0;

    [Header("Weapon Ref")]
    [SerializeField] private GameObject prefabsOfTheWeapon;
    [SerializeField] private UnityEvent UpdateFonction;

    [SerializeField] private GameObject instanceInTheScene = null;

    //===========
    //FONCTION
    //===========

    /// <summary>
    /// Spawn the Weapon in the scene and Get a ref
    /// </summary>
    public void AddTheWeaponToTheSceneAndTheInventory()
    {
        if(IsThiwWeaponInTheScene() && actualLevelOfTheUpdate > 1)
        {
            //Get PowerManager Instance
            //get power related
            //Level it up
        }
        else if(actualLevelOfTheUpdate == 0)
        {
            //We add the Weapon to the Inventory
            InventoryManager.AddWeaponToInventory(this, speciality);
            //Get PowerManager Instance
            //Spawn power related
            actualLevelOfTheUpdate++;
        }
    }
    /// <summary>
    /// Return the level of the weapon
    /// </summary>
    /// <returns></returns>
    public int GetCurrentLevelOfTheWeapon()
    {
        //Get PowerManager 
        //Get weapon related
        //Get level
        return actualLevelOfTheUpdate;
    }
    /// <summary>
    /// Return true if the weapon is level max
    /// </summary>
    /// <returns></returns>
    public bool IsTheWeaponMaxLevel()
    {
        /*if (GetCurrentLevelOfTheWeapon() >= maxLevelOfTheUpdate)
        {
            return true;
        }
        else*/
            return false;
    }
    /// <summary>
    /// Did the spell spawn in the Scene
    /// </summary>
    public bool IsThiwWeaponInTheScene()
    {
        return false;
    }
}

public enum WeaponSpeciality
{
    Weapon, Passif
}