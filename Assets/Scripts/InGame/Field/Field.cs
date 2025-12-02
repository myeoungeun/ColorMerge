// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class Field : MonoBehaviour
// {
//     public Vector3 localSize;
//     
//     
//     private void Start()
//     {
//         MeshFilter mf = GetComponent<MeshFilter>();
//         if (mf != null)
//         {
//             localSize = mf.sharedMesh.bounds.size;
//
//             Debug.Log("LocalSize = " + localSize);
//         }
//     }
//     
//     public float GetWarningY()
//     {
//         Debug.Log("WarningY = " + localSize.y * 0.7f);
//         return localSize.y * 0.7f;
//     }
//     
//     public float GetGameOverY()
//     {
//         Debug.Log("GameOverY = " + localSize.y * 0.9f);
//         return localSize.y * 0.9f;
//     }
// }
