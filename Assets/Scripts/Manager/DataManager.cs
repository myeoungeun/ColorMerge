//using DataTable;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GoogleSheet.Core.Type;
using PinkDatatable;
using UGS;
using UnityEngine;
using UnityEngine.Networking;

[UGS(typeof(ColorPhase))]
public enum ColorPhase
{
    Early,
    Mid,
    Late,
    Survival
}

public class DataManager : Singleton<DataManager>
{
    public ColorSpawnData Color;
    
    private bool _isInitialized = false;

    public void Initialize()
    {
        if (_isInitialized) return;
        UnityGoogleSheet.LoadAllData();
        
        Color = new ColorSpawnData();
        
        _isInitialized = true;
    }
}
