using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;    
    private Stack<MenuBehaviour> _menuStack;
    public MenuBehaviour CurrentMenu => _menuStack.Peek();
    private MenuBehaviour RootMenu;
    private void Awake(){
        if(Instance == null || Instance != this) Instance = this;
        Instance._menuStack = new();
    }
    public void PushMenu(MenuBehaviour menu){
        if(menu == null) return;

        if(_menuStack.TryPeek(out var topMenu)){
            topMenu.Exit();
        }
        else{
            InputManager.Controls.GamePlay.Disable();
            InputManager.Controls.InGameMenu.Enable();
        }
        menu.OnAddedToStack();
        InputManager.Controls.InGameMenu.SetCallbacks(menu);
        _menuStack.Push(menu);
    }

    public void PopMenu(){
        if(_menuStack.TryPop(out var menu)){
            menu.OnRemovedFromStack();
        }
        if(_menuStack.TryPeek(out var currentMenu)){
            currentMenu.Entry();
            InputManager.Controls.InGameMenu.SetCallbacks(currentMenu);
        }
        else{
            InputManager.Controls.GamePlay.Enable();
            InputManager.Controls.InGameMenu.Disable();
        }
    }

    public void PopAll(){
        while(_menuStack.TryPop(out var menu)){
            menu.OnRemovedFromStack();
        }
        InputManager.Controls.GamePlay.Enable();
        InputManager.Controls.InGameMenu.Disable();
    }

    public void PushRootMenu(){
        if(RootMenu == null){
            RootMenu = FindObjectsOfType<MenuBehaviour>(includeInactive:true).Where((menu)=> menu.isRootMenu).FirstOrDefault();
        }
        PushMenu(RootMenu);
    }
}
