using System.Collections;
using System.Collections.Generic;
using PinkDatatable;
using UnityEngine;

public class SwipeAndDrop : MonoBehaviour
{
    public Transform target;
    public Camera camera;

    public bool isDrag = false;
    private Vector3 _beginMousePos;
    private Vector3 _lastMousePos;
    private string _shapePath;
    private float _sensitivity = 0.1f;
    private float _dragDistance = 1f; //드래그로 판단할 최소 이동량
    private ShapePhysicsData physicsData;

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
            ClickToDrop();
        }
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
        if (_lastMousePos.x <= -4) _lastMousePos.x = -4;
        if (_lastMousePos.x >= 4) _lastMousePos.x = 4;

        RandomShapeDrop();
        if(_shapePath != null) Instantiate(Resources.Load<GameObject>(_shapePath), _lastMousePos, Quaternion.identity, target);
    }
    
    private void RandomShapeDrop()
    {
        int range = Random.Range(0, 3); // todo : 나중에 계속 도형 추가할 거라면 db 가져와서 개수 안에서 랜덤값 돌리는 걸로 수정 필요함 
        _shapePath = physicsData.GetShapePhysicsData(range).path;
    }
}
