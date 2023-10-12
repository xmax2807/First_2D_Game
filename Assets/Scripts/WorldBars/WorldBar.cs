using System;
using UnityEngine;
public class WorldBar
{
    public Action<float,bool> OnValueChanged;
    public WorldBar(float maxValue, bool isFill = false)
    {
        CurrentValue = isFill ? maxValue : 0;
        MaxValue = maxValue;
    }
    public float MaxValue { get; private set; }

    private float _currentValue;
    public float CurrentValue
    {
        get
        {
            return _currentValue;
        }
        set
        {
            _currentValue = value;
        }
    }
    public float Normalized => CurrentValue / MaxValue;
    public virtual void Change(float amount)
    {
        CurrentValue = Mathf.Clamp(CurrentValue + amount, 0, MaxValue);
        OnValueChanged?.Invoke(_currentValue, false);
    }

    public void Refill()
    {
        CurrentValue = MaxValue;
        OnValueChanged?.Invoke(_currentValue, false);
    }
    public void Empty()
    {
        CurrentValue = 0f;
        OnValueChanged?.Invoke(_currentValue, false);
    }

    public void ExpandMaxValue(float amount, bool shouldFill = false)
    {
        MaxValue+= amount;
        
        if(!shouldFill) return;
        Refill();
    }
    public void SetMaxValue(float newValue, bool shouldFill = false){
        MaxValue = newValue;

        if(!shouldFill) return;
        Refill();
    }

}