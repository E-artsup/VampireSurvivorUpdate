using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables

    public static UIManager instance;
    
        #region Components
        private PlayerStats playerStats;
        
        [Header("Experience Bar Component")] 
        public GameObject expBar;
        private Image fillBar;
        private TextMeshProUGUI levelText;
        
        [Header("Gold Counter Component")] 
        public GameObject goldCounter;
        private Image goldImage;
        private TextMeshProUGUI goldText;
        
        #endregion

        #region Settings

        [Header("Experience Bar Settings")]
        public Color expColor;
        public string levelPrefixText = "LV ";
        
        #endregion

    #endregion

    #region Initialization Functions
    
    /// <summary>
    /// Convert this script into a Singleton.
    /// Initialize all the variables necessary.
    /// playerStats = Get the PlayerStats Script on the player.
    /// 
    /// expBar = If null; Get the first child of this GameObject.
    /// fillBar = Get the first child of expBar.
    /// levelText = Get the second child of expBar.
    ///
    /// goldCounter = 
    /// goldImage = Get the first child of goldCounter.
    /// goldText = Get the second child of goldCounter.
    /// </summary>
    private void Awake()
    {
        instance = this;
        
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        if (expBar == null)
        {
            transform.GetChild(0);
        }
        
        if (goldCounter == null)
        {
            transform.GetChild(0);
        }

        fillBar = expBar.transform.GetChild(0).GetComponent<Image>();
        levelText = expBar.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        goldImage = goldCounter.transform.GetChild(1).GetComponent<Image>();
        goldText = goldCounter.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

    }

    /// <summary>
    /// Call all the functions at the start to update the HUD.
    /// </summary>
    private void Start()
    {
        UpdateExpBar(); // Call the function UpdateExpBar to sync the progress bar with the current XP of the player.
        UpdateGoldCounter(); // Call the function UpdateGoldCounter to sync the counter text with the current amount of gold.
    }
    
    #endregion
    
    #region Experience Functions
    
    /// <summary>
    /// Fill the Progress Bar with the current amount of XP.
    /// Update the text associated to the level
    /// </summary>
    public void UpdateExpBar()
    {
        
        fillBar.fillAmount = (float) playerStats.xp / playerStats.maxXp; // XP / MaxXP to get a result between 0 and 1.
        UpdateLevelText(); // Call the function UpdateLevelText to update the text with the current Level.
        
    }
    
    /// <summary>
    /// Update the level text
    /// </summary>
    private void UpdateLevelText()
    {

        levelText.text = levelPrefixText + playerStats.level; // Set the text to the current level of the player.

    }

    #endregion

    #region Gold Functions
    
    /// <summary>
    /// Update the gold counter text
    /// </summary>
    public void UpdateGoldCounter()
    {

        goldText.text = playerStats.gold.ToString("0"); // Get the player stat "gold" and convert it to string with the good format.

    }

    #endregion
}
