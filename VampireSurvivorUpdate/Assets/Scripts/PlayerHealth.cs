using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    private PlayerStats playerStats;
    
    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    /// <summary>
    /// Make the player's health decrease of a certain amount of damage
    /// </summary>
    /// <param name="damageAmount"></param>
    private void TakeDamage(float damageAmount)
    {
        playerStats.currentHealth -= damageAmount; //Maybe subtract armor to damage here
        
        if (playerStats.currentHealth >0)   //If player's health is below 0 then print "Game Over" in the Console
            return;
        
        Debug.Log("Game Over");
    }
}