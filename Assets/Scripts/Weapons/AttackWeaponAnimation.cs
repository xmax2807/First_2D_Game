using UnityEngine;

public class AttackWeaponAnimation : BaseWeaponAnimation{
    public void ReadyToAttack(){
        Weapon.ReadyToAttack();
    }
    public void TriggerAttack(){
        Weapon.TriggerAttack();
    }
}