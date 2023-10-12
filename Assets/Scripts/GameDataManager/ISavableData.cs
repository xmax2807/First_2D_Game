using System;
public interface ISavableData{
    void LoadData(GameData data, bool isSceneLoad = false);
    void SaveData(GameData data, bool isSceneLoad = false);
}