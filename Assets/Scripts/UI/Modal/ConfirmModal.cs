using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ConfirmModal : BaseModal{

    [SerializeField]private string[] ButtonNames = new string[2];
    public string YesConfirm{get;private set;}
    public string NoConfirm{get;private set;}
    protected Button[] Buttons;

    protected void Awake(){
        PopulateButton();
        if(ButtonNames.Length < 2) return;
        YesConfirm = ButtonNames[0];
        NoConfirm = ButtonNames[1];
    }
    public void PopulateButton(){
        if(Buttons == null || Buttons.Length == 0){
            Buttons = new Button[ButtonNames.Length];
            for(int i = 0; i < ButtonNames.Length; i++){
                Buttons[i] = AddButtonEvent(ButtonNames[i]);
            }

            for(int i = 0; i < Buttons.Length; i++){                
                int prev = (i - 1 + Buttons.Length) % Buttons.Length;
                int next = (i + 1 + Buttons.Length) % Buttons.Length;
                
                Buttons[i].navigation = new Navigation(){
                    mode = Navigation.Mode.None,
                    // selectOnLeft = Buttons[prev],
                    // selectOnRight = Buttons[next]
                };
            }
        }
    }
    public override void OnEnable()
    {
        base.OnEnable();
        //PopulateButton();
        YesConfirm = ButtonNames[0];
        NoConfirm = ButtonNames[1];
        Callback += Confirm;
        currentIndex = 0;
        Buttons[currentIndex].Select();
        UnityEngine.EventSystems.EventSystem.current.firstSelectedGameObject = Buttons[currentIndex].gameObject;
    }
    public override void OnDisable()
    {
        Callback -= Confirm;
        base.OnDisable();
    }
    public virtual void Confirm(string name){
        if(this == null) return;
        gameObject?.SetActive(false);
    }
    public override void OnClose(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        
        if(this == null) return;
        Callback?.Invoke(Buttons[currentIndex].name);
        gameObject?.SetActive(false);
    }
    public override void OnNavigation(InputAction.CallbackContext context)
    {
        int direction = context.ReadValue<float>() > 0 ? 1 : -1;
        currentIndex = (currentIndex + direction + ButtonNames.Length) % ButtonNames.Length;
        Buttons[currentIndex].Select();
    }
}