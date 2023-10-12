using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class PlayerLevelData{
    public List<string> savedObjectIDs;
    public int numberOfHPBar;
    public int numberOfManaBar;
    public float CurrentHPValue;
    public float CurrentManaValue;
    public float CurrentExp;
    public PlayerLevelData(Stat stat){
        CurrentHPValue = stat.HitPoint;
        CurrentManaValue = 0f;
        CurrentExp = 0f;
        savedObjectIDs = new();
    }
}

[CreateAssetMenu(fileName = "InGameInfo", menuName = "Data/Entities/PlayerInGameInfo")]
public class ScriptablePlayerLevelData : ScriptableObject{
    
    public PlayerLevelData Data;
}