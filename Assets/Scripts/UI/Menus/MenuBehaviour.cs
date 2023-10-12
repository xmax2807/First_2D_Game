using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public abstract class MenuBehaviour : MonoBehaviour, GamePlayInputMap.IInGameMenuActions 
{
    [SerializeField] protected Selectable FirstSelectable;

    protected MenuAudioPreset MenuSFX => AudioManager.Instance.SoundFX.MenuAudio;
    public virtual bool isRootMenu {get;} = false;
    protected virtual void Awake(){
        if(FirstSelectable == null) {
            FirstSelectable = GetComponentInChildren<Selectable>();
        }
    }
    protected virtual void Start(){
        gameObject.SetActive(false);
    }
    public virtual void Entry(){
        if(this == null || gameObject == null) return;

        gameObject.SetActive(true);
        SelectFirstSelectable();
    }
    public virtual void Exit(){
        if(this == null || gameObject == null) return;
        
        gameObject.SetActive(false);
    }

    public virtual void OnRemovedFromStack(){
        Exit();
    }
    public virtual void OnAddedToStack(){
        Entry();
    }

    protected void ToggleActiveOtherUI(bool value){
        foreach(Transform children in transform.parent){
            var childCanvas = children.GetComponent<Canvas>();
            if(childCanvas == null) continue; 
            childCanvas.enabled = value;
        }
    }

    public virtual void OnToggleMenu(InputAction.CallbackContext context){
        if(!context.started) return;
        MenuManager.Instance.PopMenu();
    }
    public virtual void OnNavigationTab(InputAction.CallbackContext context){}

    protected virtual void SelectFirstSelectable(){
        EventSystem.current.SetSelectedGameObject(null);
        TimeManager.Instance.WaitFor(
            new WaitForEndOfFrame(), 
            ()=>EventSystem.current.SetSelectedGameObject(FirstSelectable.gameObject)
        );
    }

    public virtual void OnNavigationUpDown(InputAction.CallbackContext context){}

    public virtual void OnNavigationLeftRight(InputAction.CallbackContext context){}

    public virtual void UpdateMenuUI(){}

    public void PushSelfToStack() => MenuManager.Instance.PushMenu(this);
}
