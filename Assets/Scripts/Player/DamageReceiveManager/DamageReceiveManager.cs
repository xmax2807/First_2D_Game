using System.Linq;
using System;
using UnityEngine;
public enum StabilityState{
    Broken = -1, 
    Interrupted = 0,
    Stable = 1,  
}
public class PlayerStability{
    private readonly float _stabilityValue;
    private float _currentValue;
    private readonly float _interruptThreshold;
    private bool isRefilling;
    public PlayerStability(float maxVal, float interruptThreshold){
        _stabilityValue = maxVal;
        _currentValue = maxVal;
        _interruptThreshold = interruptThreshold;
    }

    public StabilityState Evaluate(float damageReceived){
        UpdateValue(-damageReceived);
        
        if(!isRefilling) AutoRefill();

        float normalized = _currentValue/_stabilityValue;
        
        if(_currentValue <= 0){
            return StabilityState.Broken;
        }
        else if(normalized < _interruptThreshold){
            return StabilityState.Interrupted;
        }
        return StabilityState.Stable;
    }

    private void AutoRefill(){
        TimeManager.Instance.ExecuteLoop(GetStopCondition, ()=>UpdateValue(10f), delayTime:1f);
        isRefilling = true;
    }
    private bool GetStopCondition() => _currentValue >= _stabilityValue;
    private void UpdateValue(float amount){
        _currentValue = Mathf.Clamp(amount + _currentValue, 0, _stabilityValue);

        if(GetStopCondition()) isRefilling = false;
    }
}
public class DamageReceiveManager<T> {
    private PlayerStability stability;
    public DamageReceiveManager(PlayerStability stability){
        this.stability = stability;
    }

    public delegate float OnDamagedCallback(ITargetable<T> attacker, float amount);
    public event OnDamagedCallback OnDamageHandler;
    private float TotalMinimumDamageReceived;
    public float EvaluateDamage(ITargetable<T> Attacker, float amount, bool isStackable = false){
        if(isStackable){
            return EvaluateDamage(Attacker, amount);
        }

        TotalMinimumDamageReceived = amount;
        var results = OnDamageHandler?.GetInvocationList()?.Select(x => (float)x.DynamicInvoke(Attacker, amount));
        if(results != null){
            foreach(float result in results){
                TotalMinimumDamageReceived = Math.Min(result, TotalMinimumDamageReceived);
            }
        }
        
        return TotalMinimumDamageReceived;
    }
    public float EvaluateDamage(ITargetable<T> Attacker, float amount){
        TotalMinimumDamageReceived = amount;
        OnDamageHandler?.GetInvocationList()?.Select(x => TotalMinimumDamageReceived = (float)x.DynamicInvoke(Attacker, TotalMinimumDamageReceived));
        return TotalMinimumDamageReceived;
    }
    public float GetDamageResult() => TotalMinimumDamageReceived;
    public StabilityState GetInterruptState() => stability.Evaluate(TotalMinimumDamageReceived);
}