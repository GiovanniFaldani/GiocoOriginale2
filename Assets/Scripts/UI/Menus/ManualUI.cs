using UnityEngine;
using UnityEngine.SceneManagement;

public class ManualUI : BaseUI
{
    public void GoToMainMenu()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.MainMenu);
    }

    public void GoToOptions()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.Option);
    }
}
