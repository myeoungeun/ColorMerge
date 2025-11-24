//using DataTable;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UGS;
using UnityEngine;
using UnityEngine.Networking;

public enum ColorPhase
{
    Early,
    Mid,
    Late,
    Survival
}

public class DataManager : Singleton<DataManager>
{
    public ColorSpawnData colorSpawnData;
    
    private bool _isInitialized = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Initialize()
    {
        if (_isInitialized) return;
        UnityGoogleSheet.LoadAllData();
        
        colorSpawnData = new ColorSpawnData();
        
        _isInitialized = true;
        
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(Load());
#else   
        //LoadSaveData();
#endif
    }
}
