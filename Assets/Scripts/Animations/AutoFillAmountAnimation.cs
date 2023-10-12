using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
public class AutoFillAmountAnimation : AmountChangingAnimation{
    [SerializeField] private Slider TargetSlider;
    [SerializeField] private float duration = 1;
    public bool IsDone = false;
    private bool isRunning = false;
    void OnEnable(){
        if(isRunning) return;

        SettingUp(0,TargetSlider.maxValue * 0.95f);
        if(duration == 0) duration = 1;
        StartAnimation();
    }
    public void Reset() {
        TargetSlider.value = 0;
        IsDone = false;
        isRunning = false;
    }
    public override void StartAnimation(bool skipAnimation = false)
    {
        isRunning = true;
        TimeManager.Instance.StartCoroutine(StartChanging());
    }

    private IEnumerator StartChanging(){
        float maxValue = TargetSlider.maxValue * 0.95f;
        float changeAmountPerFrame = maxValue / duration * Time.unscaledDeltaTime;
        float current = 0f;
        YieldInstruction instruction = new WaitForEndOfFrame();

        while(!IsDone){
            current = Math.Min(changeAmountPerFrame + current, maxValue);
            TargetSlider.value = current;
            yield return instruction;
        }
        TargetSlider.value = TargetSlider.maxValue;
    }
}