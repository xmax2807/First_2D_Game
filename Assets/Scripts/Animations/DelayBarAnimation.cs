using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
public class DelayBarAnimation : AmountChangingAnimation{
    [SerializeField] private Slider mainSlider;
    [SerializeField] private Slider subSlider;

    private const float amountChangPerFrame = 0.5f;
    private bool isChanging, isDelaying;

    public override void SettingUp(float start, float end)
    {
        StartPoint = mainSlider.value;
        amount = end;
        EndPoint = amount * mainSlider.maxValue;
    }
    public override void StartAnimation(bool skipAnimation = false) {
        if(skipAnimation){
            mainSlider.value = EndPoint;
            subSlider.value = EndPoint;
            OnAnimationFinished?.Invoke();
            return; 
        }
        
        bool isGain = StartPoint < EndPoint;

        float changeAmountPerFrame = isGain? amountChangPerFrame : -amountChangPerFrame;
        var slider = isGain? ref subSlider : ref mainSlider;
        var delaySlider = isGain? ref mainSlider : ref subSlider;

        Func<float,bool> onCheckLimit = isGain ? 
        (current) => EndPoint <= current : 
        (current) => EndPoint > current;
        
        if(isChanging) return;
        isChanging = true;
        TimeManager.Instance.RunPausableCoroutine(StartChanging(slider,changeAmountPerFrame, onCheckLimit));
        
        

        if(isDelaying) return;
        isDelaying = true;
        TimeManager.Instance.RunPausableCoroutine(LateChanging(delaySlider,changeAmountPerFrame, onCheckLimit));
    }

    
    private IEnumerator StartChanging(Slider _slider,float changeAmountPerFrame, Func<float,bool> isLimit){
        float sliderCurrentValue = _slider.value;  

        while(!isLimit.Invoke(sliderCurrentValue)){
            sliderCurrentValue += changeAmountPerFrame;
            _slider.value = sliderCurrentValue;
            yield return new WaitForFixedUpdate();
        }
        _slider.value = EndPoint;
        isChanging = false;        
    }
    private IEnumerator LateChanging(Slider _slider, float changeAmountPerFrame, Func<float,bool> isLimit){
        yield return new WaitForSeconds(0.5f);

        float sliderCurrentValue = _slider.value;  

        while(!isLimit.Invoke(sliderCurrentValue)){
            sliderCurrentValue += changeAmountPerFrame;
            _slider.value = sliderCurrentValue;
            yield return new WaitForFixedUpdate();
        }
        _slider.value = EndPoint;
        isDelaying = false;
        OnAnimationFinished?.Invoke();
    }
}