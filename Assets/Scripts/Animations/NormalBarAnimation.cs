using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;
public class NormalBarAnimation : AmountChangingAnimation{
    [SerializeField] private Slider mainSlider;
    private bool isChanging;
    
    private const float amountChangPerFrame = 0.5f;

    public override void StartAnimation(bool skipAnimation = false){
        if(skipAnimation){
            mainSlider.value = EndPoint;
            OnAnimationFinished?.Invoke();
            return; 
        }
        
        bool isGain = StartPoint < EndPoint;

        float changeAmountPerFrame = isGain? amountChangPerFrame : -amountChangPerFrame;

        Func<float,bool> onCheckLimit = isGain ? 
        (current) => EndPoint <= current : 
        (current) => EndPoint > current;
        
        if(isChanging) return;
        isChanging = true;
        TimeManager.Instance.RunPausableCoroutine(StartChanging(mainSlider,changeAmountPerFrame, onCheckLimit));
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
}