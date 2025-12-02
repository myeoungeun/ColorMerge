using System.Collections;
using System.Collections.Generic;
using PinkDatatable;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float time = 0;
    private int _curScore = 0;
    private ColorPhase _colorPhase;
    public ColorPhase ColorPhase => _colorPhase;
    
    private void Awake()
    {
        DataManager.Instance.Initialize();
    }
    
    private void Update()
    {
        time += Time.deltaTime;
        LevelCurve();
    }

    private void LevelCurve()
    {
        if (time < 30)
        {
            _colorPhase = ColorPhase.Early;
        }
        else if (time >= 30 && time < 60)
        {
            _colorPhase = ColorPhase.Mid;
        }
        else if (time >= 60 && time < 120)
        {
            _colorPhase = ColorPhase.Late;
        }
        else if (time >= 120)
        {
            _colorPhase = ColorPhase.Survival;
        }
    }

    public void AddScore(int score)
    {
        _curScore += score;
        UIManager.Instance.UpdateScore(_curScore);
    }

    public void GameOver()
    {
        Debug.Log("게임 오버!");
        Time.timeScale = 0;
        // 에디터에서는 플레이 모드 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 게임에서는 실제 종료
        Application.Quit();
#endif
    }
}
