using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableOject/AudioPreset/InGameSFX", fileName ="InGameSFXPreset")]
public class InGameSoundFX : ScriptableObject{
    public AudioComponent ItemCollected;
    public AudioComponent SkillCollected;
    public AudioComponent LevelUp;
} 