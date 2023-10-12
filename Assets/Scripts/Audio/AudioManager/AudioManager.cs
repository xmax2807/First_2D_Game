using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour{
    public static AudioManager Instance;
    public enum MixerType{
        Background, SoundEffect
    }
    [SerializeField] private AudioSource _backgroundSource;
    public AudioSource BackgroundSource => _backgroundSource;
    [SerializeField] private SoundFXManager _soundFX;
    public SoundFXManager SoundFX => _soundFX;
    [SerializeField] private AudioSource MainSource;
    [SerializeField] private AudioMixerGroup BackgroundMixer;
    [SerializeField] private AudioMixerGroup SoundEffectMixer;
    public void Awake(){
        if(Instance == null){
            Instance = this;
        }
        
    }
    public void OnEnable(){
        ChangeVolume(MixerType.Background, GameManager.Instance.SettingData.BgVolumeValue);
        ChangeVolume(MixerType.SoundEffect, GameManager.Instance.SettingData.SoundFxVolumeValue);
    }

    public void PlayOneShot(AudioClip clip){
        MainSource.PlayOneShot(clip);
    }

    public void Play(AudioClip clip){
        MainSource.clip = clip;
        MainSource.Play();
    }

    public void ChangeVolume(MixerType type, float value){
        switch(type){
            case MixerType.Background:
            BackgroundMixer.audioMixer.SetFloat("bgVol", value);
            break;

            case MixerType.SoundEffect:
            SoundEffectMixer.audioMixer.SetFloat("fxVol", value);
            break;
        }
    }
}