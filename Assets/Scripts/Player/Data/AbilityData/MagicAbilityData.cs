using UnityEngine;

[CreateAssetMenu(fileName = "MagicAbilityData", menuName = "Data/Player Data/Ability Data/Magic")]

public class MagicAbilityData : ScriptableObject{
    
    [Header("General")]
    public float DamageMultiplier = 0.2f;
    public GameObject MagicSword;

    [Header("Summon")]
    public float SwordSpeed = 20f;
    public float RandomSpawn = 2f;

    [Header("Million Stabs")]
    public int SpawnSword = 5;

    [Header("Volcano")]
    public float VolcanoManaRequired = 20f;
    public GameObject DarkMagic;
    public float VolcanoDamageMultiplier = 1.2f;
}