using UnityEngine;

public class GameOverMenu : NavigationMenu{
    public void UseRevive(){}
    public void ReturnLastCheckPoint(){}
    public void ReturnMainMenu()=> GameManager.Instance.LoadScene("MainMenuScene", null);
}