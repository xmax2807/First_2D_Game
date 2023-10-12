using UnityEngine;
public class BaseBarUI : MonoBehaviour{
    protected BaseAnimationScript<float> animationScript;
    private void Awake(){
        animationScript = GetComponent<BaseAnimationScript<float>>();
    }
    protected virtual void Start(){
        if(animationScript == null) return;
        animationScript.enabled = false;
        enabled = false;
    }

    public virtual void UpdateValue(float amount, bool skipAnimation = false) {
        // if(animationScript == null) {
        //     TimeManager.Instance.WaitUntil(()=>animationScript != null, ()=>{
        //         UpdateValue(amount, skipAnimation);
        //     });
        //     return;
        // }
        enabled = true;
        if(animationScript == null){ 
            animationScript = GetComponent<BaseAnimationScript<float>>();
            
            if(animationScript == null)return;
        }

        animationScript.enabled = true;
        animationScript.SettingUp(0f, amount);
        animationScript.OnAnimationFinished += ()=> animationScript.enabled = false;
        animationScript.StartAnimation(skipAnimation);
    }
}