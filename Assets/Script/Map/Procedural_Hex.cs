using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public struct Face
{
    public List<Vector3> vertices { get; private set; }
    public List<int> triangles { get; private set; }
    public List<Vector2> uvs { get; private set; }

    public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
    {
        this.vertices = vertices;
        this.triangles = triangles;
        this.uvs = uvs;
    }
}

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Procedural_Hex : MonoBehaviour
{
    private Mesh m_mesh;
    private MeshFilter m_meshFilter;
    private MeshRenderer m_meshRenderer;
    private List<Face> m_faces;

    Material material;
    public Color colorMesh;
    float innerSize = 0;
    float outerSize = 0.5f;
    float height = 0.1f;
    bool isFlatTopped;
    //[InspectorButton(nameof(SaveMesh))]
    //public bool buttonField;

    //public string meshName = "standard";

    private void Awake()
    {
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();

        m_mesh = new Mesh();
        m_mesh.name = "Hex";
        material = new Material(Shader.Find("Diffuse"));
        m_meshFilter.mesh = m_mesh;
        m_meshRenderer.sharedMaterial = material;
        //m_meshRenderer.sharedMaterial.color = colorMesh;
        //DrawMesh();
    }

    public Mesh GetMesh()
    {
        return m_mesh;
    }

    public void SetColor(Color c)
    {
        material.color = c;
    }
    public void setVal(float inSize, float outSize, float h, bool FlatTopped = false)
    {
        innerSize = inSize;
        outerSize = outSize;
        height = h;
        isFlatTopped = FlatTopped;
        DrawMesh();
    }
    public void DrawMesh()
    {
        DrawFaces();
        CombineFaces();
    }

    private void DrawFaces()
    {
        //top faces
        m_faces = new List<Face>();
        for(int point = 0; point < 6; point++)
        {
            m_faces.Add(CreateFace(innerSize, outerSize, height / 2f, height / 2f, point));
        }
        //bottom faces
        for (int point = 0; point < 6; point++)
        {
            m_faces.Add(CreateFace(innerSize, outerSize, -height / 2f, -height / 2f, point, true));
        }
        //outer faces
        for(int point = 0; point < 6; point++)
        {
            m_faces.Add(CreateFace(outerSize, outerSize, height / 2f, -height / 2f, point, true));
        }
        //Inner faces
        for (int point = 0; point < 6; point++)
        {
            m_faces.Add(CreateFace(innerSize, innerSize, height / 2f, -height / 2f, point));
        }

    }

    private void CombineFaces()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for(int i = 0; i< m_faces.Count; i++)
        {
            vertices.AddRange(m_faces[i].vertices);
            uvs.AddRange(m_faces[i].uvs);

            int offset = 4 * i;
            foreach(int triangle in m_faces[i].triangles)
            {
                tris.Add(triangle + offset);
            }
        }

        m_mesh.vertices = vertices.ToArray();
        m_mesh.triangles = tris.ToArray();
        m_mesh.uv = uvs.ToArray();
        m_mesh.RecalculateNormals();
    }

    private Face CreateFace(float innerRad, float outerRad, float heightA, float heightB, int point, bool reverse = false)
    {
        Vector3 pointA = GetPoint(innerRad, heightB, point);
        Vector3 pointB = GetPoint(innerRad, heightB, (point < 5) ? point + 1 : 0);
        Vector3 pointC = GetPoint(outerRad, heightA, (point < 5) ? point + 1 : 0);
        Vector3 pointD = GetPoint(outerRad, heightA, point);

        List<Vector3> vertices = new List<Vector3>() { pointA, pointB, pointC, pointD };
        List<int> triangles = new List<int>() { 0, 1, 2, 2, 3, 0 };
        List<Vector2> uvs = new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        if (reverse)
        {
            vertices.Reverse();
        }
        return new Face(vertices, triangles, uvs);
    }

    protected Vector3 GetPoint(float size, float height, int index)
    {
        float angle_deg = isFlatTopped ? (60 * index) : (60* index -30);
        float angle_rad = Mathf.PI / 180f * angle_deg;
        return new Vector3((size * Mathf.Cos(angle_rad)), height, size * Mathf.Sin(angle_rad));
    }

    public void SaveMesh()
    {
        //AssetDatabase.CreateAsset(this.gameObject, "Assets/Models/Prefeb/Hexs/" + meshName + ".asset");
        //string pathname = "Assets/Models/Prefeb/Hexs/" + meshName + ".prefab";
        //PrefabUtility.SaveAsPrefabAsset(this.gameObject, AssetDatabase.GenerateUniqueAssetPath(pathname));
    }
}
