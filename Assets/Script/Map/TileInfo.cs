using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileInfo 
{
    public Vector2Int pos;
    public HexInfo thisTile;
    //public Estate.Estates thisEstate;
    public EstateInfo estateinfo;

    public TileInfo(float inSize, float outSize, float h, Vector2Int p)
    {
        thisTile = new HexInfo(inSize, outSize , h);
        pos = p;
    }
   
    public void AddEstate(EstateInfo myEstate)
    {
        estateinfo = myEstate;
    }
}

[System.Serializable]
public class HexInfo
    {
        public float outerSize = 0.5f;
        public float innerSize = 0;
        public float height = 0.1f;
        public HexInfo() { }
        public HexInfo(float inSize, float outSize, float h)
        {
            innerSize = inSize;
            outerSize = outSize;
            height = h;
        }
    }