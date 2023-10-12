using System;
using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    public bool IsGamePaused {get;private set;}
    void Awake(){
        if(Instance == null) {
            Instance = this;
        }
    }
    void Start()
    {
        if(Instance == null) {
            Instance = this;
        }
    }

    void OnDestroy(){
        StopAllCoroutines();
    }

    public void PauseGame(){
        IsGamePaused = true;
        Time.timeScale = 0;
    }
    public void ResumeGame(){
        Time.timeScale = 1;
        IsGamePaused = false;
    }

    public Coroutine WaitFor(YieldInstruction waiter, Action result){
        return StartCoroutine(WaitUntil(waiter, result));
    }

    private IEnumerator WaitUntil(YieldInstruction waiter, Action result){
        yield return waiter;
        result?.Invoke();
    }
    public void CancelCoroutine(Coroutine coroutine)=>StopCoroutine(coroutine);

    public void TimeOut(float limit, Action result){
        StartCoroutine(StartTimeOut(()=>Time.time >= limit,result));
    }

    public Coroutine WaitForSeconds(float seconds, Action result){
        float t0 = seconds + Time.time;
        return StartCoroutine(StartTimeOut(()=>Time.time >= t0, result));
    }

    public void WaitUntil(Func<bool> condition, Action result){
        StartCoroutine(StartTimeOut(condition, result));
    }

    public Coroutine WaitForSecondsUnscaled(float seconds, Action result){
        float t0 = seconds + Time.unscaledTime;
        return StartCoroutine(StartTimeOut(()=>Time.unscaledTime >= t0, result));
    }


    private IEnumerator StartTimeOut(Func<bool> condition, Action result){
        yield return new WaitUntil(condition);
        result?.Invoke();
    }

    public Coroutine RunPausableCoroutine(IEnumerator func){
        return StartCoroutine(PausableCoroutine(func));
    }

    private IEnumerator PausableCoroutine(IEnumerator func){
        bool canMoveNext = func.MoveNext();
        while(canMoveNext){
            if(IsGamePaused) {
                yield return new WaitUntil(()=>IsGamePaused == false);
            }

            yield return func.Current;
            canMoveNext = func.MoveNext();
        }
    }

    public void SlowMotion(float factor = 0.05f, float duration = 1f){
        StartCoroutine(StartSlowMotion(factor, duration));
    }

    private IEnumerator StartSlowMotion(float factor, float duration){
        Time.timeScale = factor;
        float startFixedDeltaTime = Time.fixedDeltaTime;

        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        while(Time.timeScale < 1f){
            Time.timeScale += 1f/duration * Time.unscaledDeltaTime;
            yield return null;
        }
        Time.fixedDeltaTime = startFixedDeltaTime;
        Time.timeScale = 1f;
    }

    public void ExecuteLoop(Func<bool> stopCondition, Action execution, float delayTime = 0f){
        StartCoroutine(ExecuteLoopCoroutine(stopCondition,execution, delayTime));
    }
    public IEnumerator ExecuteLoopCoroutine(Func<bool> stopCondition, Action execution, float delayTime = 0f){
        while(stopCondition != null && !stopCondition.Invoke()){
            execution?.Invoke();
            yield return new WaitForSeconds(delayTime);
        }
    }
}
