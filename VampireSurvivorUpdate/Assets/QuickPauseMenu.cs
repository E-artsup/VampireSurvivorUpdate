using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickPauseMenu : MonoBehaviour
{
    public Transform uiSettings; 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && PowersManager.instance.playerStats.currentHealth > 0.5f)
        {
            if (!uiSettings.gameObject.activeInHierarchy)
            {
                uiSettings.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                SoundManager.instance.stopSound("Character_Movement");
                EscapePauseMenu();
            }
        }
    }
    public void EscapePauseMenu()
    {
        uiSettings.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
