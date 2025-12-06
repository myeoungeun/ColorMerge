using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeShape : MonoBehaviour
{
    public float duration = 0.2f;
    public float strength = 0.05f;

    private Vector3 originalPos;

    public void Shake()
    {
        StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        originalPos = transform.localPosition;
        float time = 0f;

        while (time < duration)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * strength;
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}

