using UnityEngine;

public abstract class RangeWeapon : MonoBehaviour{

    [SerializeField] protected Animator weaponAnimator;

    public System.Action OnFinishAttackCallBack;
    protected float startTime;
    public virtual void WeaponEnter(){
        startTime = Time.time;
        TimeManager.Instance.WaitForSeconds(1f, WeaponExit);
    }
    public abstract void CollidedObject(Collider2D collider);
    public abstract void LoseObject(Collider2D collider);
    public abstract void TriggerAttack();
    public abstract void ReadyToAttack();

    public virtual void Update(){}
    public virtual void BeginAnimation(){}
    public void FinishAnimation() => OnFinishAttackCallBack?.Invoke();
    public virtual void WeaponExit(){
        if(this == null) return;
        Destroy(this.gameObject);
    }

    public void SetBool(string name, bool value){
        weaponAnimator?.SetBool(name,value);
    }
    public void SetInt(string name, int value){
        weaponAnimator?.SetInteger(name,value);
    }
    public void SetFloat(string name, float value){
        weaponAnimator?.SetFloat(name, value);
    }
}

public abstract class  RangeWeapon<T> : RangeWeapon where T : Entity{
    protected T Holder;
    protected ITargetable<T> target;
    protected float damage;
    public virtual void Init(T holder, float damage){
        Holder = holder;
        this.damage = damage;
    }
}
