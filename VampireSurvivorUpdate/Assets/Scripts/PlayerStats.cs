using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Tooltip("Player's max health.")]public float maxHealth;
    [Tooltip("Player's current health.")]public float currentHealth;
    [Tooltip("Player's regen rate.")]public float regenRate;
    [Tooltip("Player's armor. Substract damages from the enemies attacks.")]public float armor;
    [Tooltip("Player's move speed.")] public float moveSpeed;
    
    [Tooltip("Player's power. Apply a bonus percentage to the damages.")]public float power;
    [Tooltip("Player's projectile bonus speed.")]public float projectileSpeed;
    [Tooltip("Player's projectile's effects life span")]
    public float effectLifeSpan;
    [Tooltip("Player's attack range.")]public float attackRange;
    
    [Tooltip("Player's attack cooldown.")]public float attackCooldown;
    [Tooltip("Number of projectiles fired per attack.")]public int projectilesPerAttack;
    [Tooltip("Number of resurections per game.")]public int resurections;
    [Tooltip("The range in which the player can grab items.")]public float grabRange;
    
    [Tooltip("Player's luck. Apply a bonus percentage to the drop rate.")]public float luck;
    [Tooltip("Player's percentage of bonus experience.")]public float bonusExp;
    [Tooltip("Player's percentage of bonus gold.")]public float bonusGold;
    [Tooltip("Stats that increase the speed, the health, the quantity and the spawn rates of ennemies")]
    public float curse;
    
    [Tooltip("Player can reroll two times the stuff he drops every level")]public bool reroll;
    [Tooltip("Player can have xp instead of stuffs two times per rank each rank up")]public bool xpInsteadOfStuff;
    [Tooltip("Player can ban an object for the game")]public bool banObject;
    
    
    [Tooltip("Gold amount of the player.")]public int gold;
    [Tooltip("Experience amount of the player.")]public int xp;
    [Tooltip("Limit of experience that the player can reach before leveling up.")]public int maxXp;
    [Tooltip("Level of the player.")]public int level;
    [Tooltip("Number of kills of the player.")]public int kills;
    
    

}
