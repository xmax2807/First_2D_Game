using UnityEngine;
public class PopupAnimation : BaseAnimationScript<Vector3>{
    [SerializeField] private float speed = 10f;    
    private void Update(){
        if(this.transform.position.y >= EndPoint.y){
            OnAnimationFinished?.Invoke();
            return;
        }
        this.transform.position += speed * Time.deltaTime * Vector3.up; 
    }
}