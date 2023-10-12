using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObject/GameSetting", fileName ="DefaultGameSettings")]
public class SettingData : ScriptableObject{
    private const string resolutionWidthKey = "ResolutionWidth";
    private const string resolutionHeightKey = "ResolutionHeight";
    private const string fullScreenKey = "FullScreen";
    private const string BgVolumeValueKey = "BGVolume";
    private const string SoundFxVolumeValueKey = "FxVolume";

    public Resolution Resolution;
    public bool IsFullScreen;
    public float BgVolumeValue;
    public float SoundFxVolumeValue;
    public void SaveData(){
        PlayerPrefs.SetInt(resolutionWidthKey,Resolution.width);
        PlayerPrefs.SetInt(resolutionHeightKey,Resolution.height);
        PlayerPrefs.SetInt(fullScreenKey,IsFullScreen? 1 : 0);
        PlayerPrefs.SetFloat(BgVolumeValueKey, BgVolumeValue);
        PlayerPrefs.SetFloat(SoundFxVolumeValueKey, SoundFxVolumeValue);
        PlayerPrefs.Save();
    }

    public void LoadData(){
        if(!PlayerPrefs.HasKey(resolutionHeightKey)){
            SetDefaultValues();
            SaveData();
            return;
        }

        int resWidth = PlayerPrefs.GetInt(resolutionWidthKey,Resolution.width);
        int resHeight = PlayerPrefs.GetInt(resolutionHeightKey,Resolution.height);
        
        Resolution = new(){
            width = resWidth,
            height = resHeight
        };

        IsFullScreen = PlayerPrefs.GetInt(fullScreenKey, 1) == 1;
        BgVolumeValue = PlayerPrefs.GetFloat(BgVolumeValueKey, 0);
        SoundFxVolumeValue = PlayerPrefs.GetFloat(SoundFxVolumeValueKey, 0);
    }

    public void Awake(){
        LoadData();
    }

    public void SetDefaultValues(){
        Resolution = Screen.currentResolution;
        IsFullScreen = false;
        BgVolumeValue = 0f;
        SoundFxVolumeValue = 0f;
    }
}