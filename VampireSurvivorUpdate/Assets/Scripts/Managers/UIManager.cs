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

    private void Awake()
    {
        instance = this;
        
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        if (expBar == null)
        {
            transform.GetChild(0);
        }

        fillBar = expBar.transform.GetChild(0).GetComponent<Image>();
        levelText = expBar.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        goldImage = goldCounter.transform.GetChild(1).GetComponent<Image>();
        goldText = goldCounter.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

    }

    private void Start()
    {
        UpdateExpBar();
        UpdateGoldCounter();
    }

    #region Experience Functions

    public void UpdateExpBar()
    {
        
        fillBar.fillAmount = (float) playerStats.xp / playerStats.maxXp;
        UpdateLevelText();
        
    }

    private void UpdateLevelText()
    {

        levelText.text = levelPrefixText + playerStats.level;

    }

    #endregion

    #region Gold Functions

    public void UpdateGoldCounter()
    {

        goldText.text = playerStats.gold.ToString("0");

    }

    #endregion
}
