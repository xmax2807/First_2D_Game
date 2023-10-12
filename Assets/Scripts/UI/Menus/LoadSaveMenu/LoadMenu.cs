using UnityEngine;
using TMPro;
public class LoadMenu : NavigationMenu{
    [SerializeField] private TextMeshProUGUI _display;
    public TextMeshProUGUI Display => _display;
    public System.Action<SaveSlot> OnClickHandler;
    private SaveSlot[] children;
    protected override void Awake(){
        base.Awake();
        children = GetComponentsInChildren<SaveSlot>();
        // for(int i = 0; i < children.Length; i++){
        //     if(children[i].OnClicked != null){
        //         children[i].OnClicked -= OnButtonClicked;
        //     }
        //     children[i].OnClicked += OnButtonClicked;
        // }
        _currentIndex = 0;
    }
    void OnDisable(){
        for(int i = 0; i < children.Length; i++){
            children[i].OnClicked -= OnButtonClicked;
        }
    }
    void OnEnable(){
        for(int i = 0; i < children.Length; i++){
            children[i].OnClicked += OnButtonClicked;
        }
    }

    protected void OnButtonClicked(DisplayButton source){
        var availableSaves = InGameDataManager.Instance.GetAllAvailableGameData(false);
        if(availableSaves.ContainsKey(source.GetIdentity())){
            MenuManager.Instance.PopAll();
            InGameDataManager.Instance.LoadGame(source.GetIdentity());
        }
    }
    public override void UpdateMenuUI()
    {
        ReloadSavedData(ForceReload:false);
        SelectFirstSelectable();
    }

    private void ReloadSavedData(bool ForceReload){
        var availableSaves = InGameDataManager.Instance.GetAllAvailableGameData(ForceReload);
        foreach(var child in children){
            availableSaves.TryGetValue(child.GetIdentity(), out var gameData);
            child.SetData(gameData);
        }
    }
}