using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //======
    //VARIABLE
    //======
    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;
    [SerializeField]private float chipSpeed = 5f;
    [SerializeField] private Animation damagedAnimation;
    [SerializeField] private GameOver gameOverUI;
    private PlayerStats playerStats;
    private float lerpTimer = 0;

    //============
    //MONOBEHAVIOUR
    //============
    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }
    private void Update()
    {
        ManageUIHealthBar();
    }

    //============
    //FONCTION
    //============
    /// <summary>
    /// Make the player's health decrease of a certain amount of damage
    /// </summary>
    /// <param name="damageAmount"></param>
    public void TakeDamage(float damageAmount)
    {
        playerStats.currentHealth -= damageAmount; //Maybe subtract armor to damage here
        if (!damagedAnimation.isPlaying) damagedAnimation.Play();

        if (playerStats.currentHealth >0)   //If player's health is below 0 then print "Game Over" in the Console
            return;

        gameOverUI.gameObject.SetActive(true);

        UnityEngine.Debug.Log("Game Over");
    }
    /// <summary>
    /// Manager the fill amount of the health bar
    /// </summary>
    private void ManageUIHealthBar()
    {

        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = playerStats.currentHealth / playerStats.maxHealth;
        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        Color healthColor = Color.Lerp(Color.red, Color.green, hFraction);
        frontHealthBar.color = healthColor;
    }
}