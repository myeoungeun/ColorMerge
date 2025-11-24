using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShapeLevelTable", menuName = "ScriptableObjects/ShapeLevelTable", order = 1)]
public class ShapeLevelTable : ScriptableObject
{
    public int level;
    public float scale;
    public float weight;
    public float spring; //탄성
    public float friction; //마찰
    public float correction; //보정 
    public string description;
}

[CreateAssetMenu(fileName = "ShapePhysicsData", menuName = "ScriptableObjects/ShapePhysicsData", order = 1)]
public class ShapePhysicsData : ScriptableObject
{
    public string shapeName;
    public float mass;
    public float drag;
    public float angularDrag;
    public float bounciness;
    public float friction;
    public string description;
}

public class ShapeBase : MonoBehaviour
{
    public Color color;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        
        if (rend == null) return;
        rend.material = new Material(rend.material); //기존 metarial 복사+인스턴스화
        rend.material.color = color;
    }

    public virtual void Init()
    {
    }
}
