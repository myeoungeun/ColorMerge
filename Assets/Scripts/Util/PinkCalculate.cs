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
        return pinkValue >= 80f; //분홍이면 리턴
    }

    public static Color ColorMerge(Color color1, Color color2, out bool isPink) //색상 합치기
    {
        Color.RGBToHSV(color1, out float h1, out float s1, out float v1);
        Color.RGBToHSV(color2, out float h2, out float s2, out float v2);
        
        // 가중치 = 채도 × 밝기
        float w1 = s1 * v1;
        float w2 = s2 * v2;
        float wSum = Mathf.Max(1e-5f, w1 + w2); ; // 0으로 나누기 방지

        // Hue는 원형
        float hue = WeightedHue(h1, h2, w1, w2);
        
        float sat = (s1 * w1 + s2 * w2) / wSum;
        float val = (v1 * w1 + v2 * w2) / wSum;
        
        Color mergedColor = Color.HSVToRGB(hue, sat, val);
        isPink = PinkCheck(mergedColor);

        return mergedColor;
    }
    
    private static float WeightedHue(float h1, float h2, float w1, float w2)
    {
        float x = Mathf.Cos(h1 * 2 * Mathf.PI) * w1 + Mathf.Cos(h2 * 2 * Mathf.PI) * w2;
        float y = Mathf.Sin(h1 * 2 * Mathf.PI) * w1 + Mathf.Sin(h2 * 2 * Mathf.PI) * w2;

        float angle = Mathf.Atan2(y, x) / (2 * Mathf.PI);
        if (angle < 0) angle += 1f;
        return angle;
    }
}