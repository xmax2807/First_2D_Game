using UnityEngine;
using System.Collections;
public abstract class SelfDestructionItem : BaseItem{
    [SerializeField] protected float TimeOut = 5f;
    protected override void Start(){
        base.Start();
        TimeManager.Instance.WaitForSeconds(TimeOut, TryDestroy);
    }
    private void TryDestroy(){
        if(this == null) return;
        Destroy(gameObject);
    }
}