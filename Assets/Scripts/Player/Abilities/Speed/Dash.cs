using UnityEngine;
public class Dash : Speed {
    
    public Dash(BaseAbility root,Core core, SpeedAbilityData data) : base(root,core, data){
        canBeDash = true;
        dashcount = data.DashCount;
    }
    private GameObjectPooling AfterImagePooling;
    int dashcount = 0;
    bool canBeDash;
    private  float currentSpeed;
    public override bool CheckCanEnter()
    {
        return canBeDash && dashcount > 0;
    }
    public override void AbilityEnter()
    {   
        currentSpeed = data.DashSpeed * core.Movement.FacingDirection * 2f;
        core.Movement.SetVectorZero();
        dashcount--;
        canBeDash = false;
        AfterImagePooling = core.PoolingComponent.GetGameObjectPooling("AbilityAfterImage");
    }
    public override void UpdateState()
    {
        //base.UpdateState();
        core.Movement.FreezeVelocity(1);
        AfterImagePooling.GetGameObject();
        core.Movement.SetVelocityX(currentSpeed);
    }
    public override void FixedUpdateState(){
        //currentSpeed = (float)System.Math.Round(currentSpeed * 0.92f,2);
        currentSpeed *= 0.92f;
        if(core.Senses.CheckIfTouchingGround() && core.Senses.CheckIfReachEdge()){
            OnFinishCallback?.Invoke();
            return;
        }
    }
    public override void AbilityExit()
    {
        TimeManager.Instance.WaitForSeconds(data.DelayTime, ()=>{canBeDash = true; dashcount = data.DashCount;});
    }
    public override void ResetState()
    {
        if(core.Senses.CheckIfTouchingGround()){
            dashcount = data.DashCount;
        }
    }
}