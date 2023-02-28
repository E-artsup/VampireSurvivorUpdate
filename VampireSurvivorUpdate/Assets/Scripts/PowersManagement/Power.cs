using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{

//#region private variables
    
    private Material icon;
    private string name;
    private ElementalType type;
    private string description;
    private int maxLevel;
    private int currentLevel;
    private int baseDamage;
    private int levelDamageMultiplier;
    private DamageTypeZone damageTypeZone;
    private float projectileSpeed;
    private int maxFireAmountAtSameTime;
    private float duration;
    private bool autoAttack;
    private float cooldown;
    private float cooldownRemaining;
    private float hitBoxDelay;
    private bool piercing;
    private bool knockback;
    private int maxOnScreenAtSameTime;
    private bool blockedByWalls;
//#endregion
//#region public variables
//#endregion
//#region public methods
    
    //<summary> Method to attack, please override it when creating an instance of this class </summary>
    public void Attack(){
        cooldownRemaining = cooldown;
    }

    //<summary> Get the remaining time of the power's cooldown </summary>
    //<returns> Remaining time of the power's cooldown </returns>
    public float getRemainingCooldown(){
        return cooldownRemaining;
    }

    //<summary> Code to execute every frame </summary>
    public void FixedUpdate(){
        // If the cooldown is not finished
        if(cooldownRemaining > 0){
            // Decrease the cooldown
            cooldownRemaining -= Time.deltaTime;
        }
    }

    //<summary> Method to level up the power </summary>
    public void LevelUp(){
        // If the power is not at max level
        if(currentLevel < maxLevel){
            // Increase the current level
            currentLevel++;
        }
    }
//#endregion

//#region sub-classes
    enum ElementalType{
        Fire,
        Water,
        Air,
        Lightning,
        Light,
        Darkness
    }

    enum DamageTypeZone{
        Unique,
        Area
    }
//#endregion
}
