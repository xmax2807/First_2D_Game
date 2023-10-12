using System;
using UnityEngine;
public class Evade : Speed
{
    private float startTime;
    public Evade(BaseAbility root, Core core, SpeedAbilityData data) : base(root,core, data)
    {
    }
    public override bool CheckCanEnter()
    {
        return core.Senses.CheckIfTouchingGround();
    }
    public override void AbilityEnter()
    {   
        startTime = Time.time;
        core.Player.IsInvincible = true;
        core.Movement.AddForce(new Vector2(-8f * core.Movement.FacingDirection,12f), ForceMode2D.Impulse);      
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        core.Movement.AddForce(new Vector2(Time.deltaTime*0.1f* Mathf.Sin(Mathf.PI * (Time.time - startTime)) * core.Movement.FacingDirection,Time.deltaTime * 0.2f), ForceMode2D.Impulse);
    }

    public override void AbilityExit()
    {
        core.Player.IsInvincible = false;
    }
}