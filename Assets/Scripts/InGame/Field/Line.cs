using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private List<Collider> _colliders = new();
    private float _time = 0;

    public void OnTriggerEnter(Collider other)
    {
        _colliders.Add(other);
    }

    public void OnTriggerStay(Collider other)
    {
        if (_colliders.Count >= 1)
        {
            _time += Time.deltaTime;
            
            if (CompareTag("gameOverLine") && _time >= 5f)
            {
                Debug.Log("게임 오버!");
            }
            else if (CompareTag("warningLine") && _time >= 5f)
            {
                Debug.Log("경고");
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        _colliders.Remove(other);
        if (_colliders.Count == 0) _time = 0;
    }
}
