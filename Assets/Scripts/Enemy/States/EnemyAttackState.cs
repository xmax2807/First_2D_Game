using UnityEngine;
public class EnemyAttackState : EnemyState{
    public EnemyAttackState(EnemyEntity other, string animName) : base(other, animName){

        foreach(var weapon in other.Weapons){
            weapon.OnFinishAttackCallBack += ()=>stateMachine.ChangeState(enemy.Factory.IdleState);
        }
    }
    protected ITargetable<EnemyEntity> Player => enemy.Player;

    protected int currentWeaponIndex = 0;
    protected BaseWeapon HoldingWeapon => enemy.Weapons.Length == 0? null : enemy.Weapons[currentWeaponIndex];
    public bool CanAttack => CheckIfCanAttack();

    private bool isReadyAttack;
    public override void Enter()
    {
        base.Enter();
        if(HoldingWeapon == null){
            stateMachine.ChangeState(enemy.Factory.IdleState);
            return;
        }
        
        HoldingWeapon?.WeaponEnter();
    }
    // public override void LogicUpdate()
    // {
    //     base.LogicUpdate();

    //     if(!CanAttack){
    //         stateMachine.ChangeState(enemy.DetectState);
    //         return;
    //     }
    // }

    public override void Exit()
    {
        base.Exit();
        HoldingWeapon?.WeaponExit();
        isReadyAttack = false;
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        Movement.SetVectorZero();
    }

    public void ReadyToAttack(){
        if(isReadyAttack) return;

        enemy.AttackCaution.PopUp(0.2f);
        TimeManager.Instance.WaitForSeconds(0.2f,()=>{
                
            if(enemy.IsDead) return;

            stateMachine.ChangeState(enemy.Factory.AttackState);
        });
        isReadyAttack = true;
    }

    protected bool CheckIfCanAttack(){
        if(Player == null) return false;

        bool delayed = Time.time < startTime + enemy.Data.delayAttackTime;
        bool withinAttack = Vector2.Distance(Player.GetPosition(), this.enemy.GetPosition()) <= enemy.Data.AttackRange;
        return !delayed && withinAttack; 
    }
}