using UnityEngine;
using System.Collections.Generic;
public class UIBarComponent : PlayerCoreComponent{

    public enum UIType {
        HP, Mana, Exp, Gaurd
    }
    [System.Serializable]
    public struct UIGameObject{
        public UIType type;
        public BaseBarUI barUI;
    }
    [SerializeField] private UIGameObject[] _gameObjects;
    protected Dictionary<UIType, ExpandableBarBehaviour> HashedBehaviours {get;private set;}
    protected Dictionary<UIType, BaseBarUI> HashedUI {get;private set;}
    public PlayerStat Stats => Core.Stats;

    protected override void Awake(){
        base.Awake();
        Init();
    }

    private void Init(){
        HashedUI = new();
        foreach(UIGameObject ui in _gameObjects){
            HashedUI[ui.type] = ui.barUI;
        }

        HashedBehaviours = new();
        var children = GetComponentsInChildren<ExpandableBarBehaviour>();
        foreach(var child in children){
            child.Init(this);
            child.OnValueUpdated += UpdateUI;
            //child.AttachUI(HashedUI[child.GetUIType()]);
            HashedBehaviours[child.GetUIType()] = child;
            UpdateUI(child.GetUIType(), child.GetNormalized(), true);
        }
    }

    public void LoadData(){
        int HPCount = Core.PlayerGameData.Data.numberOfHPBar;
        int ManaCount = Core.PlayerGameData.Data.numberOfManaBar;

        float currentHP = Core.PlayerGameData.Data.CurrentHPValue;
        float currentMana = Core.PlayerGameData.Data.CurrentManaValue;
        float currentExp = Core.PlayerGameData.Data.CurrentExp;
        
        SetValue(UIType.HP,currentHP <= 0? Stats.HitPoint : currentHP);
        SetValue(UIType.Mana, currentMana);
        SetValue(UIType.Exp, currentExp);

        for(int i = 0; i < HPCount; i++) ExpandUIOnly(UIType.HP);
        for(int i = 0; i < ManaCount; i++) ExpandUIOnly(UIType.Mana);

    }

    public void SaveData(){
        Core.PlayerGameData.Data.CurrentHPValue = GetCurrentValue(UIType.HP);
        Core.PlayerGameData.Data.CurrentManaValue = GetCurrentValue(UIType.Mana);
        Core.PlayerGameData.Data.CurrentExp = GetCurrentValue(UIType.Exp);
    }

    private void UpdateUI(UIType type, float value, bool skipAnimation){
        HashedUI[type].UpdateValue(value, skipAnimation);
    }

    public void UpdateValue(UIType type,float amount) => HashedBehaviours[type].UpdateValue(amount);
    public void SetValue(UIType type, float value) => HashedBehaviours[type].SetValue(value);
    public void ExpandBar(UIType type) {
        if(HashedUI[type] is not ExpandableBar) return;

        HashedBehaviours[type].ExpandBar(shouldFill:true);
        ((ExpandableBar)HashedUI[type]).ExpandBar();
        
        if(type == UIType.HP){
            Core.PlayerGameData.Data.numberOfHPBar++;
        }
        else{
            Core.PlayerGameData.Data.numberOfManaBar++;
        }
    }
    public float GetCurrentValue(UIType type) => HashedBehaviours[type].GetCurrentValue();

    private void ExpandUIOnly(UIType type){
        if(HashedUI[type] is not ExpandableBar) return;
        ((ExpandableBar)HashedUI[type]).ExpandBar();
    }
    public void ToggleVisual(UIType type,bool value) => HashedUI[type].gameObject.SetActive(value);
}