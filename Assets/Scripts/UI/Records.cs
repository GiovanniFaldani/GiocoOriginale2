using TMPro;
using UnityEngine;

public class Records : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI easyRecord;
    [SerializeField] private TextMeshProUGUI normalRecord;
    [SerializeField] private TextMeshProUGUI hardRecord;

    private void OnEnable()
    {
        easyRecord.text = "Facile: " + GameManager.Instance.highScores[0].ToString();
        normalRecord.text = "Normale: " + GameManager.Instance.highScores[1].ToString();
        hardRecord.text = "Difficile: " + GameManager.Instance.highScores[2].ToString();
    }
}
