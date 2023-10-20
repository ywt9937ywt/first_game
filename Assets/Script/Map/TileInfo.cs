using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileInfo 
{
    public Vector2Int pos;
    public HexInfo thisTile;
    //public EstateInfo estateinfo;
    public Estate.Estates estateType;

    public TileInfo(float inSize, float outSize, float h, Vector2Int p, TileType tt = TileType.Edit)
    {
        thisTile = new HexInfo(inSize, outSize , h, tt);

        pos = p;
    }

    public void RemoveEstate()
    {
        estateType = Estate.Estates.None;
    }

    public void ChangeEstate(int myEstate)
    {
        estateType = (Estate.Estates)myEstate;
    }

    public void SetHexType(TileType tt)
    {
        thisTile.SetHexType(tt);
    }

    public TileType GetHexType()
    {
        return thisTile.tiletype;
    }
}

[System.Serializable]
public class HexInfo
{
    public float outerSize = 0.5f;
    public float innerSize = 0;
    public float height = 0.1f;
    public TileType tiletype;
    public HexInfo() { }
    public HexInfo(float inSize, float outSize, float h, TileType tt = TileType.Edit)
    {
        innerSize = inSize;
        outerSize = outSize;
        height = h;
        tiletype = tt;
    }

    public void SetHexType(TileType tt)
    {
        tiletype = tt;
    }
}