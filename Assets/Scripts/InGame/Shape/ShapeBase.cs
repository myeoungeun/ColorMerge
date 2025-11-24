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
    public Color shapeColor;
    public ShapePhysicsData shapePhysicsData = new();

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        
        if (rend == null) return;
        rend.material = new Material(rend.material);
        rend.material.color = shapeColor;

        Init();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ShapeBase otherShape = collision.gameObject.GetComponent<ShapeBase>();
        if (otherShape != null) //todo : && otherShape가 같은 레벨이라면
        {
            shapeColor = PinkCalculate.ShapeColorMerge(shapeColor, otherShape.shapeColor);
            Debug.Log("합쳐짐");
            Merge();
            Destroy(collision.gameObject);
        }
    }

    public virtual void Init()
    {
        //기본 색상 등등
    }

    public virtual void Merge()
    {
        //크기 업그레이드
    }
}
