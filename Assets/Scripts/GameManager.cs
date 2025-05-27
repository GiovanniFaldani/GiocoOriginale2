using System.Collections.Concurrent;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Starting player HP")]
    [SerializeField] private int startingHp = 10;
    [SerializeField] private int currentHp;
    [Header("Starting player Money")]
    [SerializeField] private int startingMoney = 20;
    [SerializeField] private int currentMoney;

    private int currentWave = 0;
    private int[] highScores = { 0, 0, 0 };

    // Singleton behavior
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentHp = startingHp;
        currentMoney = startingMoney;
    }

    public void AddToHp(int addendum)
    {
        currentHp += addendum;
        CheckDeath();
    }

    public void AddToWave(int addendum)
    {
        currentWave += addendum;
    }

    public int GetCurrentMoney()
    {
        return currentMoney;
    }

    public void AddToMoney(int addendum)
    {
        currentMoney += addendum;
    }

    private void CheckDeath()
    {
        if(currentHp <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.Lose);
    }

    private void WinGame()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.Win);
    }

}
