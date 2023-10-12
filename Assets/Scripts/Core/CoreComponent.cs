using UnityEngine;
public abstract class CoreComponent : MonoBehaviour{
    protected BaseCore core {get;private set;}
    protected virtual void Awake(){
        core = transform.parent.GetComponent<BaseCore>();

        if(core == null){
            // Debug
        }
    }

    public virtual void OnEnable(){}
    public virtual void OnDisable(){}
}
public abstract class PlayerCoreComponent : CoreComponent{
    public Core Core => (Core)core;
}
public abstract class EnemyCoreComponent : CoreComponent{
    public EnemyCore Core => (EnemyCore)core;
}