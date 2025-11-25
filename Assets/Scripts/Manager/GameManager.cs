using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    void Start()
    {
        DataManager.Instance.Initialize();
        var data = DataManager.Instance.Color.GetColorSpawnData(0);
        Debug.Log(data.colorType);
    }
}
