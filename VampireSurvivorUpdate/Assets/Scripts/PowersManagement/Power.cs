using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{

#region private variables
    [SerializeField]
    [Tooltip("The power data object containing informations about the power")]
    protected PowerData powerData;
    [SerializeField]
    [Tooltip("Sound chen the attack is launch")]
    protected AudioSource attackSound;
    [SerializeField]
    protected int currentLevel = 1;
    protected float cooldownRemaining;
    #endregion

    #region Getters and setters
    //<summary> Get the remaining time of the power's cooldown </summary>
    //<returns> Remaining time of the power's cooldown </returns>
    public float getRemainingCooldown()
    {
        return cooldownRemaining;
    }

    //<summary> Get the power data </summary>
    public PowerData PowerData { get => powerData; }
    public int GetCurrentLevel { get { return currentLevel; }}
    public bool IsMaxLevel { get { return currentLevel >= powerData.MaxLevel; } }
    #endregion
    #region Private methods
    private void Awake()
    {
        ResetLevel();
    }
    //<summary> Code to execute every frame </summary>
    private void FixedUpdate(){
        // If the cooldown is not finished
        if(cooldownRemaining > 0){
            // Decrease the cooldown
            cooldownRemaining -= Time.deltaTime;
        }
    }
    #endregion

    #region public methods
    //<summary> Method to attack, please override it when creating an instance of this class </summary>
    public virtual void Attack() { }

    //<summary> Method to level up the power </summary>
    public void LevelUp()
    {
        // Increase the current level
        currentLevel++;
        print("Level up " + this.name + " Current Level = " + currentLevel);
    }
    /// <summary>
    /// Set Current Level to 1
    /// </summary>
    public void ResetLevel()
    {
        currentLevel = 1;
    }
    #endregion

    public int CurrentLevel { set => currentLevel = value; }

    //#region sub-classes
    public enum DamageTypeZone{
        Unique,
        Area
    }
//#endregion
}

public enum DamageTypeZone
{
    Area,
    Projectile
}