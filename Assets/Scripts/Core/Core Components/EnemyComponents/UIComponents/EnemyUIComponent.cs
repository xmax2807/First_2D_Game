using UnityEngine;
public class EnemyUIComponent : EnemyCoreComponent{
    [SerializeField] BarComponent HPBar;
    protected override void Awake(){
        base.Awake();
        HPBar.Init(Core.EnemyStat.HitPoint);
    }
    public void Damage(float amount){
        HPBar.UpdateValue(amount < 0f ? amount : -amount);
        if(HPBar.GetCurrentValue() <= 0f){
            Core.Enemy.StateMachine.ChangeState(Core.Enemy.Factory.DeathState);
        }
    }
}