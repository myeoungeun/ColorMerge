using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public Transform target;
    public Camera camera;

    public bool isDrag = false;
    private Vector3 _lastMousePos;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isDrag = true;
        }
        
        if (isDrag && Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - _lastMousePos;
            target.Rotate(0f, delta.x * 0.1f, 0f, Space.World);
            _lastMousePos = Input.mousePosition;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            _lastMousePos = Input.mousePosition;
            _lastMousePos.z = Mathf.Abs(camera.transform.position.z - target.position.z); //카메라-타겟 거리
            _lastMousePos = camera.ScreenToWorldPoint(_lastMousePos);
            if(_lastMousePos.y > 10) ShapeDrop(); //도형 떨어뜨리기 -> 위에서만 동작
        }
    }

    private void ShapeDrop()
    {
        //좌우 거리 제한
        if (_lastMousePos.x <= -4) _lastMousePos.x = -4;
        else if (_lastMousePos.x >= 4) _lastMousePos.x = 4;
        
        Debug.Log(_lastMousePos);
    }
}
