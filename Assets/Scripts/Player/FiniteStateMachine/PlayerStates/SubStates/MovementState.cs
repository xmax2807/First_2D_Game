using UnityEngine;
public class MovementState : GroundedState{
    public MovementState(Player other, string otherAnimName)
    :base(other,otherAnimName){}

    private float currentSpeed;
    private Coroutine speedCoroutine;
    private bool isRunning = false;
    private GameObjectPooling AfterImage;
    public override void Enter()
    {
        base.Enter();
        speedCoroutine = TimeManager.Instance.WaitForSeconds(2f,IncreaseSpeed);
        currentSpeed = data.movementSpeed;
        isRunning = false;
        AfterImage = player.Core.PoolingComponent.GetGameObjectPooling("MainAfterImage");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(isStateForceChanged) return;

        Movement.CheckIfShouldFlip((int)input.x);
        Movement.SetVelocityX(currentSpeed * input.x);

        if(InputHandler.isFocus){
            stateMachine.ChangeState(player.Factory.FocusState);
            return;
        }
        if(input.x == 0f){
            stateMachine.ChangeState(player.Factory.IdleState);
            return;
        }
        if(isRunning){
            AfterImage.GetGameObject();
            return;
        }
    }
    public override void Exit()
    {
        base.Exit();
        player.Animator.speed = 1;
        TimeManager.Instance.CancelCoroutine(speedCoroutine);
    }
    private void IncreaseSpeed(){
        if(player == null || player.Animator == null) return;
        player.Animator.speed = 1.2f;
        currentSpeed *= 1.5f;
        isRunning = true;
    }
}