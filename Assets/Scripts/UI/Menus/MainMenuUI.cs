using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : BaseUI
{
    private void Awake()
    {
        Time.timeScale = 0f;
    }

    private void Start()
    {
        
    }

    public void GoToLoadingScreen()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.Load);
    }

    public void GoToHud()
    {
        Time.timeScale = 1f;
        UIManager.Instance.ShowUI(UIManager.GameUI.HUD);
    }

    public void GoToOptions()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.Option);
    }

    public void GoToManual()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.Manual);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
