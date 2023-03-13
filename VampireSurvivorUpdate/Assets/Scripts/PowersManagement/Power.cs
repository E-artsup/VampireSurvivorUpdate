using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{

//#region private variables
    protected PowersManager powersManager;

    private Material icon;
    private new string name;
    private int type;
        // 0 = unknown
        // 1 = Fire
        // 2 = Water
        // 3 = Air
        // 4 = Lightning
        // 5 = Light
        // 6 = Darkness
    private string description;
    protected int maxLevel;
    protected int currentLevel;
    protected int baseDamage;
    protected int levelDamageMultiplier;
    private DamageTypeZone damageTypeZone;
    protected float projectileSpeed;
    protected int maxFireAmountAtSameTime;
    protected float duration;
    protected float cooldown;
    protected float cooldownRemaining;
    protected float hitBoxDelay;
    private bool piercing;
    protected float knockback;
    protected int maxOnScreenAtSameTime;
    private bool blockedByWalls;
//#endregion
//#region public variables
//#endregion

//#region constructor
    //<summary> Constructor of the Power class </summary>
    //<param name="name"> Name of the power </param>
    //<param name="type"> Type of the power </param>
    //<param name="description"> Description of the power </param>
    //<param name="maxLevel"> Max level of the power </param>
    //<param name="baseDamage"> Base damage of the power </param>
    //<param name="levelDamageMultiplier"> Damage multiplier per level of the power </param>
    //<param name="damageTypeZone"> Type of damage zone of the power </param>
    //<param name="projectileSpeed"> Speed of the projectile of the power </param>
    //<param name="maxFireAmountAtSameTime"> Max amount of projectiles that can be fired at the same time </param>
    //<param name="duration"> Duration of the power </param>
    //<param name="cooldown"> Cooldown of the power </param>
    //<param name="hitBoxDelay"> Delay before the hitbox of the power appears </param>
    //<param name="piercing"> If the power can pierce through enemies </param>
    //<param name="knockback"> Knockback's strength </param>
    //<param name="maxOnScreenAtSameTime"> Max amount of projectiles that can be on screen at the same time </param>
    //<param name="blockedByWalls"> If the power is blocked by walls </param>
    public Power(
        string name,
        int type,
        string description,
        int maxLevel,
        int baseDamage,
        int levelDamageMultiplier,
        DamageTypeZone damageTypeZone,
        float projectileSpeed,
        int maxFireAmountAtSameTime,
        float duration,
        float cooldown,
        float hitBoxDelay,
        bool piercing,
        float knockback,
        int maxOnScreenAtSameTime,
        bool blockedByWalls
    ){
        this.name = name;
        this.type = type;
        this.description = description;
        this.maxLevel = maxLevel;
        this.baseDamage = baseDamage;
        this.levelDamageMultiplier = levelDamageMultiplier;
        this.damageTypeZone = damageTypeZone;
        this.projectileSpeed = projectileSpeed;
        this.maxFireAmountAtSameTime = maxFireAmountAtSameTime;
        this.duration = duration;
        this.cooldown = cooldown;
        this.hitBoxDelay = hitBoxDelay;
        this.piercing = piercing;
        this.knockback = knockback;
        this.maxOnScreenAtSameTime = maxOnScreenAtSameTime;
        this.blockedByWalls = blockedByWalls;
    }

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

    public enum DamageTypeZone{
        Unique,
        Area
    }
//#endregion
}
