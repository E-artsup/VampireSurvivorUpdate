using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region Variables

        #region Components

        [Header("Components")]
        public TextMeshProUGUI timerText;

        #endregion

        #region Settings
        
        [Header("Timer Settings.")]
        [Tooltip("Current time of the timer.")] public float currentTime;
        [Tooltip("Make the timer count backwards.")] public bool countDown;
        
        [Header("Limit Settings")]
        [Tooltip("Set a limit to the timer.")] public bool hasLimit = false;
        [Tooltip("Limit of the timer.")] public float timerLimit;
        [Tooltip("Set the color when the timer reach the limit.")] public Color limitColor = Color.red;
        
        #endregion

    #endregion
    

    /// <summary>
    /// Check if timer is a countDown, if yes, 
    /// </summary>
    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = limitColor;
        }
        
        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString("00:00");
    }
}
