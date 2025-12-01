using System.Collections;
using System.Collections.Generic;
using PinkDatatable;
using UnityEngine;

public class ShapeBase : MonoBehaviour
{
    public Color color;
    protected int curLevel;
    protected Rigidbody rb;
    protected Material mat; //시각용
    protected PhysicMaterial phyMat; //물리 충돌용
    
    protected ShapeLevelData shapeLevelData;
    protected ShapePhysicsData shapePhysicsData;
    protected ColorSpawnData colorSpawnData;

    void Awake()
    {
        shapeLevelData = DataManager.Instance.Level;
        shapePhysicsData = DataManager.Instance.Physics;
        colorSpawnData = DataManager.Instance.Color;
        curLevel = 1;
    }

    void Start()
    {
        Debug.Log("start");
        phyMat = new PhysicMaterial();
        rb = GetComponent<Rigidbody>();
        Renderer rend = GetComponent<Renderer>();
        
        if (rend == null) return;
        mat = new Material(rend.material); //기존 메터리얼을 기준으로 새로운 인스턴스 생성
        rend.material = mat;

        InitColor();
        rend.material.color = color; //초기 색상 지정

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
        colorSpawnData.GetColorSpawnData(1);
        //기본 색상
        //각각의 도형 물리 특징
    }

    public virtual void ShapeMerge(int level) //도형 합치기
    {
        level += 1;
        curLevel = shapeLevelData.GetShapeLevelData(level).level;
        //크기 업그레이드 + 실제 도형에 적용
    }

    private void InitColor()
    {
        Debug.Log("color init");
        List<ColorSpawnData> curPhaseList = colorSpawnData.GetColorPhaseType(GameManager.Instance.ColorPhase);
        int rand = Random.Range(0, curPhaseList.Count); //todo : 가중치별로 변경되도록 수정해야 됨
        
        ColorSpawnData pick = curPhaseList[rand];

        float hueMin = pick.hueMin;
        float hueMax = pick.hueMax;
        float saturation = pick.saturation;
        float valueMin = pick.valueMin;
        float valueMax = pick.valueMax;
        float weight = pick.weight; //가중치(확률)
        
        color = Color.HSVToRGB(Random.Range(hueMin, hueMax) / 360f, saturation / 100f, Random.Range(valueMin, valueMax));
    }
}
