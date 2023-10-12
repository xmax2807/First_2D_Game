using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
public class TabMenu : MenuBehaviour
{
    private int _currentTabIndex = 0;

    [Header("Tab Panel")]

    [SerializeField] private GameObject tabPanel;
    [SerializeField] private GameObject PanelContainer;
    private SwapableImage[] _tabButtons;
    private MenuBehaviour[] _Panels;

    [HideInInspector]public string[] MenuPanelNames;
    [SerializeField]public int selectedMenuFlag = -1;

    #region InEditor
#if UNITY_EDITOR
    public void GetSubMenus(){
        _Panels ??= PanelContainer.GetComponentsInChildren<MenuBehaviour>(true);
        MenuPanelNames ??= _Panels.Select((menu) => menu.GetType().ToString()).ToArray();
    }
#endif
    #endregion
    protected override void Awake(){
        base.Awake();
        _tabButtons = tabPanel.GetComponentsInChildren<SwapableImage>();
        _Panels ??= PanelContainer.GetComponentsInChildren<MenuBehaviour>(true);
        FilterMenu();
    }
    private void FilterMenu(){
        if(selectedMenuFlag != -1){
            
            List<MenuBehaviour> filteredList = new();
            List<SwapableImage> tabs = new(_tabButtons);

            for(int i = 0; i < MenuPanelNames.Length; i++){
                int layer = 1 << i;
                if ((selectedMenuFlag & layer) != 0)
                {
                    filteredList.Add(_Panels[i]);
                }
                else{
                    tabs[i].gameObject.SetActive(false);
                    tabs.RemoveAt(i);
                }
            }
            
            _Panels = filteredList.ToArray();
            _tabButtons = tabs.ToArray();
        }
    }
    public override void OnNavigationTab(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        ToggleNextTab();
    }

    public override void OnNavigationUpDown(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        _Panels[_currentTabIndex].OnNavigationUpDown(context);
    }

    public override void Entry()
    {
        _currentTabIndex = 0;
        UpdateMenu();
        base.Entry();
    }

    private void ToggleNextTab(){
        
        int nextIndex = (_currentTabIndex + 1 + _Panels.Length) % _Panels.Length;
        if(nextIndex == _currentTabIndex) return;
        _currentTabIndex = nextIndex;
        UpdateMenu();
    }
    private void UpdateMenu(){
        for(int i = 0; i < _tabButtons.Length; i++){
            int index = i;
            TimeManager.Instance.WaitFor(new WaitForEndOfFrame(), 
            ()=>{
                SwitchActivePanel(_currentTabIndex == index, index);
            });
        }
        SelectFirstSelectable();
    }
    protected virtual void SwitchActivePanel(bool value, int index = 0){
        _Panels[index].gameObject.SetActive(value);

        if(value) {
            _tabButtons[index].Select();
            _Panels[index].UpdateMenuUI();
        } 
        else _tabButtons[index].UnSelect();
    }

    protected override void SelectFirstSelectable(){
        Selectable[] selectableList = _Panels[_currentTabIndex].GetComponentsInChildren<Selectable>();
        if(selectableList == null) return;

        FirstSelectable = selectableList.FirstOrDefault((first) => first != null && first.interactable);
        base.SelectFirstSelectable();
    }
}
