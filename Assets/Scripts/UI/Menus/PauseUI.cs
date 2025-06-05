using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : BaseUI
{

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToHud()
    {
        Time.timeScale = 1.0f;
        UIManager.Instance.ShowUI(UIManager.GameUI.HUD);
    }
}
