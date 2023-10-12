using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ExpandableLayout : MonoBehaviour{
    public enum InsertMethod{
        First,AfterFirst, Last, BeforeLast
    }
    public UnityEvent OnExpanded;
    [SerializeField] private InsertMethod method;
    [SerializeField] private GameObject ExpandableObject;
    private int insertIndex = 0;
    private void Awake(){

        switch(method){
            case InsertMethod.First: insertIndex = 0; 
            break;
            case InsertMethod.AfterFirst: insertIndex = 1; 
            break;
            case InsertMethod.Last: insertIndex = transform.childCount - 1; 
            break;
            case InsertMethod.BeforeLast: insertIndex = transform.childCount - 2; 
            break;
        }
    }
    public void ExpandObject(){
        var obj = Instantiate(ExpandableObject, transform);
        obj.transform.SetSiblingIndex(insertIndex);
        OnExpanded?.Invoke();
    }
}