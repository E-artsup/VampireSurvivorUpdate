using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsMenuManager : MonoBehaviour
{
        
        #region Variables
    
        
        [Header("Audio Section")]
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AudioMixerGroup mixeGroup;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;
        
        [Header("Graphic Section")]
        [SerializeField] private Toggle fullscreenToggle;

        private const string MIXER_MUSIC = "Music";
        private const string MIXER_SFX = "SFX";

        #endregion

        #region Initialize

        private void Awake()
        {
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }

        #endregion
        
        #region Functions

        private void SetMusicVolume(float value)
        {
            mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
        }

        private void SetSFXVolume(float value)
        {
            mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
        }
        
        private void SetFullscreen(bool value)
        {
            Screen.fullScreen = value;
        }

        #endregion

}

