using System.Collections;
using System.Collections.Generic;
using PinkDatatable;
using UnityEngine;
using UnityEngine.AI;

public class SwipeAndDrop : MonoBehaviour
{
    public Transform target;
    public Transform previewParent;
    public Camera camera;

    public bool isDrag = false;
    private Vector3 _beginMousePos;
    private Vector3 _lastMousePos;
    private string _shapePath;
    private float _sensitivity = 0.1f;
    private float _dragDistance = 5f; //드래그로 판단할 최소 이동량
    private ShapePhysicsData physicsData;
    private Queue<GameObject> _shapeIndex = new();
    private int _MaxCount = 5;
    private bool _isDrop = false;
    
    void Start()
    {
        physicsData = DataManager.Instance.Physics;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _beginMousePos = Input.mousePosition;
            _lastMousePos = _beginMousePos;
            isDrag = false; //처음엔 드래그 아님
        }
        
        if (Input.GetMouseButton(0))
        {
            if (!isDrag)
            {
                if (Vector3.Distance(Input.mousePosition, _beginMousePos) >= _dragDistance) //마우스 움직이는 거리 체크
                    isDrag = true;
            }
            if (isDrag)
            {
                Vector3 delta = Input.mousePosition - _lastMousePos;
                target.Rotate(0f, delta.x * _sensitivity, 0f, Space.World); //회전
                _lastMousePos = Input.mousePosition;
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            if (!_isDrop)
            {
                _isDrop = true;
                StartCoroutine(DelayDrop());
            }
        }
    }
    
    private IEnumerator DelayDrop()
    {
        ClickToDrop();
        yield return new WaitForSeconds(0.5f);
        _isDrop = false;
    }

    private void ClickToDrop()
    {
        _lastMousePos = Input.mousePosition;
        _lastMousePos.z = Mathf.Abs(camera.transform.position.z - target.position.z); //카메라-타겟 거리
        _lastMousePos = camera.ScreenToWorldPoint(_lastMousePos);
        if(_lastMousePos.y > 10) ShapeDrop(); //도형 떨어뜨리기 -> 위에서만 동작
    }
    
    private void ShapeDrop()
    {
        //높이 + 좌우 거리 제한
        _lastMousePos.y = 18;
        _lastMousePos.x = Mathf.Clamp(_lastMousePos.x, -4f, 4f);

        if(_shapeIndex.Count < _MaxCount) CreateShape(); //미리보기 없으면 생성
        
        _shapeIndex.Peek().SetActive(true);

        if (_shapeIndex.Count > 0) //첫 번째 도형 꺼내서 드랍
        {
            GameObject obj = _shapeIndex.Dequeue();
            if (obj != null)
            {
                obj.tag = "Shape";
                obj.GetComponent<Rigidbody>().useGravity = true;
                obj.GetComponent<Collider>().isTrigger = false;
                obj.transform.parent = target;
                obj.transform.position = _lastMousePos;
            }
            _shapeIndex.Peek().SetActive(true);
        }
    }
    
    private void CreateShape()
    {
        Vector3 pos = new Vector3(27, 1, 0);
        int toCreate = _MaxCount - _shapeIndex.Count;

        for (int i = 0; i < toCreate; i++)
        {
            int range = Random.Range(0, 3); //나중에 계속 도형 추가할 거라면 db 가져와서 개수 안에서 랜덤값 돌리는 걸로 수정 필요함. 랜덤 도형 선택 
            _shapePath = physicsData.GetShapePhysicsData(range).path;

            if (_shapePath != null)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>(_shapePath), pos, Quaternion.identity, previewParent);
                obj.tag = "Preview";
                obj.GetComponent<Rigidbody>().useGravity = false;
                obj.GetComponent<Collider>().isTrigger = true;
                obj.SetActive(false);
                _shapeIndex.Enqueue(obj);
            }
        }
    }
}
