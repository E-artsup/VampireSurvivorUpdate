using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Managers;


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
    private bool isThePlayerDead = false;


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
        //Regen
        playerStats.currentHealth += playerStats.regenRate * Time.deltaTime;
        playerStats.currentHealth = Mathf.Clamp(playerStats.currentHealth, 0, playerStats.maxHealth);

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
        if (isThePlayerDead) return;
        gameOverUI.gameObject.SetActive(true);

        //No TimeScale = 0, because Animation
        //This do many error but it work
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        InputManager.instance = null;
        FindObjectOfType<TimeManager>().enabled = false;
        FindObjectOfType<WaveSystem>().gameObject.SetActive(false);
        foreach(AIBehavior ennemiScript in FindObjectsOfType<AIBehavior>())
        {
            ennemiScript.canMove = false;
        }

        //Screenshake for gamefeel
        CinemachineImpulseSource screenshake = gameOverUI.gameObject.GetComponent<CinemachineImpulseSource>();
        screenshake.GenerateImpulseWithVelocity(Vector3.one);
        UnityEngine.Debug.Log("Game Over");

        //No double screenshake
        isThePlayerDead = true;

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
        else if(fillB < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            frontHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }


        Color healthColor = Color.Lerp(Color.red, Color.green, hFraction);
        frontHealthBar.color = healthColor;
    }
}