using UnityEngine;

public class MagicSword : RangeWeapon<Player>
{
    private Quaternion targetRotation;
    public override void Init(Player holder, float damage)
    {
        base.Init(holder, damage);
        transform.rotation = holder.transform.rotation;
        target = !holder.Core.InputHandler.isFocus? null : holder.Core.VisibilityComponent.GetClosestEnemy();

        Vector3 frontVector = Vector3.right * Holder.Core.Movement.FacingDirection;

        if(target != null){
            targetRotation = Quaternion.FromToRotation(frontVector, target.GetPosition() - (Vector2)transform.position);
        }
        else{
            targetRotation = Quaternion.identity;
        }
    }
    public override void CollidedObject(Collider2D collider)
    {
        if(!collider.TryGetComponent<ITargetable<Player>>(out var enemy)){
            return;
        }
        enemy.Damage(Holder, damage);
        WeaponExit();
    }
    public override void Update()
    {
        base.Update();
        transform.Translate(Vector3.right * 50f * Time.deltaTime,Space.Self);
        if(!Holder.Core.InputHandler.isFocus || target == null) return;

        Vector3 frontVector = Vector3.right; // Set your front vector here


        targetRotation = Quaternion.FromToRotation(frontVector, target.GetPosition() - (Vector2)transform.position);
        
        this.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.75f);
    }
    public override void LoseObject(Collider2D collider){}

    public override void ReadyToAttack(){}

    public override void TriggerAttack(){
        target.Damage(Holder, damage);
        WeaponExit();
    }
}