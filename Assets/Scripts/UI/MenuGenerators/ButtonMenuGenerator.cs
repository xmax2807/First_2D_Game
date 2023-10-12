using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

[ExecuteInEditMode]
public class ButtonMenuGenerator : MonoBehaviour
{
    [SerializeField] protected Navigation.Mode navigationMode;
    [SerializeField] protected NavigationMenuData navigations;
    protected Dictionary<string,Button.ButtonClickedEvent> _callbacks;
    protected Dictionary<string,Button.ButtonClickedEvent> Callbacks {
        get{
            _callbacks ??= new();
            return _callbacks;
        }
    }
    [SerializeField] protected GameObject prefab;
    
    public virtual void PopulateButtons(){
        if(navigations == null){
            Debug.LogWarning("Null Navigation Data");
            return;
        }
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        for(int i = 0; i < navigations.ListCallbacks.Length; i++){
            var newGameObj = Instantiate(prefab, transform);
            newGameObj.name = navigations.ListCallbacks[i].Name;
            ((RectTransform)newGameObj.transform).sizeDelta = ((RectTransform)prefab.transform).sizeDelta;
            var text = newGameObj.GetComponentInChildren<TextMeshProUGUI>();
            text.SetText(newGameObj.name);

            if(Callbacks.ContainsKey(newGameObj.name)){
                Debug.LogWarning("A duplicated name in menu");
                continue;
            }

            Callbacks[newGameObj.name] = navigations.ListCallbacks[i].Value;
            newGameObj.GetComponent<Button>().onClick = Callbacks[newGameObj.name];
        }
        AutoAddNavigation();
    }
    public void AutoAddNavigation(){
        Button[] listButton = GetComponentsInChildren<Button>();

        for(int i = 0; i < listButton.Length; i++){
            Navigation navigation = new()
            {
                mode = navigationMode,
                selectOnUp = listButton[(i + listButton.Length - 1) % listButton.Length],
                selectOnDown = listButton[(i + 1) % listButton.Length]
            };
            
            listButton[i].navigation = navigation; 
        }
    }

    public void UpdateEvent(){
        if(navigations == null) return;
        Button[] listButton = GetComponentsInChildren<Button>();
        for(int i = 0; i < navigations.ListCallbacks.Length; i++){
            string navigationName = navigations.ListCallbacks[i].Name;
            Callbacks[navigationName] = navigations.ListCallbacks[i].Value;
            listButton[i].onClick = Callbacks[navigationName];
        }
    }
}
