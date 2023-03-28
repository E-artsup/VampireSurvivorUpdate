using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuUIManager : MonoBehaviour
    {
        
        #region Variables

            #region Components

            private GameObject mainGo;
            private GameObject settingsGo;
            private GameObject powerUpGo;
            private GameObject creditsGo;
            
            [SerializeField] private GameObject goldCounter;
            private TextMeshProUGUI goldText;

            [SerializeField] private GoldCounter_SO goldCounterSo;

            #endregion
        
            
        
        #endregion

        #region Initialization
        
        /// <summary>
        /// Initialize all the references.
        /// </summary>
        private void Awake()
        {
            mainGo = transform.GetChild(0).gameObject;
            settingsGo = transform.GetChild(1).gameObject;
            powerUpGo = transform.GetChild(2).gameObject;
            creditsGo = transform.GetChild(3).gameObject;

            if(goldCounter == null) goldCounter = mainGo.transform.GetChild(1).GetChild(0).gameObject;
            goldText = goldCounter.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            
            UpdateGoldAccumulated();
        }

        #endregion

        #region Functions
        /// <summary>
        /// Load the game scene
        /// </summary>
        public void Play()
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }
        
        /// <summary>
        /// Quit the game
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }
        
        /// <summary>
        /// Open the Settings menu
        /// </summary>
        public void Settings()
        {
            settingsGo.SetActive(true);
            mainGo.SetActive(false);
        }
        
        /// <summary>
        /// Open the PowerUp menu
        /// </summary>
        public void PowerUp()
        {
            powerUpGo.SetActive(true);
            mainGo.SetActive(false);
        }
        
        /// <summary>
        /// Open the credits
        /// </summary>
        public void Credits()
        {
            creditsGo.SetActive(true);
            mainGo.SetActive(false);
        }

        public void Return()
        {
            powerUpGo.SetActive(false);
            settingsGo.SetActive(false);
            creditsGo.SetActive(false);
            mainGo.SetActive(true);
        }

        public void UpdateGoldAccumulated()
        {
            goldText.text = goldCounterSo.goldAccumulated.ToString("0");
        }
        #endregion
        
    }
    
}
