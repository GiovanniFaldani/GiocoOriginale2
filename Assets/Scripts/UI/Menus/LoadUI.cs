using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUI : BaseUI
{
    [SerializeField] GameObject screen1;
    [SerializeField] GameObject screen2;

    private void OnEnable()
    {
        if(Random.Range(0,99) > 49)
        {
            screen1.SetActive(true);
            screen2.SetActive(false);
        }
        else
        {
            screen1.SetActive(false);
            screen2.SetActive(true);
        }
    }

    public void GoToHud()
    {
        Time.timeScale = 1f;
        UIManager.Instance.ShowUI(UIManager.GameUI.HUD);
    }
}
