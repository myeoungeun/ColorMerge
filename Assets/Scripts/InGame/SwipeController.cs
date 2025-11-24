using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public Transform target;

    public bool isDrag = false;
    private Vector3 _lastMousePos;
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Touch");
            isDrag = true;
        }
        
        if (isDrag && Input.GetMouseButton(0))
        {
            Debug.Log("Dragging");
            Vector3 delta = Input.mousePosition - _lastMousePos;
            target.Rotate(0f, delta.x * 0.1f, 0f, Space.World);
            _lastMousePos = Input.mousePosition;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            Debug.Log("Touch End");
        }
    }
}
