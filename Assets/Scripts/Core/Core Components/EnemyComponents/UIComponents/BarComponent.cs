using UnityEngine;
using UnityEngine.UI;

public class BarComponent : MonoBehaviour{

    private WorldBar Bar;
    [SerializeField] Slider UISlider;

    [SerializeField] private bool isFull;
    
    protected float maxValue = 0f;
    public virtual void Init(float maxValue){
        this.maxValue = maxValue;
        Bar = new WorldBar(maxValue,isFull);
        Bar.OnValueChanged += (value,_)=>UISlider.value = Bar.Normalized;
        Bar.OnValueChanged?.Invoke(maxValue,true);
    }

    public float GetCurrentValue() => Bar.CurrentValue;
    public bool isEmpty() => Bar.CurrentValue == 0f;
    public void UpdateValue(float amount) {    
        Bar.Change(amount);
    }
    public void SetValue(float value){
        Bar.CurrentValue = value;
    }
}