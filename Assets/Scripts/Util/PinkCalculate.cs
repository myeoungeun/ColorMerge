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

    public static Color ShapeMerge(Color color1, Color color2)
    {
        Color.RGBToHSV(color1, out float h1, out float s1, out float v1);
        Color.RGBToHSV(color2, out float h2, out float s2, out float v2);
        
        float hue = (h1 + h2) * 0.5f;
        float sat = (s1 + s2) * 0.5f;
        float val = (v1 + v2) * 0.5f;
        
        Color mergedColor = Color.HSVToRGB(hue, sat, val);
        
        return mergedColor;
    }
}