using UnityEngine;
using System.Linq;
using System.Collections;
public class BreakableItem : BaseItem, ITargetable<Player>{
    protected bool isBroken = false;
    [SerializeField]ScriptableDropItem dropItem;
    [SerializeField] protected BaseAnimationScript onHitAnimation;

    public bool IsInvisible => false;

    public Vector2 GetPosition() => transform.position;
    public void Damage(float amount){
        onHitAnimation?.StartAnimation();
        OnDamage(amount);
        
        if(isBroken) OnDestroyState();
    }
    public void Damage(Player player,float amount) => Damage(amount);

    protected virtual void OnDamage(float amount){
        isBroken = true;
    }
    protected override void ApplyItem(Player player){
        //DoNothing
    }
    protected override void OnDestroyState(){
        if(this == null) return;
        SpawnSystem.Instance.SpawnObject(dropItem.SpawnEffect,transform.position);
        SpawnSystem.Instance.SpawnObjectRandomly(dropItem.Item, transform.position, dropItem.NumberOfItem);
        Destroy(gameObject);
    }

    public void OnBeforeTakingDamage(Player attacker){}
}