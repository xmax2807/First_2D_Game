using System.Collections.Generic;
using UnityEngine;

public class ExplosionWeapon : RangeWeapon<Player>
{
    List<ITargetable<Player>> CacheEnemies = new();
    public override void WeaponEnter()
    {
        base.WeaponEnter();
        target = Holder.Core.VisibilityComponent.GetClosestEnemy();
        this.transform.position = target == null? 
            Holder.GetPosition() + new Vector2(5 * Holder.Core.Movement.FacingDirection, 0) : 
            target.GetPosition(); 
    }
    public override void CollidedObject(Collider2D collider)
    {
        if(!collider.TryGetComponent<ITargetable<Player>>(out var enemy)) return;
        
        if(CacheEnemies.Contains(enemy)) return;
        CacheEnemies.Add(enemy);
    }

    public override void LoseObject(Collider2D collider){}

    public override void ReadyToAttack(){}

    public override void TriggerAttack()
    {
        foreach(var enemy in CacheEnemies){
            enemy.Damage(Holder, damage);
        }
    }
}