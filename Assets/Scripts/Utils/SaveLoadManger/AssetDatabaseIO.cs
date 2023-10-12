using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

#if UNITY_EDITOR
public static class AssetDatabaseIO{
    private const string Root = "Assets/Resources";
    public static void SaveAsset<T>(T data,string fileName,string folderName = "") where T : ScriptableObject{
        string path = Root + '/' + folderName;
        if(!AssetDatabase.IsValidFolder(path))
            AssetDatabase.CreateFolder(Root,folderName);

        int i = 1;
        string tempPath = fileName;
        while(System.IO.File.Exists($"{path}/{tempPath}.asset")){
            tempPath = fileName;
            tempPath += i;
            i++;
        }
        AssetDatabase.CreateAsset(data, $"{path}/{tempPath}.asset");
        AssetDatabase.SaveAssets();
    }

    public static void DeleteAsset(string id){
        string path = AssetDatabase.GUIDToAssetPath(id);
        AssetDatabase.DeleteAsset(path);
    }

    public static void DeleteAssets<T>(string folderName, Func<string, bool>filter){
        var folderPath = Root + '/' + folderName;
        string[] unusedFolder = { folderPath };
        var paths = AssetDatabase.FindAssets("", unusedFolder).Select((asset)=>AssetDatabase.GUIDToAssetPath(asset)).ToList();
        foreach (var path in paths.Where(filter).ToList())
        {
            AssetDatabase.DeleteAsset(path);
        }
    }

    public static List<T> LoadAssets<T>(IList<string> paths) where T: ScriptableObject{
        List<T> result = new();
        foreach(string path in paths){
            result.Add((T)AssetDatabase.LoadAssetAtPath(path, typeof(T)));
        }

        return result;
    }
}
#endif