using UnityEngine;
using TMPro;
public class ExpBarBehaviour : ExpandableBarBehaviour{
    [SerializeField] TextMeshProUGUI LevelText; 
    public override void Init(UIBarComponent manager)
    {
        Manager = manager;
        RecalculateMaxValue();

        InitWorldBar();
        LevelText.text = $"lv.{Manager.Stats.CurrentLevel}";
    }
    private void RecalculateMaxValue(){
        if(Bar != null){
            SetValue(0);
        }
        
        // NextLevelPointNeed = (Level - / 0.07) ^ 2 - ((Level - 1) / 0.07)^2
        float TotolExp = (float)System.Math.Pow(Manager.Stats.CurrentLevel/0.07f, 2f); 
        float PrevTotolExp = (float)System.Math.Pow((Manager.Stats.CurrentLevel < 1 ? 0 : Manager.Stats.CurrentLevel - 1)/0.07f, 2f);
        maxValue = TotolExp - PrevTotolExp;
    }
    public override void ExpandBar(bool shouldFill)
    {
        RecalculateMaxValue();
        Bar.SetMaxValue(maxValue, false);
    }
    public override void UpdateValue(float amount)
    {
        float leftOver = GetCurrentValue() + amount - maxValue;

        if(leftOver > 0){
            Manager.Stats.CurrentLevel++;
            LevelText.text = $"lv.{Manager.Stats.CurrentLevel}";
            ExpandBar(false);
            base.UpdateValue(leftOver);
        }
        else{
            base.UpdateValue(amount);
        }
    }
}