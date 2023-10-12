using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


public class InputManager : MonoBehaviour
{
    public static InputManager Instance {get;private set;}
    public void Awake(){
        if(Instance == null){
            Instance = this;
            Instance._Controls = new GamePlayInputMap();
            //Instance._Controls.UI.Enable();
            //Instance._Controls.InGameMenu.Enable();
        }
        Instance.EnableGameplay();
    }
    private GamePlayInputMap _Controls;
    public static GamePlayInputMap Controls => Instance._Controls;
    public void EnableGameplay(){
        foreach(var map in Controls.asset.actionMaps){
            map.Disable();
        }
        Controls.GamePlay.Enable();
    }
    public void DisableGamePlay(){
        foreach(var map in Controls.asset.actionMaps){
            map.Enable();
        }
        Controls.GamePlay.Disable();
    }
    public List<InputActionMap> GetCurrentActiveActionMaps(){
        List<InputActionMap> result = new();
        foreach(var map in Controls.asset.actionMaps){
            if(map.enabled) {
                result.Add(map);
            }
        }
        return result;
    }
}
