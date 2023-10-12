using UnityEngine;

public class GameData { 
    public Vector3 PlayerPosition;
    public PlayerStat PlayerStats;
    public LevelEntity CurrentLevel;
    public PlayerLevelData PlayerInGameInfo; 
    public string LastSavedDate;
    public string TimePlayed;

    public GameData(){
        PlayerStats = new();
        PlayerInGameInfo = new(PlayerStats);
        LastSavedDate = "";
        CurrentLevel = new();
    }
}