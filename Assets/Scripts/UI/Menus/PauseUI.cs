using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : BaseUI
{

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToHud()
    {
        Time.timeScale = 1;
        UIManager.Instance.ShowUI(UIManager.GameUI.HUD);
    }
}
