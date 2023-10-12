using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using ExtensionMethods;

[Serializable]
public class ModalData
{
    public string Title;
    [TextArea]public string Description;
    public Sprite Image;
}

public class ScriptableModalData : ScriptableObject
{
    [SerializeField] public ModalData[] SavedData;
}

public abstract class BaseModal : MonoBehaviour, GamePlayInputMap.IInGameModalActions
{
    [Header("Base Modal Attributes")]
    [SerializeField] protected Transform ButtonPanel;
    [SerializeField] protected GameObject ButtonSample;
    [SerializeField] protected TextMeshProUGUI TitleText;
    [SerializeField] protected TextMeshProUGUI DescriptionText;
    [SerializeField] protected Image ImageHolder;
    [SerializeField] Sprite DefaultImageHolder;
    protected List<ModalData> ListOfModalData;
    protected int currentIndex;
    public delegate void OnButtonClickCallback(string status);
    protected OnButtonClickCallback Callback;
    public void AddListener(OnButtonClickCallback action){
        Callback += action;
    }
    public Button AddButtonEvent(string name)
    {
        name = name.UpperAtFirst();

        var newButtonObj = Instantiate(ButtonSample, ButtonPanel);
        var newButton = newButtonObj.GetComponent<Button>();
        AttachDataToButton(newButton, name);
        return newButton;
    }

    protected void AttachDataToButton(Button button, string name){
        button.GetComponentInChildren<TextMeshProUGUI>().text = name;
        button.onClick.AddListener(() => Callback?.Invoke(name));
    }

    public void ShowModal(ModalData data){
        if (data != null) ListOfModalData = new (){data};
        ShowModal();
    }
    public void ShowModal(ModalData[] data)
    {
        if (data != null) ListOfModalData = new List<ModalData>(data);
        ShowModal();
    }
    public void ShowModal()
    {
        if (ListOfModalData == null || ListOfModalData.Count == 0) return;

        currentIndex = 0;
        UpdateUI();
        gameObject.SetActive(true);
    }

    protected virtual void UpdateUI(){
        SetData(ListOfModalData[currentIndex]);
    }

    private void SetData(ModalData data){
        TitleText.text = data.Title;
        DescriptionText.text = data.Description;

        if(ImageHolder == null) return;

        if(data.Image == null){
            ImageHolder.sprite = DefaultImageHolder;
        }
        else{
            ImageHolder.sprite = data.Image;
        }
        ImageHolder.preserveAspect = true;
    }

    private List<InputActionMap> lastActiveActionMaps;
    public virtual void OnEnable(){
        lastActiveActionMaps = InputManager.Instance.GetCurrentActiveActionMaps();
        foreach(var map in lastActiveActionMaps){
            map.Disable();
        }
        InputManager.Controls.InGameModal.SetCallbacks(this);
        InputManager.Controls.InGameModal.Enable();
        ModalManager.Instance?.DisplayModal();
    }

    public virtual void OnDisable(){
        foreach(var map in lastActiveActionMaps){
            map.Enable();
        }
        InputManager.Controls.InGameModal.Disable();
        ModalManager.Instance?.CloseModal();
        Callback = null;
    }

    public virtual void OnClose(InputAction.CallbackContext context){}

    public virtual void OnNavigation(InputAction.CallbackContext context){}
}