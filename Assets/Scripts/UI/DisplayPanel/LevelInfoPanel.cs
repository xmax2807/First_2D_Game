using UnityEngine;
using TMPro;
public class LevelInfoPanel : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI Title; 
    [SerializeField] private TextMeshProUGUI Difficulty;
    [SerializeField] private Transform SkillItemContainer;
    [SerializeField] private GameObject SkillItemIcon;
    [SerializeField] private UnityEngine.UI.Image BackgroundImage;
    private LevelData LevelInfo;

    private void Awake(){
        TimeManager.Instance.WaitUntil(()=>LevelInfo!=null, ()=>{
            LevelInfo.OnAnObjectDestroyed += UpdateSkillItemUI;
            //PopulateSkilItem();
        });
    }
    private void OnEnable(){
        if(LevelInfo == null){
            LevelInfo = FindObjectOfType<LevelData>();
            DisplayData();
        }
        if(SkillItemContainer.childCount == 0){
            PopulateSkilItem();
        }
        UpdateSkillItemUI();
    }
    private void DisplayData(){
        //LevelInfo = FindObjectOfType<LevelData>();
        if(LevelInfo == null) return;

        Title.text = LevelInfo.LevelEntity.LevelName;
        Difficulty.text = $"Difficulty: {LevelInfo.LevelEntity.LevelDifficulty}";
        BackgroundImage.sprite = LevelInfo.LevelEntity.BackgroundImage;
    }

    private void PopulateSkilItem(){
        for(int i = 0; i< LevelInfo.TotalHiddenItems; i++){
            Instantiate(SkillItemIcon, SkillItemContainer);
        }
    }
    private void UpdateSkillItemUI(){
        var icons = SkillItemContainer.GetComponentsInChildren<Icon>();
        //int count = Mathf.Min(icons.Length, LevelInfo.AvailableHiddenItems);

        int i = 0;
        for(; i < icons.Length; i++){
            icons[i].Toggle(true);
        }
        
        i = icons.Length - LevelInfo.AvailableHiddenItems;
        if(i < 0) return;
        
        for(; i < icons.Length; i++){
            icons[i].Toggle(false);
        }
    }
}