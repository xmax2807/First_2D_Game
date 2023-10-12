using UnityEngine;

public class BaseWeaponAnimation : MonoBehaviour{
    protected BaseWeapon Weapon;
    public void Awake(){
        Weapon = GetComponentInParent<BaseWeapon>();
    }
    public void OnAnimationFinish()=>Weapon.FinishAnimation();

    public void OnAnimationStarted(){}
    public void OnAnimationInterupt(){}
}