using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    float outerSize = 0.5f;
    float innerSize = 0;
    float height = 0.1f;
    public Vector2Int pos;
    GameObject hexBase;
    GameObject estateObj;
    private void Awake()
    {
    }

    public void Init(float inSize, float outSize, float h, Vector2Int p, bool FlatTopped = false)
    {
        pos = p;
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

    public void AddObj(GameObject objToGen, Vector3 basePos)
    {
        if(estateObj != null)
        {
            DestroyImmediate(estateObj);
        }
        estateObj = GameObject.Instantiate(objToGen, basePos, Quaternion.identity);
        estateObj.transform.parent = this.transform;
    }

    public void SetHexBase(Color c)
    {
        Procedural_Hex procedural_Hex = hexBase.GetComponent<Procedural_Hex>();
        procedural_Hex.SetColor(c);
    }
}
