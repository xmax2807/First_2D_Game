using UnityEngine;
[CreateAssetMenu(fileName = "DefenseAbilityData", menuName = "Data/Player Data/Ability Data/Defense")]
public class DefenseAbilityData : ScriptableObject{
    [Header("General")]
    public float Timing = 0.1f;
    [Header("Defense")]
    public float[] DamageReduction = new float [3] {0f,0.1f,0.5f};
    public AudioClip blockSoundEffect;
    public GameObject DefenseEffect;
    public MilkShake.ShakePreset ShakeEffect;

    [Header("Counter")]
    public int PerfectCounterPlus = 125;
    public float InvincibleTime = 0.2f;
    public float CounterDistance = 2f;
}