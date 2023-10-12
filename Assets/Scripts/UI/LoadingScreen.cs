using UnityEngine;
public class LoadingScreen : MonoBehaviour{
    [SerializeField] private AutoFillAmountAnimation FillAmountAnimation;
    private const string fileName = "tips.txt";
    private string[] _tips;
    private string[] randomTips;
    void Awake(){
        _tips = TextFileIO.Load(Application.persistentDataPath, fileName);
        GenerateTips();
    }
    void OnEnable(){
        FillAmountAnimation.enabled = true;
        FillAmountAnimation.Reset();
        FillAmountAnimation.StartAnimation();
        InputManager.Controls.UI.Enable();
        TimeManager.Instance.PauseGame();
    }

    void OnDisable(){
        InputManager.Controls.UI.Disable();
        FillAmountAnimation.Reset();
        FillAmountAnimation.enabled = false;
        TimeManager.Instance.ResumeGame();
    }

    public void Finish(){
        FillAmountAnimation.IsDone = true;
        TimeManager.Instance.WaitForSecondsUnscaled(0.2f,()=>{
            gameObject.SetActive(false);
        });
    }

    private void GenerateTips()=> randomTips = RandomSystem.TakeRandomElements(_tips, 5);
}