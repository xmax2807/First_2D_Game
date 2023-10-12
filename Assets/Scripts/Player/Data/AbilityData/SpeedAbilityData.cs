using UnityEngine;
[CreateAssetMenu(fileName = "SpeedAbilityData", menuName = "Data/Player Data/Ability Data/Speed")]
public class SpeedAbilityData : ScriptableObject{
    [Header("General")]
    public float DodgeTiming = 0.15f;
    [Header("Dash")]
    public float DelayTime = 0.3f;
    public float DashSpeed = 15f;
    public int DashCount = 1;

    [Header("Teleport")]
    public int TeleportCount = 1;
    public float MaxDistance = 10f;
    
}