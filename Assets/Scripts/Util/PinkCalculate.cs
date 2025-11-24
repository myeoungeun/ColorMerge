using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PinkCalculate
{
    public static bool PinkCheck(Color color)
    {
        Color.RGBToHSV(color, out float h, out float s, out float v); // v = lightness

        float hueDegree = h * 360f;
        float hueDiff = Mathf.Min(Mathf.Abs(hueDegree - 0f), 360f - Mathf.Abs(hueDegree - 0f));
        float hueScore = 100f - (hueDiff / 180f * 100f);
        float satScore = s * 100f;
        float valScore = 100f - (Mathf.Abs(v - 0.55f) * 200f);

        float pinkValue = (hueScore * 0.6f) + (satScore * 0.25f) + (valScore * 0.15f);

        return pinkValue >= 70f; //분홍이면 리턴
    }

    public static Color ShapeColorMerge(Color color1, Color color2)
    {
        Color.RGBToHSV(color1, out float h1, out float s1, out float v1);
        Color.RGBToHSV(color2, out float h2, out float s2, out float v2);
        
        // 가중치 = 채도 × 밝기
        float w1 = s1 * v1;
        float w2 = s2 * v2;

        float wSum = w1 + w2;
        if (wSum == 0) wSum = 1; // 0으로 나누기 방지

        float hue = (h1 * w1 + h2 * w2) / wSum;
        float sat = (s1 * w1 + s2 * w2) / wSum;
        float val = (v1 * w1 + v2 * w2) / wSum;
        
        Color mergedColor = Color.HSVToRGB(hue, sat, val);

        bool isPink = PinkCheck(mergedColor);
        if (isPink)
        {
            Debug.Log("분홍입니다!");
            //도형 = 분홍일 때 해야되는 처리
        }

        return mergedColor;
    }
}