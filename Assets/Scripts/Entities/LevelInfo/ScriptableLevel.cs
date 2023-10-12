
using UnityEngine;

[System.Serializable]
public class LevelEntity{
    public string SceneName = "";
    public enum Difficulty{
        Easy, Normal, Hard
    }
    public Difficulty LevelDifficulty = Difficulty.Easy;
    public string LevelName;
    public AudioClip BackgroundAudio;
    public Sprite BackgroundImage;
    public LevelEntity(){
        SceneName = "";
        LevelName = "";
        LevelDifficulty = Difficulty.Easy;
    }
}

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/Entities/LevelInfo")]
public class ScriptableLevel : ScriptableObject{
    public LevelEntity Data;
}