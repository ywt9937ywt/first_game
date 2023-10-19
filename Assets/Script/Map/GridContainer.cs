using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class GridContainer //: MonoBehaviour
{
    //public static GridContainer GCinstance { get; private set; }
    public Dictionary<Vector2Int, TileInfo> myMap = new Dictionary<Vector2Int, TileInfo>();

    /*void Awake()
    {
        if (GCinstance == null)
        {
            GCinstance = this;
        }
        else
        {
            Destroy(this);
        }
    }*/
    public GridContainer()
    {

    }

    public void Add(Vector2Int pos, TileInfo info)
    {
        myMap[pos] =  info;
    }

    public void TryEditTile(Vector2Int pos, out TileInfo info)
    {
        myMap.TryGetValue(pos, out TileInfo tryInfo);
        if(tryInfo == null)
        {
            Debug.Log("fail to edit obj");
        }
        info = tryInfo;
    }

    public bool TryDeleteTile(Vector2Int pos)
    {
        if(myMap[pos] != null)
        {
            myMap.Remove(pos);
            return true;
        }
        return false;
    }

    public bool TryDeleteObjInfo(Vector2Int pos)
    {
        if (myMap.ContainsKey(pos))
        {
            myMap[pos].RemoveEstate();
            return true;
        }
        return false;
    }

    public void Clear()
    {
        myMap.Clear();
    }

    public int TileCount()
    {
        return myMap.Count;
    }

    public void Save(string saveName)
    {
        List<TileInfo> saveData = new List<TileInfo>();
        foreach (KeyValuePair<Vector2Int, TileInfo> pairs in myMap)
        {
            saveData.Add(pairs.Value);
        }
        //Debug.Log(Application.persistentDataPath);
        SaveHandler.SaveToJSON<TileInfo>(saveData, saveName);
    }

    public List<TileInfo> Load(string saveName)
    {
        List<TileInfo> loadMap = SaveHandler.ReadFromJSON<TileInfo>(saveName);
        foreach (TileInfo info in loadMap)
        {
            myMap[info.pos] = info;

        }
        return loadMap;
    }
}
