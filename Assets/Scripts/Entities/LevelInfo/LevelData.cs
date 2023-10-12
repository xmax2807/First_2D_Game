using UnityEngine;
using Unity.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
public class LevelData : MonoBehaviour, ISavableData{
    [SerializeField]private ScriptableLevel _data;
    public LevelEntity LevelEntity {
        get => _data.Data;
        private set => _data.Data = value;  
    }
    private Dictionary<string, ISavableObject> currentLevelObject;
    
    private int _availableHiddenItems;
    public int AvailableHiddenItems {get => _availableHiddenItems;
    private set{
        if(_availableHiddenItems == value){
            return;
        }

        _availableHiddenItems = value;
        OnAnObjectDestroyed?.Invoke();
    }}
    public int TotalHiddenItems {get;private set;}
    public event System.Action OnAnObjectDestroyed;

    void Awake(){
        var objects = FindObjectsOfType<MonoBehaviour>(true).OfType<ISavableObject>();
        currentLevelObject = new();
        foreach(var obj in objects){
            if(currentLevelObject.ContainsKey(obj.GetID())) continue;

            if(obj is SkillItem){
                TotalHiddenItems++;
            }
            if(currentLevelObject.TryAdd(obj.GetID(), obj)){
                obj.OnDestroyed += OnObjectDestroyed;
            };
        }
        _availableHiddenItems = TotalHiddenItems;
    }
    public void Start(){
        AudioManager.Instance.BackgroundSource.clip = _data.Data.BackgroundAudio;
        AudioManager.Instance.BackgroundSource.Play(); 
    }
    public void OnDisable(){
        if(AudioManager.Instance == null) return;
        AudioManager.Instance.BackgroundSource.clip = null;
    }
    public void LoadData(GameData data, bool isSceneLoad)
    {
        string[] allIDs = data.PlayerInGameInfo.savedObjectIDs.ToArray();
        
        for(int i = 0; i < allIDs.Length; i++){
            //string id = LevelEntity.AvailableHiddenItems[i].ID;
            if(currentLevelObject.ContainsKey(allIDs[i])){
                currentLevelObject[allIDs[i]].DestroyThisObject();
                _availableHiddenItems--;
            }
        }
    }

    public void SaveData(GameData data, bool isSceneLoad)
    {
        if(isSceneLoad) return;
        LevelEntity.SceneName = SceneManager.GetActiveScene().name;
        data.CurrentLevel = this.LevelEntity;
    }

    private void OnObjectDestroyed(){
        AvailableHiddenItems--;
    }
}
