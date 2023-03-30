using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField]
    [Tooltip("The list of sounds to register in the game. A sound can be registered by name calling the 'registerSound()' method.")]
    private SoundData[] _soundsDatas;
    [SerializeField]
    private AudioMixerGroup musicMixerGroup;
    [SerializeField]
    private AudioMixerGroup sfxMixerGroup;
    private Dictionary<string, SoundData> soundsDatas = new Dictionary<string, SoundData>();

    public void Awake(){
        if(instance == null){
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);


        foreach(SoundData _data in this._soundsDatas){
            _data.Source = gameObject.AddComponent<AudioSource>();
            _data.Source.clip = _data.Clip;
            _data.Source.volume = _data.Volume;
            _data.Source.pitch = _data.Pitch;
            _data.Source.loop = _data.Loop;
            _data.Source.outputAudioMixerGroup = this.sfxMixerGroup;
            this.soundsDatas.Add(_data.Name, _data);
        }
    }

    public void Start(){
        foreach(SoundData _data in this._soundsDatas)
        {
            if (!_data.Autoplay) continue;
            if(_data.Delay > 0){
                playSoundAfter(_data, _data.Delay);
            }
            else {
                this.playSound(_data);
            }
        }
    }

    public void FixedUpdate(){
        this.updateSoundsVolume();
    }

    public bool soundIsValid(SoundData soundData){
        if(soundData == null){
            UnityEngine.Debug.LogWarning("SoundManager: The sound is null.");
            return false;
        }
        if(soundData.Source == null){
            UnityEngine.Debug.LogWarning("SoundManager: The sound clip is null.");
            return false;
        }
        if(this.soundsDatas.ContainsKey(soundData.Name) == false){
            UnityEngine.Debug.LogWarning("SoundManager: The sound is not registered in the game.");
            return false;
        }
        return true;
    }

    public bool soundIsValid(string soundName){
        SoundData _soundData;
        if(!this.soundsDatas.TryGetValue(soundName, out _soundData)){
            UnityEngine.Debug.LogWarning("SoundManager: The sound is not registered in the game.");
            return false;
        }
        if(_soundData == null){
            UnityEngine.Debug.LogWarning("SoundManager: The sound is null.");
            return false;
        }
        if(_soundData.Source == null){
            UnityEngine.Debug.LogWarning("SoundManager: The sound source is null -> Don't forget to register a sound data by calling the method 'registerSound()' in the SoundManager class.");
            return false;
        }
        return true;
    }

    public SoundData getSoundData(string soundName){
        SoundData _soundData;
        if(!this.soundsDatas.TryGetValue(soundName, out _soundData)){
            UnityEngine.Debug.LogWarning("SoundManager: The sound is not registered in the game.");
            return null;
        }
        return _soundData;
    }

    public void playSound(string soundName){
        SoundData _soundData = getSoundData(soundName);
        if(!this.soundIsValid(_soundData)){
            return;
        }
        _soundData.Source.Play();
    }
    
    public void playSound(SoundData soundData){
        if(!this.soundIsValid(soundData)){
            return;
        }
        soundData.Source.Play();
    }

    public void playSoundAfter(SoundData soundData, float delay){
        if(!this.soundIsValid(soundData)){
            return;
        }
        StartCoroutine(PlaySoundAfterRoutine(soundData, delay));
    }

    public void playSoundAfter(string soundName, float delay){
        SoundData _soundData = getSoundData(soundName);
        if(!this.soundIsValid(_soundData)){
            return;
        }
        StartCoroutine(PlaySoundAfterRoutine(_soundData, delay));
    }

    private IEnumerator PlaySoundAfterRoutine(SoundData soundData, float delay){
        yield return new WaitForSeconds(delay);
        this.playSound(soundData);
    }

    // <summary> Change the volume of a sound smoothly over some time.\n WARNING ! This will unlink the sound from his curve and can cause damage ! </summary>
    // <param name="soundData">The sound to change the volume.</param>
    // <param name="targetVolume">The target volume.</param>
    // <param name="duration">The duration of the change.</param> 
    public void changeVolumeSmoothly(SoundData soundData, float targetVolume, float duration){
        if(!this.soundIsValid(soundData)){
            return;
        }
        UnityEngine.Debug.LogWarning("SoundManager: The sound "+soundData+" have been unlinked from his curve -> Used changeVolumeSmoothly on it.");
        soundData.LinkToCurve = false;
        StartCoroutine(ChangeVolumeSmoothlyRoutine(soundData, targetVolume, duration));
    }

    // <summary> Change the volume of a sound smoothly over some time.\n WARNING ! This will unlink the sound from his curve and can cause damage ! </summary>
    // <param name="soundName">The sound to change the volume.</param>
    // <param name="targetVolume">The target volume.</param>
    // <param name="duration">The duration of the change.</param> 
    public void changeVolumeSmoothly(string soundName, float targetVolume, float duration){
        SoundData _soundData = getSoundData(soundName);
        if(!this.soundIsValid(_soundData)){
            return;
        }
        UnityEngine.Debug.LogWarning("SoundManager: The sound "+_soundData+" have been unlinked from his curve -> Used changeVolumeSmoothly on it.");
        _soundData.LinkToCurve = false;
        StartCoroutine(ChangeVolumeSmoothlyRoutine(_soundData, targetVolume, duration));
    }

    private IEnumerator ChangeVolumeSmoothlyRoutine(SoundData soundData, float targetVolume, float duration){
        // set time to 0 and get the start volume
        float currentTime = 0f;
        float startVolume = soundData.Source.volume;
        while(currentTime < duration){
            // update the time and the volume
            currentTime += Time.deltaTime;
            soundData.Source.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    // <summary> Update the volume of all sounds linked to a curve. </summary>
    private void updateSoundsVolume(){
        foreach(SoundData _soundData in this.soundsDatas.Values){
            if(_soundData.LinkToCurve){
                UnityEngine.Debug.Log("Sound "+_soundData.Name+": "+(_soundData.Source.time/_soundData.Clip.length)*100+"% ("+_soundData.Source.time+" / "+_soundData.Clip.length+")");
                _soundData.Source.volume = _soundData.Volume * _soundData.VolumeCurve.Evaluate(_soundData.Source.time/_soundData.Clip.length);
                UnityEngine.Debug.Log("volume of sound "+_soundData.Name+" is now "+_soundData.Source.volume);
            }
        }
    }

    public void stopAllSounds(){
        foreach(SoundData _soundData in this.soundsDatas.Values){
            _soundData.Source.Stop();
        }
    }

    public void stopAllSoundsOfType(SoundGroup soundType){
        foreach(SoundData _soundData in this.soundsDatas.Values){
            if(_soundData.SoundGroup == soundType){
                _soundData.Source.Stop();
            }
        }
    }

    public void stopSound(SoundData soundData){
        if(!this.soundIsValid(soundData)){
            return;
        }
        soundData.Source.Stop();
    }

    public void stopSound(string soundName){
        SoundData _soundData = getSoundData(soundName);
        if(!this.soundIsValid(_soundData)){
            return;
        }
        _soundData.Source.Stop();
    }

    public void pauseAllSounds(){
        foreach(SoundData _soundData in this.soundsDatas.Values){
            _soundData.Source.Pause();
        }
    }

    public void pauseAllSoundsOfType(SoundGroup soundType){
        foreach(SoundData _soundData in this.soundsDatas.Values){
            if(_soundData.SoundGroup == soundType){
                _soundData.Source.Pause();
            }
        }
    }

    public void pauseSound(SoundData soundData){
        if(!this.soundIsValid(soundData)){
            return;
        }
        soundData.Source.Pause();
    }

    public void pauseSound(string soundName){
        SoundData _soundData = getSoundData(soundName);
        if(!this.soundIsValid(_soundData)){
            return;
        }
        _soundData.Source.Pause();
    }

    public void resumeAllSounds(){
        foreach(SoundData _soundData in this.soundsDatas.Values){
            _soundData.Source.UnPause();
        }
    }

    public void resumeAllSoundsOfType(SoundGroup soundType){
        foreach(SoundData _soundData in this.soundsDatas.Values){
            if(_soundData.SoundGroup == soundType){
                _soundData.Source.UnPause();
            }
        }
    }

    public void resumeSound(SoundData soundData){
        if(!this.soundIsValid(soundData)){
            return;
        }
        soundData.Source.UnPause();
    }

    public void resumeSound(string soundName){
        SoundData _soundData = getSoundData(soundName);
        if(!this.soundIsValid(_soundData)){
            return;
        }
        _soundData.Source.UnPause();
    }
}
