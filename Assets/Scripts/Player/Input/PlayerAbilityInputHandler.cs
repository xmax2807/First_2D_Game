using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAbilityInputHandler : MonoBehaviour{

    private int _abilityIndex = 0;
    public int AbilityIndex {get => _abilityIndex; 
        private set{
            if(_abilityIndex != value) OnAbilityChanged?.Invoke(value);
            _abilityIndex = value;
        }
    }
    public System.Action<int> OnAbilityChanged; 
    public void AbilityInputListener(InputAction.CallbackContext context){
        // if(!context.performed){
        //     // if(!context.canceled){
        //     // }
        //     return;
        // }
        if(!context.started) return;
        var vector = context.ReadValue<Vector2>();
        if(vector.x == 0){
            AbilityIndex = vector.y == 1? 0 : 2;
        }
        if (vector.y == 0){
            AbilityIndex = vector.x == 1? 1 : 3;
        }
    }

    public bool IsAbilityPressed;
    public void AbilityActionListener(InputAction.CallbackContext context){
        IsAbilityPressed = !context.canceled;
    }
}