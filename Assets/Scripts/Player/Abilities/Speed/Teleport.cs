using System;
using UnityEngine;
public class Teleport : Speed
{
    private int _teleportCount;
    private Vector2 destination;
    private Vector2 startPosition;
    public Teleport(BaseAbility root,Core core, SpeedAbilityData data) : base(root,core, data)
    {
        _teleportCount = data.TeleportCount;
    }

    public override bool CheckCanEnter()
    {
        return _teleportCount > 0;
    }
    public override void AbilityEnter()
    {   
        core.Player.IsInvincible = true;
        core.Movement.SetVectorZero();
        core.Movement.AddForce(new Vector2(5f * core.Movement.FacingDirection,5f), ForceMode2D.Impulse);

        var target = core.VisibilityComponent.GetClosestEnemy();
        startPosition = (Vector2)core.Player.transform.position + Vector2.one;
        destination = target == null ? startPosition : target.GetPosition() - new Vector2(core.Movement.FacingDirection, -1f);
        
        _teleportCount--;
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        core.Movement.FreezeVelocity(1);
    }
    public override void ChangeState(){
        core.Player.transform.position = destination;
    }

    public override void AbilityExit()
    {
        core.Player.IsInvincible = false;
        TimeManager.Instance.WaitForSeconds(data.DelayTime, ()=>{_teleportCount = data.TeleportCount;});
    }
}