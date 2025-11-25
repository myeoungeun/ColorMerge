using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinkDatatable
{
    public partial class ShapeLevelData
    {
        public ShapeLevelData GetShapeLevelData(int idx)
        {
            if (ShapeLevelDataMap.ContainsKey(idx))
            {
                return ShapeLevelDataMap[idx];
            }

            Debug.LogWarning($"ColorSpawnData with ID {idx} not found!");
            return null;
        }
    }
}