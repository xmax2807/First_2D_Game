using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class PlayerWeapon : BaseWeapon<Player>
{

    private Dictionary<Collider2D, ITargetable<Player>> _cache;
    private Dictionary<Collider2D, ITargetable<Player>> CacheEnemies
    {
        get
        {
            _cache ??= new();
            return _cache;
        }
    }
    public override void CollidedObject(Collider2D collider)
    {
        if(!collider.TryGetComponent<ITargetable<Player>>(out var enemy)){
            return;
        }

        CacheEnemies.TryAdd(collider, enemy);
    }

    public override void LoseObject(Collider2D collider)
    {
        CacheEnemies.Remove(collider);
    }

    public override void ReadyToAttack(){}

    public override void TriggerAttack()
    {
        foreach(Collider2D enemyCollider in CacheEnemies.Keys.ToArray()){

            if(enemyCollider == null || CacheEnemies[enemyCollider] == null){
                CacheEnemies.Remove(enemyCollider);
                continue;
            }
            CacheEnemies[enemyCollider]?.Damage(holder, holder.Core.Stats.Damage);
        }
    }
}