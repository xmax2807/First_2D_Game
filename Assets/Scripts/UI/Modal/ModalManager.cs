using UnityEngine;
using System.Collections.Generic;
using System;
public class ModalManager : MonoBehaviour{
    public static ModalManager Instance;
    [SerializeField] private BaseModal ConfirmModal;
    [SerializeField] private BaseModal GameTutorModal;
    [SerializeField] private BaseModal ControlTutorModal;

    private Canvas canvas;
    void Awake(){
        canvas = GetComponent<Canvas>(); 
        canvas.worldCamera = Camera.main;
        canvas.renderMode = RenderMode.ScreenSpaceCamera;

        if(Instance == null) {
            Instance = this;
            SavedModals = new()
            {
                { ModalType.ControlTutorialModal.ToString(), ControlTutorModal },
                { ModalType.GameTutorialModal.ToString(), GameTutorModal },
                { ModalType.ConfirmModal.ToString(), ConfirmModal }
            };
        }
        InputManager.Controls.InGameModal.SetCallbacks(null);
        InputManager.Controls.InGameModal.Disable();
    }

    void Start(){
        canvas.enabled = false;
        foreach(var modal in SavedModals.Values){
            modal?.gameObject.SetActive(false);
        }
    }
    public void DisplayModal()=>canvas.enabled = true;
    public void CloseModal()=>canvas.enabled = false;
    
    public enum ModalType{
        ControlTutorialModal, GameTutorialModal, ConfirmModal
    }

    private Dictionary<string, BaseModal> SavedModals;
    
    public T CreateModal<T>(string name) where T : BaseModal{
        if(!typeof(T).IsSubclassOf(typeof(BaseModal))) return null;

        if(SavedModals.ContainsKey(name)) return null;

        var newModal = Instantiate(new GameObject(name), this.transform);
        newModal.AddComponent<Canvas>();
        return newModal.AddComponent<T>();
    }

    public bool TryGetBuiltModal(string name, out BaseModal result){
        return SavedModals.TryGetValue(name, out result);
    }

    public BaseModal BuiltModal(ModalType type){
        if(!TryGetBuiltModal(type.ToString(), out var result))
            return null;
        return result;
    }
}