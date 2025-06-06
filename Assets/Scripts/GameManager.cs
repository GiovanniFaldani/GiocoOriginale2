using System.Collections.Concurrent;
using System.Threading;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Starting player HP")]
    [SerializeField] private int startingHp = 10;
    [SerializeField] private int currentHp;
    [SerializeField] private GameObject[] cuori;
    [Header("Starting player Money")]
    [SerializeField] private int startingMoney = 20;
    [SerializeField] public int currentMoney;

    [SerializeField] private MessageUI messageUI;

    public int currentWave = 0;
    public int difficulty = 1; // 0, 1 or 2
    private int[] highScores = { 0, 0, 0 };

    // Singleton behavior
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        for( int i = 0; i < highScores.Length; i++)
        {
            highScores[i] = PlayerPrefs.GetInt("Difficulty" + i.ToString());
        }
    }

    private void Start()
    {
        currentHp = startingHp;
        currentMoney = startingMoney;
    }

    public void AddToHp(int addendum)
    {
        currentHp = Mathf.Clamp(currentHp + addendum, 0, startingHp);
        for (int i = 0; i < startingHp; i++)
        {
            if(i > currentHp-1)
            {
                cuori[i].SetActive(false);
            }
            else
            {
                cuori[i].SetActive(true);
            }
        }

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

    public void GameOver()
    {
        // update records
        if (highScores[difficulty] < currentWave)
        {
            highScores[difficulty] = currentWave;
            PlayerPrefs.SetInt("Difficulty" + difficulty.ToString(), highScores[difficulty]); // save high score
        }
        UIManager.Instance.ShowUI(UIManager.GameUI.Lose);
    }

    public void WinGame()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.Win);
    }

    public void DisplayMessage(string text, float duration)
    {
        messageUI.gameObject.SetActive(true);
        messageUI.SetMessage(text, duration);
    }

}
