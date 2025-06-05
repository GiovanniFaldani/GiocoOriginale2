using UnityEngine;
using UnityEngine.SceneManagement;

public class ManualUI : BaseUI
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToOptions()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.Option);
    }
}
