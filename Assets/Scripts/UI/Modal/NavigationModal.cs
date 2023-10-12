using UnityEngine;
using UnityEngine.InputSystem;

public class NavigationModal : BaseModal{
    [Header("Navigates")]
    [SerializeField] protected GameObject PreviousButton;
    [SerializeField] protected GameObject NextButton;

    public System.Action OnCloseModal;

    public override void OnClose(InputAction.CallbackContext context)
    {
        if(!context.started) return;

        this.gameObject.SetActive(false);
        OnCloseModal?.Invoke();
    }

    public override void OnNavigation(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        
        var audioClip = AudioManager.Instance.SoundFX.MenuAudio.Navigate.Clip;
        AudioManager.Instance.PlayOneShot(audioClip);

        int direction = context.ReadValue<float>() > 0 ? 1 : -1;
        currentIndex = Mathf.Clamp(currentIndex + direction, 0, ListOfModalData.Count - 1);
        UpdateUI();
    }

    protected override void UpdateUI()
    {
        NextButton.SetActive(currentIndex < ListOfModalData.Count - 1);
        PreviousButton.SetActive(currentIndex > 0);
        base.UpdateUI();
    }
}