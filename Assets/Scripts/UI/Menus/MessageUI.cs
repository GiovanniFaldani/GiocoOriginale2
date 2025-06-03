using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI message;
    private float timer = 0f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
            gameObject.SetActive(false);
    }

    public void SetMessage(string _text, float _timer)
    {
        message.text = _text;
        timer = _timer;
    }
}
