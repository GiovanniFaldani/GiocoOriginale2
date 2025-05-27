using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : BaseUI
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            UIManager.Instance.ShowUI(UIManager.GameUI.Pause);
            Time.timeScale = 0;
        }
    }
}
