using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine;

public class NavigationMenu: MenuBehaviour{
    protected int _currentIndex = 0;
    private Selectable[] _allButtons ;
    protected Selectable[] allButtons{
        get{
            if(_allButtons == null){
                _allButtons = GetComponentsInChildren<Selectable>();
            }
            return _allButtons;
        }
    }

    protected override void SelectFirstSelectable()
    {
        FirstSelectable = allButtons[_currentIndex];
        base.SelectFirstSelectable();
    }
    public override void OnToggleMenu(InputAction.CallbackContext context){}
    public override void OnNavigationUpDown(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        AudioManager.Instance.PlayOneShot(MenuSFX.Navigate.Clip);

        int direction = context.ReadValue<float>() > 0? 1 : -1;
        _currentIndex = (_currentIndex + direction + allButtons.Length) % allButtons.Length;

        SelectFirstSelectable();
    }
}