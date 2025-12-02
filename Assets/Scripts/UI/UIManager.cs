using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    private void Start()
    {
        scoreText.text = "0";
        timeText.text = "00.00";
    }

    private void Update()
    {
        timeText.text = GameManager.Instance.time.ToString("00.00");
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
