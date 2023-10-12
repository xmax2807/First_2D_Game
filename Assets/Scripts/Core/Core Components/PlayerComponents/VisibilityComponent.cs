using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityComponent : ObserverComponent<EnemyEntity>{
    [SerializeField] private float sensitivity = 1.5f;
    [SerializeField] private TargetFollwer Follwer;
    protected EnemyEntity ClosestEnemy{get;set;}
    protected EnemyEntity AlternativeClosest{get;set;} // whenever lock is off assign this to main one (ClosestEnemy)
    protected override void Awake()
    {
        base.Awake();
        Observer = this.transform;
    }
    protected override void FixedUpdate()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(Observer.position, (int)Distance * 2 * Vector2.one, angle:0, LayerMask);
        for (int i = hitColliders.Length - 1; i > -1; i--)
        {   
            var obj = hitColliders[i].transform.root.gameObject;

            VisibleObject[obj] = true;
            ToggleState (obj, true);
        }

        float minDistance = AlternativeClosest == null? float.MaxValue : Vector2.Distance(Observer.position, AlternativeClosest.GetPosition());
        foreach(var key in VisibleObject.Keys.ToList()){
            if(key == null){
                VisibleObject.Remove(key);
                continue;
            }
            
            if(VisibleObject[key] == false && ClosestEnemy != null){
                if(VisibleObject.ContainsKey(ClosestEnemy.gameObject)){
                    ClosestEnemy = null;
                }
                ToggleState(key, false);
                VisibleObject.Remove(key);
            }
            else{
                VisibleObject[key] = false;

                if(!key.TryGetComponent<EnemyEntity>(out var enemy)) continue;

                if(minDistance > Vector2.Distance(Observer.position, enemy.GetPosition())){
                    AlternativeClosest = enemy;
                }
            }
        }
        if(VisibleObject.Count == 0){
            AlternativeClosest = null;
            ClosestEnemy = null;
        }
        
        if(ClosestEnemy == null){
            ClosestEnemy = AlternativeClosest;
        }

        SetActiveFollower(Core.InputHandler.isFocus);
        
    }

    public void FocusEnemy(){
        
        if(ClosestEnemy == null){
            GiveUpEnemy();
            return;
        }
        
        float size = Vector2.Distance(ClosestEnemy.GetPosition(), Observer.position) * sensitivity;
        CameraManager.Instance.SetOrthographicSize(size);
        CameraManager.Instance.FollowTarget(Vector2.Lerp(ClosestEnemy.GetPosition(), Observer.position, 0.5f));
    }
    
    public void GiveUpEnemy(){
        CameraManager.Instance.SetOrthographicDefaultSize();
        CameraManager.Instance.FollowTarget(Core.Player.transform);
    }

    public EnemyEntity GetClosestEnemy() {
        if(ClosestEnemy == null){
            ClosestEnemy = AlternativeClosest;
        }
        return ClosestEnemy;
    }

    public void SetActiveFollower(bool value){
        if(value){
            Follwer.SelfActive(ClosestEnemy?.transform);
            FocusEnemy();
        }
        else {
            Follwer.SelfActive(null);
            GiveUpEnemy();
            ClosestEnemy = AlternativeClosest;
        }
    }

    // void OnDrawGizmos(){
    //     if(ClosestEnemy == null) return;
    //     Debug.DrawLine(Observer.position, ClosestEnemy.GetPosition());
    // }
}
