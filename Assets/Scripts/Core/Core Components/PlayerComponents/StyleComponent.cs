using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StyleComponent : PlayerCoreComponent
{
    private Animator AnimatorController;
    [SerializeField] ManualSwapSprite Avatar;
    [SerializeField] ManualSwapTextColor StyleName; 

    private int currentIndex;
    protected override void Awake()
    {
        base.Awake();
        AnimatorController = GetComponentInChildren<Animator>();
    }
    public override void OnEnable(){
        base.OnEnable();
        Core.AbilityInputHandler.OnAbilityChanged += ChangeAbility;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        Core.AbilityInputHandler.OnAbilityChanged -= ChangeAbility;
    }

    public void Start(){
        currentIndex = Core.AbilityInputHandler.AbilityIndex;

        UpdateUI();
    }

    private void ChangeAbility(int index){
        currentIndex = index;

        AnimatorController.gameObject.SetActive(true);
        AnimatorController.SetInteger("style",index + 1);

        UpdateUI();
    }
    public void AnimationFinish(){
        AnimatorController.gameObject.SetActive(false);
    }
    private void UpdateUI(){
        Avatar.SwapSprite(currentIndex);
        
        StyleName.Swap(currentIndex);
        TimeManager.Instance.WaitFor(new WaitForEndOfFrame(),()=>
        StyleName.SwapText(Core.AbilityHandler.ActiveAbility.GetName));

        Core.UIBarComponent.ToggleVisual(UIBarComponent.UIType.Gaurd, currentIndex == 2);
        
    }
}
