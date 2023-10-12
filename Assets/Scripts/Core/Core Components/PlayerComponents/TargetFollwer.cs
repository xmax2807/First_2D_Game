using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollwer : MonoBehaviour
{
    private Transform _attachedTarget;
    void OnEnable(){
        if(_attachedTarget == null){
            SelfDeactivate();
            return;
        }
        
        transform.position = _attachedTarget.position;
    }
    void Update(){
        if(_attachedTarget == null) {
            SelfDeactivate();
            return;
        }
        transform.position = _attachedTarget.position;
    }    

    public void SelfActive(Transform target){
        _attachedTarget = target;
        gameObject.SetActive(true);
    }
    public void SelfDeactivate(){
        gameObject.SetActive(false);
        _attachedTarget = null;
    }
}
