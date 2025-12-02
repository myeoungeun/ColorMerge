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

public enum ShapeType
{
    Cube,
    Sphere,
    Tetrahedron
}

public class DataManager : Singleton<DataManager>
{
    public ColorSpawnData Color;
    public ShapePhysicsData Physics;
    public ShapeLevelData Level;
    
    private bool _isInitialized = false;

    public void Initialize()
    {
        if (_isInitialized) return;
        UnityGoogleSheet.LoadAllData();
        
        Color = new ColorSpawnData();
        Physics = new ShapePhysicsData();
        Level = new ShapeLevelData();
        
        _isInitialized = true;
    }
}
