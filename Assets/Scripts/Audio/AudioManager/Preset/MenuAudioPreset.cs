using UnityEngine;
[CreateAssetMenu(menuName ="ScriptableOject/AudioPreset/MenuPreset", fileName ="MenuAudioPreset")]
public class MenuAudioPreset : ScriptableObject{
    [SerializeField]public AudioComponent Navigate;
    [SerializeField]public AudioComponent Select;
    [SerializeField]public AudioComponent Back;
}