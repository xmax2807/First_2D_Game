using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.InputSystem;

public class SettingsMenu : MenuBehaviour
{
    [SerializeField] private Slider BgAudioAdjustment;
    [SerializeField] private Slider SoundFxAudioAdjustment;
    [SerializeField] private Toggle Fullscreen;
    [SerializeField] private TMP_Dropdown Resolutions;
    private SettingData Data => GameManager.Instance.SettingData;

    private Resolution[] resolutions;
    protected override void Awake()
    {
        base.Awake();
        resolutions = Screen.resolutions;
        Resolutions.ClearOptions();
        //TimeManager.Instance.WaitFor(new WaitForEndOfFrame(), LoadSetting);
    }
    public override void Entry()
    {
        base.Entry();

        if(Resolutions.options.Count == 0){
            
            Resolutions.AddOptions(
                resolutions.Select(
                    (res)=>new TMP_Dropdown.OptionData(res.ToString())
                )
                .ToList()
            );
        }
        
        LoadUI();

        Resolutions.onValueChanged.AddListener(OnResolutionsValueChanged);
        Fullscreen.onValueChanged.AddListener(ToggleFullScreen);
        BgAudioAdjustment.onValueChanged.AddListener(OnBgVolumeValueChanged);
        SoundFxAudioAdjustment.onValueChanged.AddListener(OnSoundFxVolumeValueChanged);
    }

    public override void OnRemovedFromStack()
    {
        base.OnRemovedFromStack();
        Resolutions.onValueChanged.RemoveAllListeners();
        Fullscreen.onValueChanged.RemoveAllListeners();
        BgAudioAdjustment.onValueChanged.RemoveAllListeners();
        SoundFxAudioAdjustment.onValueChanged.RemoveAllListeners();
        Data.SaveData();
    }

    private void OnResolutionsValueChanged(int index)
    {
        Data.Resolution = resolutions[index];
        
        Screen.SetResolution(
            Data.Resolution.width,
            Data.Resolution.height,
            Data.IsFullScreen
        );
    }
    
    private void ToggleFullScreen(bool value){
        Data.IsFullScreen = value;

        Screen.fullScreen = Data.IsFullScreen;
    }
    private void OnBgVolumeValueChanged(float value){
        Data.BgVolumeValue = value;
        AudioManager.Instance.ChangeVolume(AudioManager.MixerType.Background, value);
    }
    private void OnSoundFxVolumeValueChanged(float value){
        Data.SoundFxVolumeValue = value;
        AudioManager.Instance.ChangeVolume(AudioManager.MixerType.SoundEffect, value);
    }

    // public void LoadSetting(){
    //     Data.LoadData();
    //     Screen.SetResolution(
    //         Data.Resolution.width,
    //         Data.Resolution.height,
    //         Data.IsFullScreen
    //     );

    //     //Audio        
    //     AudioManager.Instance.ChangeVolume(AudioManager.MixerType.Background, Data.BgVolumeValue);
    // }

    private void LoadUI(){
        if(Data == null){
            Data.LoadData();
        }
        //DropDown
        int currentIndex = -1;
        for(int i = 0; i < resolutions.Length; i++){
            
            if(
                resolutions[i].width == Data.Resolution.width && 
                resolutions[i].height == Data.Resolution.height
            ){
                currentIndex = i;
            }
        }
        Resolutions.value = currentIndex;
        
        //FullScreen
        Fullscreen.isOn = Data.IsFullScreen;
        
        // Audio
        BgAudioAdjustment.value = Data.BgVolumeValue;
        SoundFxAudioAdjustment.value = Data.SoundFxVolumeValue;
    }
}