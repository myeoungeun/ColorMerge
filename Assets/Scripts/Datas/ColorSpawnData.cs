using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinkDatatable
{
    public partial class ColorSpawnData
    {
        public ColorSpawnData GetColorSpawnData(int idx)
        {
            if (ColorSpawnDataMap.ContainsKey(idx))
            {
                return ColorSpawnDataMap[idx];
            }
            Debug.LogWarning($"ColorSpawnData with ID {idx} not found!");
            return null;
        }
        
        public List<ColorSpawnData> GetColorPhaseType(ColorPhase phase)
        {
            List<ColorSpawnData> result = new List<ColorSpawnData>();

            foreach (var a in ColorSpawnDataMap)
            {
                if (a.Value.ColorPhase == phase)
                {
                    result.Add(a.Value);
                }
            }

            return result;
        }
    }
}