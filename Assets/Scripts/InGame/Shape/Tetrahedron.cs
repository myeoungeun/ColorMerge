using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class Tetrahedron : ShapeBase //todo : 얘 버그 있어서 사용 안하고있긴한데 고쳐야됩니다
{
    public float size = 1.0f;
    public Vector3 offset = new Vector3(0, 0, 0);
    private Rigidbody rb;
    private MeshCollider mc;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    void OnValidate()
    {
        if (mesh == null) return;

        if (size > 0 || offset.magnitude > 0)
        {
            setMeshData(size);
            createProceduralMesh();
        }
    }

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        rb = GetComponent<Rigidbody>();
        mc = gameObject.GetComponent<MeshCollider>();
        
        mc.convex = true;
        rb.isKinematic = false;
        rb.useGravity = true;

        setMeshData(size);
        createProceduralMesh();
    }

    void setMeshData(float size)
    {
        float g = Mathf.Sqrt(3.0f) / 6.0f * size; // 정삼각형의 중점
        float h = Mathf.Sqrt(6.0f) / 3.0f * size; // 정사면체의 높이
        float c = Mathf.Sqrt(6.0f) / 12.0f * size; // 정사면체의 중점

        Vector3 d0 = new Vector3(0, h - c, 0) + offset; 
        Vector3 d1 = new Vector3(-0.5f * size, -c, -g) + offset;
        Vector3 d2 = new Vector3(0, -c, Mathf.Sqrt(3.0f) / 2.0f * size - g) + offset;
        Vector3 d3 = new Vector3(0.5f * size, -c, -g) + offset;

        vertices = new Vector3[] { d0, d1, d2, d0, d2, d3, d0, d3, d1, d1, d3, d2 };

        triangles = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
    }

    void createProceduralMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        Destroy(this.GetComponent<MeshCollider>());
        this.gameObject.AddComponent<MeshCollider>();
    }
}