using UnityEngine;
public class AttackCaution : MonoBehaviour{
    [SerializeField] private PopupAnimation _animation;
    private Vector3 StartPosition;
    private void Start(){
        gameObject.SetActive(false);
    }
    public void PopUp(float duration = 0.5f){
        TimeManager.Instance.WaitForSeconds(duration,()=>{
            gameObject?.SetActive(false);
        });
        gameObject?.SetActive(true);

        StartPosition = this.transform.position;
        
        _animation?.SettingUp(StartPosition, StartPosition + 0.1f * Vector3.up);
        _animation.StartAnimation();
    }
}