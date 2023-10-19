using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalSetting;

[ExecuteInEditMode]
public class HexTile : MonoBehaviour
{
    float outerSize = 0.5f;
    float innerSize = 0;
    float height = 0.1f;
    public Vector2Int pos;
    GameObject hexBase;
    GameObject estateObj;
    private GameObject mapRoot;

    public void Init(float inSize, float outSize, float h, Vector2Int p, GameObject root, TileType tt,  bool FlatTopped = false)
    {
        pos = p;
        AddHexBase(tt, p);
        mapRoot = root;
    }
    
    private void AddHexBase(TileType tt, Vector2Int p)
    {
        SetHexBase((int)tt, p);
    }

    private void GenDefaultHexBase()
    {
        hexBase = new GameObject("Hexagon Base");
        hexBase.transform.parent = this.transform;
        hexBase.transform.localPosition = new Vector3(0, 0, 0);

        Procedural_Hex procedural_Hex =  hexBase.AddComponent<Procedural_Hex>();
        procedural_Hex.setVal(innerSize, outerSize, height);

        MeshCollider meshc =  this.transform.gameObject.AddComponent<MeshCollider>();
        meshc.sharedMesh = procedural_Hex.GetMesh();
    }

    public void AddObj(int objToGen, Vector2Int basePos)
    {
        if(estateObj != null)
        {
            DestroyImmediate(estateObj);
        }
        Vector3 worldpos = HexGrid.get_world_pos(new Vector3(0.0f, 0.0f, 0.0f), new Vector2(pos.x, pos.y), outerSize);
        GameObject genObj = GlobalScripableObj.Instance.EstateSetting.GetEstate(objToGen);
        if (genObj == null)
        {
            Debug.Log("obj to gen is null");
            return;
        }
        estateObj = GameObject.Instantiate(genObj, worldpos, Quaternion.identity);
        estateObj.transform.parent = this.transform;

    }

    private void RemoveObj()
    {
        if(estateObj != null) DestroyImmediate(estateObj);
    }


    public void SetHexBase(int myHexid, Vector2Int pos)
    {
        if (hexBase != null)
        {
            DestroyImmediate(hexBase);
        }
        Vector3 worldpos = HexGrid.get_world_pos(new Vector3(0.0f, 0.0f, 0.0f), new Vector2(pos.x, pos.y), outerSize);
        GameObject genHex = GlobalScripableObj.Instance.hexSetting.GetTile((TileType)myHexid);
        if (genHex == null)
        {
            Debug.Log("hex to gen is null");
            return;
        }
        hexBase = GameObject.Instantiate(genHex, worldpos, Quaternion.identity);
        hexBase.transform.parent = this.transform;

        MeshCollider meshc = this.transform.gameObject.AddComponent<MeshCollider>();
        meshc.sharedMesh = genHex.GetComponent<MeshFilter>().sharedMesh;
    }

    public void RemoveHexBase()
    {
        DestroyImmediate(hexBase);

    }

    public void InvokeRootChangeObj(int myestateid)
    {
        if (hexBase == null)
        {
            Debug.Log("Please add base first");
            return;
        }
        RawMap rootRawMap = mapRoot.GetComponent<RawMap>();
        rootRawMap.InvokeChangeObj(myestateid, pos);
        if(myestateid == -1)
        {
            RemoveObj();
        }
        else
        {
            AddObj(myestateid, pos);
        }
    }

    public void InvokeRootChangeHex(int myHexid)
    {
        RawMap rootRawMap = mapRoot.GetComponent<RawMap>();
        rootRawMap.InvokeChangeHex(myHexid, pos);
        SetHexBase(myHexid, pos);
    }
}
