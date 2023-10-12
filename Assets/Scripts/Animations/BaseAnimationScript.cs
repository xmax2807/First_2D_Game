using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
public abstract class BaseAnimationScript : MonoBehaviour{
    public Action OnAnimationFinished;
    public virtual void StartAnimation(bool skipAnimation = false){}
    private void OnDisable(){
        OnAnimationFinished = null; 
    }
}

public abstract class BaseAnimationScript<T> : BaseAnimationScript{
    protected T StartPoint;
    protected T EndPoint;
    public virtual void SettingUp(T start, T end){
        StartPoint = start;
        EndPoint = end;
    }
}