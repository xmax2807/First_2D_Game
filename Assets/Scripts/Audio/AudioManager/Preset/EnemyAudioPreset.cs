using UnityEngine;
[CreateAssetMenu(menuName ="ScriptableOject/AudioPreset/EnemyPreset", fileName ="EnemyAudioPreset")]
public class EnemyAudioPreset : ScriptableObject{
    [SerializeField]public AudioComponent Walking;
    [SerializeField]public AudioComponent Hurt;
    [SerializeField]public AudioComponent Fixed;
}