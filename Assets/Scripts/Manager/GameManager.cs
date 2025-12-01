using System.Collections;
using System.Collections.Generic;
using PinkDatatable;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private float _time = 0;
    private ColorPhase _colorPhase;
    public ColorPhase ColorPhase => _colorPhase;
    
    private void Awake()
    {
        DataManager.Instance.Initialize();
    }
    
    //시간 체크 + 기본 색상 나누기 + 경고 등등
    private void Update()
    {
        _time += Time.deltaTime;
        LevelCurve();
    }

    private void LevelCurve()
    {
        if (_time < 30)
        {
            _colorPhase = ColorPhase.Early;
        }
        else if (_time >= 30 && _time < 60)
        {
            _colorPhase = ColorPhase.Mid;
        }
        else if (_time >= 60 && _time < 120)
        {
            _colorPhase = ColorPhase.Late;
        }
        else if (_time >= 120)
        {
            _colorPhase = ColorPhase.Survival;
        }
    }
}
