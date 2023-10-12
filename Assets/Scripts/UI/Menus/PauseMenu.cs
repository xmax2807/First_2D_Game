using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PauseMenu : NavigationMenu
{
    public override bool isRootMenu => true;
    public override void OnToggleMenu(InputAction.CallbackContext context){
        if(context.started){
            if(gameObject.activeSelf) MenuManager.Instance.PopAll();
            else MenuManager.Instance.PushMenu(this);
        }
    }
    public override void OnAddedToStack()
    {
        TimeManager.Instance?.PauseGame();
        base.OnAddedToStack();
    }
    public override void OnRemovedFromStack()
    {
        base.OnRemovedFromStack();
        TimeManager.Instance?.ResumeGame();
    }
    public override void Exit()
    {
        TimeManager.Instance.WaitForSecondsUnscaled(0.2f,()=>{
            base.Exit();
        });
    }

    public void ContinueGame() => MenuManager.Instance.PopAll();
    public void ReplayLevel(){
    }
    public void ReturnMainMenu() { 
        var modal = ModalManager.Instance.BuiltModal(ModalManager.ModalType.ConfirmModal);
        modal.AddListener((name)=>{
            if(name == ((ConfirmModal)modal).YesConfirm){
                GameManager.Instance.LoadScene("MainMenuScene", null);
            }
        });
        modal.ShowModal(new ModalData(){
            Title = "Attention",
            Description = "Are you sure want to quit?"
        });
    }
}