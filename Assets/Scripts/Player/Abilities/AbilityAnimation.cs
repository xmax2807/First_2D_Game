using UnityEngine;

public class AbilityAnimation : MonoBehaviour{
    [SerializeField]protected BaseAbility ability;
    
    public void OnAbilityChangeState() {
        ability.CurrentActiveAbility.ChangeState();
    }
    public void OnAnimationFinish()=>ability.ManualFinishAnimation();
}