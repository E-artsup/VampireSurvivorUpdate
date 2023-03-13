using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Power : MonoBehaviour
{

//#region private variables
    [SerializeField]
    [Tooltip("The power data object containing informations about the power")]
    protected PowerData powerData;
    protected int currentLevel;
    protected float cooldownRemaining;
//#endregion
//#region public variables
//#endregion

//#region constructor
    //<summary> Constructor of the Power class </summary>
    //<param name="powersManager"> PowersManager of the player </param>
   public Power(){
        if(this.powerData == null)
            throw new System.ArgumentNullException("powerData");
        currentLevel = 0;
        cooldownRemaining = 0;
    }

//#endregion

//#region Private methods
    //<summary> Code to execute every frame </summary>
    private void FixedUpdate(){
        // If the cooldown is not finished
        if(cooldownRemaining > 0){
            // Decrease the cooldown
            cooldownRemaining -= Time.deltaTime;
        }
    }
//#endregion

//#region public methods
    //<summary> Method to attack, please override it when creating an instance of this class </summary>
    public abstract void Attack();

    //<summary> Method to level up the power </summary>
    public void LevelUp(){
        // If the power is not at max level
        if(currentLevel < powerData.MaxLevel){
            // Increase the current level
            currentLevel++;
        }
    }
//#endregion

//#region Getters and setters
    //<summary> Get the remaining time of the power's cooldown </summary>
    //<returns> Remaining time of the power's cooldown </returns>
    public float getRemainingCooldown(){
        return cooldownRemaining;
    }

    //<summary> Get the power data </summary>
    public PowerData PowerData { get => powerData; }

//#endregion

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