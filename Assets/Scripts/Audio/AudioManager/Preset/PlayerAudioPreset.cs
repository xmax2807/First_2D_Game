using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableOject/AudioPreset/PlayerPreset", fileName ="PlayerAudioPreset")]
public class PlayerAudioPreset : ScriptableObject{
    [SerializeField]public AudioComponent Walking;
    [SerializeField]public AudioComponent Hurt;
    [SerializeField]public AudioComponent Attack;
}