using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "SoundManagement/SoundData", order = 0)]
public class SoundData : ScriptableObject
{
    [SerializeField]
    [Tooltip("The name of the sound. This name will be used to register the sound in the game.")]
    private new string name;
    [SerializeField]
    [Tooltip("The group of the sound. This will change the audio mixer group of the sound.")]
    private SoundGroup soundGroup;
    [SerializeField] 
    [Tooltip("The audio clip to play.")]
    private AudioClip clip;
    [SerializeField] 
    [Tooltip("The volume of the sound.")]
    [Range(0f, 1f)] private float volume = 1f;
    [SerializeField] 
    [Tooltip("The pitch of the sound.")]
    [Range(.1f, 3f)] private float pitch = 1f;
    [SerializeField] 
    [Tooltip("Should the sound loop?")]
    private bool loop = false;
    [SerializeField] 
    [Tooltip("Should the sound play automatically when the game starts?")]
    private bool autoplay = false;
    [SerializeField] 
    [Tooltip("The delay before the sound starts playing.")]
    private float delay = 0f;
    [SerializeField] 
    [Tooltip("The sound to play after this sound.")]
    private SoundData soundToPlayAfter = null;
    private AudioSource source;
    [SerializeField]
    [Tooltip("The curve of volume that will be applied to the sound. The curve is applied to the sound when the sound is played.")]
    private AnimationCurve volumeCurve = AnimationCurve.Linear(0, 1, 1, 1);
    


    public string Name => name;
    public AudioClip Clip => clip;
    public float Volume => volume;
    public float Pitch => pitch;
    public bool Loop => loop;
    public bool Autoplay => autoplay;
    public float Delay => delay;
    public SoundData SoundToPlayAfter => soundToPlayAfter;
    public AudioSource Source { get => source; set => source = value; }
    public AnimationCurve VolumeCurve => volumeCurve;

    public enum SoundGroup
    {
        Music,
        SFX
    }
}
