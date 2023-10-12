using UnityEngine;
public class AttackState : AbilityState{
    private PlayerActionData actionData;
    private PlayerWeapon _weapon;
    private PlayerCombatInput combatInputState => InputHandler.AttackInput;
    public AttackState(Player other,PlayerWeapon weapon, string otherAnimName) : base (other, otherAnimName){
        _weapon = weapon;
        _weapon.OnFinishAttackCallBack += AnimationFinishTrigger;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlayOneShot(player.AudioPreset.Attack.Clip);
        actionData = CombatInput.currentData;
        isInAir = !isGrounded; 
        _weapon.WeaponEnter();
        SetConditions();
    }
    
    public override void Exit(){
        base.Exit();
        CombatInput.DettachCurrentAttackData();
        _weapon.WeaponExit();
    }

    private Vector2 input => InputHandler.Movement;
    private bool isInAir;
    private bool isTouchingWall;
    public override void Check()
    {
        base.Check();
        isTouchingWall = Sense.CheckIfTouchingWall();
        if(isInAir && isInAir == isGrounded){
            AnimationFinishTrigger();
        }
    }

    
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }

    private void SetConditions(){
        _weapon.SetInt("attackInput",(int)combatInputState.GetInputState());
        _weapon.SetInt("xInput", (int)(input.x * Movement.FacingDirection));
        _weapon.SetBool("inAir", isInAir);
        Movement.SetVelocity(actionData.Velocity);
    }
}