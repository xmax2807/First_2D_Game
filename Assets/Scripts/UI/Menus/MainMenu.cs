using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : NavigationMenu
{
    public override bool isRootMenu => true;
    protected override void Awake()
    {
        base.Awake();
        TimeManager.Instance.WaitUntil(()=>SceneManager.GetActiveScene().isLoaded, PushSelfToStack);
    }
    protected override void Start()
    {
    }
    public void ContinueLatestData() { }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    public void NewGame()
    {
        InGameDataManager.Instance.NewGame();
        GameManager.Instance.LoadScene(
            "Level1",null
        );
    }
}