using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ExpandableBarBehaviour : MonoBehaviour
{
    [SerializeField] private UIBarComponent.UIType type;
    [SerializeField] private bool isFull;
    
    protected WorldBar Bar;
    protected UIBarComponent Manager;

    protected float maxValue = 0f;
    public virtual void Init(UIBarComponent manager){
        Manager = manager;
        InitWorldBar();
    }

    protected void InitWorldBar(){
        Bar = new WorldBar(maxValue,isFull);
        Bar.OnValueChanged += (_,skipAnimation)=>OnValueUpdated?.Invoke(type,Bar.Normalized, skipAnimation);
        Bar.OnValueChanged?.Invoke(maxValue,true);
    }

    public System.Action<UIBarComponent.UIType,float, bool> OnValueUpdated;
    public UIBarComponent.UIType GetUIType() => type;   
    public float GetCurrentValue() => Bar.CurrentValue;
    public float GetNormalized() => Bar.Normalized;
    public bool isEmpty() => Bar.CurrentValue == 0f;
    public virtual void UpdateValue(float amount) {    
        Bar.Change(amount);
    }
    public void SetValue(float value){
        Bar.CurrentValue = value;
        OnValueUpdated?.Invoke(type, Bar.Normalized, true);
    }

    public virtual void ExpandBar(bool shouldFill){
        Bar.ExpandMaxValue(500f, shouldFill);
    }
}