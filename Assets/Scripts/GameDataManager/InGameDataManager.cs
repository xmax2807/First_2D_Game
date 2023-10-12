using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
public class InGameDataManager : MonoBehaviour{
    private GameData _gameData;
    private ISavableData[] availableOjects;
    [SerializeField] private string _fileName;

    public static InGameDataManager Instance {get;private set;}
    private GameDataIOHandler _fileIO ;
    private string _selectedGameProfile = "";
    private readonly (string,string) DateFormat = ("DateFormatSave","MM/dd/yyyy hh:mm tt"); 

    void Awake(){
        if(Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _fileIO = new GameDataIOHandler(Application.persistentDataPath,_fileName);
        PlayerPrefs.SetString(DateFormat.Item1, DateFormat.Item2);        
    }
    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        TimeManager.Instance.WaitFor(new WaitForEndOfFrame(), ()=>{
            availableOjects = FindAllSavableObjectsInScene();
            LoadGame();
        });
    }
    private void OnSceneUnloaded(Scene scene){
        if(scene.name == "MainMenuScene") return;

        SaveGame();
    }
    public void NewGame(){
        Instance._gameData = new GameData();
        Instance._selectedGameProfile = null;
    }
    public void SaveGameToFile(string selectedGameProfile){
        this._selectedGameProfile = selectedGameProfile;
        SaveGameToFile();
    }
    public void SaveGame(bool isSceneLoad = false){
        foreach(var data in availableOjects){
            data.SaveData(_gameData, isSceneLoad);
        }
    }
    public void SaveGameToFile(){
        if(_gameData == null || string.IsNullOrEmpty(_selectedGameProfile)) return;

        SaveGame();
        _gameData.LastSavedDate = System.DateTime.Now.ToString(PlayerPrefs.GetString(DateFormat.Item1));
        _fileIO.Save(_gameData, _selectedGameProfile);
    }
    public void LoadGame(bool autoLoadSave = false){

        //Try load the saved game
        if(autoLoadSave) _gameData = _fileIO.Load(_selectedGameProfile);
        
        if(_gameData == null){
            NewGame();
            return;
        }
        // Add all data
        foreach (var data in availableOjects) {
            data.LoadData(_gameData);
        }
    }

    public void LoadGame(string selectedGameProfile){
        this._selectedGameProfile = selectedGameProfile;
        _gameData = _fileIO.Load(_selectedGameProfile);
        GameManager.Instance.LoadScene(_gameData.CurrentLevel.SceneName, null);
    }

    private ISavableData[] FindAllSavableObjectsInScene() {
        return FindObjectsOfType<MonoBehaviour>(true).OfType<ISavableData>().ToArray(); 
    }

    public System.Collections.Generic.Dictionary<string, GameData> GetAllAvailableGameData(bool needReload) => _fileIO.LoadAllAvailableData(needReload);
}