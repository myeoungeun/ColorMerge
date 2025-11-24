using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public Transform target;

    public bool isDrag = false;
    private Vector3 _startMousePos;
    private Vector3 _lastMousePos;
    private Vector3 _swipeDir;
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Touch");
            isDrag = true;
            _startMousePos = Input.mousePosition;
        }
        
        if (isDrag && Input.GetMouseButton(0))
        {
            _swipeDir = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) - _startMousePos;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            _lastMousePos = Input.mousePosition;
        }
    }
}
