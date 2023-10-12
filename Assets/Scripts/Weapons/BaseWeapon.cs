using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour{
    [SerializeField] protected Animator baseAnimator;
    [SerializeField] protected Animator weaponAnimator;

    public System.Action OnFinishAttackCallBack;
    protected float startTime;

    protected virtual void Start(){
        gameObject.SetActive(false);
    }
    public virtual void WeaponEnter(){
        gameObject.SetActive(true);
        SetBool("attack", true);
        startTime = Time.time;
    }
    public abstract void CollidedObject(Collider2D collider);
    public abstract void LoseObject(Collider2D collider);
    public abstract void TriggerAttack();
    public abstract void ReadyToAttack();

    public virtual void Update(){
        SetFloat("transitionValue", Mathf.Clamp(Time.time - startTime, 0f, 1f));
    }
    public virtual void BeginAnimation(){}
    public void FinishAnimation() => OnFinishAttackCallBack?.Invoke();
    public virtual void WeaponExit(){
        SetBool("attack", false);
        gameObject.SetActive(false);
    }

    public void SetBool(string name, bool value){
        baseAnimator.SetBool(name,value);
        weaponAnimator.SetBool(name,value);
    }
    public void SetInt(string name, int value){
        baseAnimator.SetInteger(name, value);
        weaponAnimator.SetInteger(name,value);
    }
    public void SetFloat(string name, float value){
        baseAnimator.SetFloat(name, value);
        weaponAnimator.SetFloat(name, value);
    }
}
public abstract class BaseWeapon<T> : BaseWeapon where T : Entity{
    
    protected T holder;
    protected void Awake(){
        holder = GetComponentInParent<T>();
    }
}