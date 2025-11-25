using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PinkDatatable;

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
    }
}