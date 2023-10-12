using UnityEngine;
using System;
using System.Collections;

public class NormalBarAnimationMask : AmountChangingAnimation{
    [SerializeField] private RectTransform rectTransformMask;
    [SerializeField] private bool isVerticalDirection = false;
    [SerializeField] private float maxValue;
    [SerializeField] private float minValue = 0;
    private bool isChanging;
    private const float amountChangPerFrame = 0.5f;
    public override void SettingUp(float start, float end)
    {
        base.SettingUp(start, end);
        StartPoint *= maxValue - minValue;
        EndPoint *= maxValue - minValue;
    }
    private Vector2 calculateRect(){
        float value = EndPoint;
        return isVerticalDirection ? new Vector2(rectTransformMask.sizeDelta.x,value) : new Vector2(value, rectTransformMask.sizeDelta.y);
    }
    public override void StartAnimation(bool skipAnimation = false){
        if(skipAnimation){
            rectTransformMask.sizeDelta = calculateRect();
            OnAnimationFinished?.Invoke();
            return; 
        }
        
        bool isGain = StartPoint < EndPoint;

        float changeAmountPerFrame = isGain? amountChangPerFrame : -amountChangPerFrame;
        Vector2 changeAmountPerFrameVector2 = isVerticalDirection? new Vector2(0, changeAmountPerFrame) : new Vector2(changeAmountPerFrame, 0);

        Func<float,bool> onCheckLimit = isGain ? 
        (current) => EndPoint <= current : 
        (current) => EndPoint > current;
        
        if(isChanging) return;
        isChanging = true;
        TimeManager.Instance.RunPausableCoroutine(StartChanging(rectTransformMask,changeAmountPerFrameVector2, onCheckLimit));
    }

    private IEnumerator StartChanging(RectTransform rectTransform,Vector2 changeAmountPerFrame, Func<float,bool> isLimit){
        Vector2 CurrentValue = rectTransform.sizeDelta;  

        while(!isLimit.Invoke(isVerticalDirection? CurrentValue.y : CurrentValue.x)){
            CurrentValue += changeAmountPerFrame;
            rectTransform.sizeDelta = CurrentValue;
            yield return new WaitForFixedUpdate();
        }
        rectTransform.sizeDelta = CurrentValue;
        isChanging = false;
    }
}