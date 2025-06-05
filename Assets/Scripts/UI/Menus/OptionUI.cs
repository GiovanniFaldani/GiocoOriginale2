using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionUI : BaseUI
{
    [SerializeField] GameObject[] difficultyPanels = new GameObject[3];

    public void GoToMainMenu()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.MainMenu);
    }

    public void GoToManual()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.Manual);
    }

    public void ChangeDifficulty()
    {
        GameManager.Instance.difficulty += 1;

        if (GameManager.Instance.difficulty > 2)
        {
            GameManager.Instance.difficulty = 0;
        }

        for (int i = 0; i < difficultyPanels.Length; i++)
        {
            if (i == GameManager.Instance.difficulty)
            {
                difficultyPanels[i].SetActive(true);
            }
            else
            {
                difficultyPanels[i].SetActive(false);
            }
        }
    }
}
