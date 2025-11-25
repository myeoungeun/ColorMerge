using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeBase : MonoBehaviour
{
    public Color shapeColor;
    //public ShapePhysicsData shapePhysicsData = new();

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
