using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinkDatatable
{
    public partial class ShapePhysicsData
    {
        public ShapePhysicsData GetShapePhysicsData(int idx)
        {
            if (ShapePhysicsDataMap.ContainsKey(idx))
            {
                return ShapePhysicsDataMap[idx];
            }

            Debug.LogWarning($"ColorSpawnData with ID {idx} not found!");
            return null;
        }
    }
}