
public class SaveMenu : NavigationMenu{
    private SaveSlot[] children;
    protected override void Awake(){
        base.Awake();
        children = GetComponentsInChildren<SaveSlot>();
        // for(int i = 0; i < children.Length; i++){
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
        InGameDataManager.Instance.SaveGameToFile(source.GetIdentity());
        ReloadSavedData(true);
        source.ManualUpdate();
    }

    public override void UpdateMenuUI(){
        ReloadSavedData(false);
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