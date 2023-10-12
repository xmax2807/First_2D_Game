using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileIOHandler<T>{
    protected string dataDirPath = "";
    protected string dataFileName = "";
    public FileIOHandler(string dataDirPath, string dataFileName){
        this.dataDirPath = dataDirPath; 
        this.dataFileName = dataFileName;
    }
    public void Save(T data, string additionDirectory = ""){
        string fullPath = Path.Combine(dataDirPath, additionDirectory, dataFileName);
        try{
            if(!File.Exists(fullPath)){
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            }
            string jsonString = JsonUtility.ToJson(data, true);

            using FileStream stream = new(fullPath, FileMode.Create);
            using StreamWriter writer = new(stream);
            writer.Write(jsonString);
        }
        catch(IOException ioe){
            Debug.Log(ioe.Message);
        }
    }

    public T Load(string additionDirectory = ""){
        string fullPath = Path.Combine(dataDirPath, additionDirectory, dataFileName);
        T loadedData = default;
        try{
            string dataJson;
            using(FileStream stream = new(fullPath, FileMode.Open)){
                using StreamReader reader = new(stream);
                dataJson = reader.ReadToEnd();
            }
            
            loadedData = JsonUtility.FromJson<T>(dataJson);
        }
        catch(IOException ioe){
            Debug.Log(ioe.Message);
        }

        return loadedData;
    }
}

public class GameDataIOHandler : FileIOHandler<GameData>
{
    private Dictionary<string, GameData> AllLoadedGameData;
    public GameDataIOHandler(string dataDirPath, string dataFileName) : base(dataDirPath, dataFileName){}

    public Dictionary<string,GameData> LoadAllAvailableData(bool ForceReload = false){
        if(AllLoadedGameData != null && !ForceReload) return AllLoadedGameData;

        AllLoadedGameData = new();
        IEnumerable<DirectoryInfo> directoryInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();

        foreach(DirectoryInfo directoryInfo in directoryInfos){
            string profileID = directoryInfo.Name;
            string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
            if(!File.Exists(fullPath)) continue;

            var gameData = Load(profileID);
            
            if(gameData != null){
                AllLoadedGameData.Add(profileID, gameData);
            }
        }

        return AllLoadedGameData;
    }
}

public static class TextFileIO {
    public static string[] Load(string dataDirPath, string dataFileName){
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try{
            return File.ReadAllLines(fullPath);            
        }
        catch(IOException ioe){
            Debug.Log(ioe.Message);
        }
        return null;
    }
}