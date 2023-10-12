using UnityEngine;
public interface ITargetable<T>{
    public Vector2 GetPosition();
    public void Damage(float amount);
    public void Damage(T enemy,float amount);
    public bool IsInvisible {get;}
    public void OnBeforeTakingDamage(T attacker);
}