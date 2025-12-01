using System.Collections;
using System.Collections.Generic;
using PinkDatatable;
using UnityEngine;

public class ShapeBase : MonoBehaviour
{
    public Color color;
    protected int curLevel;
    protected ShapeLevelData shapeLevelData;
    protected ShapePhysicsData shapePhysicsData;

    void Awake()
    {
        shapeLevelData = DataManager.Instance.Level;
        shapePhysicsData = DataManager.Instance.Physics;
        curLevel = 1;
    }

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        
        if (rend == null) return;
        rend.material = new Material(rend.material);
        rend.material.color = color;

        Init();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ShapeBase otherShape = collision.gameObject.GetComponent<ShapeBase>();
        if (otherShape != null) //todo : && otherShape가 같은 레벨 + 같은 도형이라면
        {
            //색상 합치기
            color = PinkCalculate.ColorMerge(color, otherShape.color);
            GetComponent<Renderer>().material.color = color;
            
            //도형 합치기
            ShapeMerge(curLevel);
            //Destroy(otherShape.gameObject);
        }
    }

    public virtual void Init()
    {
        //기본 색상
        //각각의 도형 물리 특징
    }

    public virtual void ShapeMerge(int level) //도형 합치기
    {
        level += 1;
        curLevel = shapeLevelData.GetShapeLevelData(level).level;
        //크기 업그레이드 + 실제 도형에 적용
    }
}
