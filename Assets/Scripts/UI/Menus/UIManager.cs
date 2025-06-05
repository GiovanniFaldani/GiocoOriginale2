using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI monetePanel;
    public static UIManager Instance { get; private set; }

    public enum GameUI
    {
        NONE,
        MainMenu,
        HUD,
        Pause,
        Load,
        Win,
        Lose,
        Option,
        Manual
    }

    private Dictionary<GameUI, IGameUI> registeredUIs = new Dictionary<GameUI, IGameUI>();
    public Transform UIContainer;
    private GameUI currentActiveUI = GameUI.NONE;
    public GameUI startingGameUI;

    public void RegisterUI(GameUI uiType, IGameUI uiToRegister)
    {
        registeredUIs.Add(uiType, uiToRegister);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;

        foreach (IGameUI enumeratedUI in UIContainer.GetComponentsInChildren<IGameUI>(true))
        {
            RegisterUI(enumeratedUI.GetUIType(), enumeratedUI);
        }

        ShowUI(startingGameUI);
    }

    private void Update()
    {
        monetePanel.text = GameManager.Instance.currentMoney.ToString();
    }

    public void ShowUI(GameUI uiType)
    {
        foreach (KeyValuePair<GameUI, IGameUI> kvp in registeredUIs)
        {
            kvp.Value.SetActive(kvp.Key == uiType);
        }

        currentActiveUI = uiType;
    }

    public GameUI GetCurrentActiveUI()
    {
        return currentActiveUI;
    }
}