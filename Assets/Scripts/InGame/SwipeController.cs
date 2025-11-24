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
            OnMouseDown();
        }
    }

    private void OnMouseDown()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(camera.transform.position.z - target.position.z);
        mousePosition = camera.ScreenToWorldPoint(mousePosition);
        
        if (mousePosition.x <= -4) mousePosition.x = -4;
        else if (mousePosition.x >= 4) mousePosition.x = 4;
        
        Debug.Log(mousePosition);
    }
}
