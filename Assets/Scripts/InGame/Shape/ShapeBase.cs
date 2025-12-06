using System.Collections;
using System.Collections.Generic;
using PinkDatatable;
using UnityEngine;
using Util;

public class ShapeBase : MonoBehaviour
{
    public Color color;
    public ShapeType shapeType;
    protected Renderer rend;
    protected int curLevel;
    protected Rigidbody rb;
    protected Material mat; //시각용
    protected PhysicMaterial phyMat; //물리 충돌용
    protected Transform t;
    private bool becamePink;
    private float PinkCorrection;
    GameObject particle;
    
    protected ShapeLevelData shapeLevelData;
    protected ShapePhysicsData shapePhysicsData;
    protected ColorSpawnData colorSpawnData;

    protected void Awake()
    {
        shapeLevelData = DataManager.Instance.Level;
        shapePhysicsData = DataManager.Instance.Physics;
        colorSpawnData = DataManager.Instance.Color;
        curLevel = 1;
    }

    void Start()
    {
        phyMat = new PhysicMaterial();
        t = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        
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
        
        if (TypeAndLevelCheck(otherShape)) //otherShape가 같은 도형 + 같은 레벨이면
        {
            //색상 합치기
            color = PinkCalculate.ColorMerge(color, otherShape.color, out becamePink, PinkCorrection);
            GetComponent<Renderer>().material.color = color;
            if (becamePink)
            {
                Debug.Log("분홍입니다!");
                particle = Instantiate(Resources.Load<GameObject>("Particle/ExplodeParticle"), gameObject.transform.position, Quaternion.identity);
                ParticleSystem ps = particle.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    Destroy(particle, ps.main.duration + ps.main.startLifetime.constantMax);
                }
                StartCoroutine(ShapeDestroy(1));
                GameManager.Instance.AddScore(1);
            }
            
            //도형 합치기
            ShapeMerge(curLevel);
            Destroy(otherShape.gameObject);
        }
    }
    
    private bool TypeAndLevelCheck(ShapeBase otherShape)
    {
        return otherShape != null && otherShape.shapeType == shapeType && otherShape.curLevel == curLevel;
    }

    private IEnumerator ShapeDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    public virtual void Init() //각각의 물리 엔진
    {
        LevelCheck();
        ShapeSetting(curLevel);
    }

    public virtual void ShapeMerge(int level) //도형 합치기
    {
        level += 1;
        if (level >= 8) level = 8; //현재는 하드코딩이라 나중에 더 추가할거라면 수정 필요함
        
        ShapeSetting(level);
    }

    public void ShapeSetting(int level)
    {
        ShapeLevelData curLevelData = shapeLevelData.GetShapeLevelData(level);
        curLevel = curLevelData.level;
        float scale = curLevelData.scale;
        float mass = curLevelData.mass;
        float bounciness = curLevelData.bounciness;
        float friction = curLevelData.bounciness;
        PinkCorrection = curLevelData.correction;

        t.localScale = new Vector3(scale, scale, scale);
        rb.mass *= mass;
        phyMat.bounciness *= bounciness;
        phyMat.dynamicFriction *= friction;
    }

    protected void InitColor()
    {
        List<ColorSpawnData> curPhaseList = colorSpawnData.GetColorPhaseType(GameManager.Instance.ColorPhase);
        List<float> weightList = new();
        
        foreach (var a in curPhaseList)
            weightList.Add(a.weight);
        
        var index = RandomWeight.GetRandomIndex(weightList);
        ColorSpawnData pick = curPhaseList[index]; //확률(가중치)별로 뽑기
        
        float hueMin = pick.hueMin;
        float hueMax = pick.hueMax;
        float saturation = pick.saturation;
        float valueMin = pick.valueMin;
        float valueMax = pick.valueMax;
        
        float hue = GetRandomHue(hueMin, hueMax) / 360f;
        
        color = Color.HSVToRGB(hue, saturation / 100f, Random.Range(valueMin, valueMax));
    }

    private float GetRandomHue(float min, float max)
    {
        if (min <= max) return Random.Range(max, min);
        
        //min > max 일 때 (350~20 이런류)
        float range1 = 360f - min;
        float range2 = max;
        float totalRange = range1 + range2;
        float r = Random.Range(0f, totalRange);
        
        if (r < range1) return r + min;
        else return r - range1;
        
        return Random.Range(min, max);
    }

    private void LevelCheck()
    {
        switch (GameManager.Instance.ColorPhase)
        {
            case ColorPhase.Early:
                curLevel = 1;
                break;
            case ColorPhase.Mid:
                int i = Random.value < 0.8f ? 1 : 2; //80%로 lv1, 20%로 lv2
                curLevel = i;
                break;
            case ColorPhase.Late:
                float r = Random.value;
                int j = r < 0.6f ? 1 : r < 0.9f ? 2 : 3; //60% lv1, 30% lv2, 10% lv3
                curLevel = j;
                break;
            case ColorPhase.Survival:
                int f = Random.Range(0, 4);
                curLevel = f + 1;
                break;
            default:
                curLevel = 1;
                break;
        }
    }
}
