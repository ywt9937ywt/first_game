using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    float outerSize = 0.5f;
    float innerSize = 0;
    float height = 0.1f;
    GameObject hexBase;
    private void Awake()
    {
    }

    public void Init(float inSize, float outSize, float h, bool FlatTopped = false)
    {
        AddHexBase();
    }
    
    private void AddHexBase()
    {
        hexBase = new GameObject("Hexagon Base");
        hexBase.transform.parent = this.transform;
        hexBase.transform.localPosition = new Vector3(0, 0, 0);

        Procedural_Hex procedural_Hex =  hexBase.AddComponent<Procedural_Hex>();
        procedural_Hex.setVal(innerSize, outerSize, height);

        MeshCollider meshc =  hexBase.AddComponent<MeshCollider>();
        meshc.sharedMesh = procedural_Hex.GetMesh();
    }

    public void SetHexBase(Color c)
    {
        GameObject newBase  = new GameObject("Hexagon Base");
        hexBase.transform.parent = this.transform;
        hexBase.transform.localPosition = new Vector3(0, 0, 0);

        Procedural_Hex procedural_Hex = hexBase.AddComponent<Procedural_Hex>();
        procedural_Hex.setVal(innerSize, outerSize, height);
        procedural_Hex.SetColor(c);

        MeshCollider meshc = hexBase.AddComponent<MeshCollider>();
        meshc.sharedMesh = procedural_Hex.GetMesh();
    }
}
