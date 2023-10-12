using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json;

public interface IMyDictionary{
    public void Add(object key, object value);
    public void Remove(object key);
    public object Get(object key);
    public void Clear();
    public object[] GetValues();
    public object SerializeDict();
}
public abstract class MyDictionary<TKey, TValue> : IMyDictionary
{
    Dictionary<TKey, TValue> _prefs;
    public MyDictionary(){
        _prefs = new();
    }
    public MyDictionary(Dictionary<TKey, TValue> clone){
        if(clone == null) _prefs = new();
        else _prefs = clone;
    }
    public virtual void Add(object key, object value)
    {
        //if(key.GetType() != typeof(TKey) || value.GetType() != typeof(TValue)) return;

        _prefs[(TKey)key] = (TValue)value;
    }

    public void Clear()=>_prefs.Clear();

    public object Get(object key)
    {
        if(key.GetType() != typeof(TKey)) return null;
        bool getResult = _prefs.TryGetValue((TKey)key, out var result);
        if(!getResult) return null;
        return result;
    }

    public object[] GetValues()
    {
        return _prefs.Select((pref)=> new KeyValuePair<TKey, TValue>(pref.Key, pref.Value)).Cast<object>().ToArray();
    }

    public void Remove(object key)
    {
        if(key.GetType() != typeof(TKey)) return;
        _prefs.Remove((TKey) key);
    }

    public object SerializeDict()
    {
        return _prefs;
    }
}
public class FloatPrefDictionary : MyDictionary<string, float>{
    public FloatPrefDictionary():base(){}
    public FloatPrefDictionary(Dictionary<string, float> clone) : base(clone)
    {
    }
    public override void Add(object key, object value)
    {
        if(key is not string || value is not string) return;

        float.TryParse((string)value, out float result);
        base.Add(key, result);
    }

}
public class IntPrefDictionary : MyDictionary<string, int>{
    public IntPrefDictionary():base(){}
    public IntPrefDictionary(Dictionary<string, int> clone) : base(clone)
    {
    }

    public override void Add(object key, object value)
    {
        if(key is not string || value is not string) return;

        int.TryParse((string)value, out int result);
        base.Add(key, result);
    }
}
public class StringPrefDictionary : MyDictionary<string, string>{
    public StringPrefDictionary():base(){}
    public StringPrefDictionary(Dictionary<string, string> clone) : base(clone)
    {
    }
}

//source: https://forum.unity.com/threads/editor-utility-player-prefs-editor-edit-player-prefs-inside-the-unity-editor.370292/
public class PlayerPrefsEditor : EditorWindow {
 
    [MenuItem("Edit/Player Prefs")]
    public static void openWindow() {
 
        PlayerPrefsEditor window = (PlayerPrefsEditor)GetWindow(typeof(PlayerPrefsEditor));
        window.titleContent = new GUIContent("Player Prefs");
        window.Show();
 
    }
 
    public enum FieldType { String,Integer,Float }

    private Dictionary<FieldType, IMyDictionary> PrefManager;
    private IntPrefDictionary _intDict = new();
    private FloatPrefDictionary _floatDict = new();
    private StringPrefDictionary _stringDict = new();
 
    private FieldType fieldType = FieldType.String;
    private string setKey = "";
    private string setVal = "";
    private string error = null;
    
    void Save(){
        string IntDict = JsonConvert.SerializeObject(_intDict.SerializeDict());
        string FloatDict = JsonConvert.SerializeObject(_floatDict.SerializeDict());
        string StringDict = JsonConvert.SerializeObject(_stringDict.SerializeDict());
        
        PlayerPrefs.SetString(FieldType.Integer.ToString(), IntDict);
        PlayerPrefs.SetString(FieldType.Float.ToString(), FloatDict);
        PlayerPrefs.SetString(FieldType.String.ToString(), StringDict);
    }

    void Load(){
        _intDict = new(JsonConvert.DeserializeObject<Dictionary<string, int>>(PlayerPrefs.GetString(FieldType.Integer.ToString())));
        _floatDict = new(JsonConvert.DeserializeObject<Dictionary<string,float>>(PlayerPrefs.GetString(FieldType.Float.ToString())));
        _stringDict = new(JsonConvert.DeserializeObject<Dictionary<string,string>>(PlayerPrefs.GetString(FieldType.String.ToString())));
        
        PrefManager = new()
        {
            { FieldType.Integer, _intDict },
            { FieldType.Float, _floatDict },
            { FieldType.String, _stringDict }
        };
    }
 
    void OnGUI() {
        if(PrefManager == null){
            Load();
        }

        EditorGUILayout.LabelField("Player Prefs Editor", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("by RomejanicDev");

        EditorGUILayout.Separator();
        foreach(var dict in PrefManager.Values){
            foreach(var value in dict.GetValues()){
                if(value == null) continue;

                EditorGUILayout.LabelField(value.ToString());
            }
        }
        EditorGUILayout.Separator();
 
        fieldType = (FieldType)EditorGUILayout.EnumPopup("Key Type", fieldType);
        setKey = EditorGUILayout.TextField("Key to Set", setKey);
        setVal = EditorGUILayout.TextField("Value to Set", setVal);
 
        if(error != null) {
            EditorGUILayout.HelpBox(error, MessageType.Error);
        }
 
        if(GUILayout.Button("Set Key")) {
            PrefManager[fieldType].Add(setKey, setVal);
            Save();
            error = null;
 
        }
 
        if(GUILayout.Button("Get Key")) {
       
            object getValue = PrefManager[fieldType].Get(setKey);
            setVal = getValue == null? "" : getValue.ToString();
        }
 
        if(GUILayout.Button("Delete Key")) {
 
            PrefManager[fieldType].Remove(setKey);
            Save();
        }
 
        if(GUILayout.Button("Delete All Keys")) {
 
            PrefManager[fieldType].Clear();
            Save();
        }
 
    }
 
}