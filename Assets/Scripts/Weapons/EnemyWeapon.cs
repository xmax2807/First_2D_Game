using UnityEngine;
public class EnemyWeapon : BaseWeapon<EnemyEntity>{
    bool isCollided = false;
    public override void CollidedObject(Collider2D collider){
        if(!collider.TryGetComponent<ITargetable<EnemyEntity>>(out var Target)) return;
        
        isCollided = Target == holder.Player;
    }

    public override void LoseObject(Collider2D collider){
        if(!collider.TryGetComponent<ITargetable<EnemyEntity>>(out var Target)){
            return;
        }
        
        isCollided = !(Target == holder.Player);
    }

    public override void ReadyToAttack()
    {
        holder.Player?.OnBeforeTakingDamage(holder);
    }

    public override void TriggerAttack(){
        if(!isCollided) return;

        holder.Player?.Damage(holder, holder.Core.EnemyStat.Damage);
        isCollided = false;
    }
    
}