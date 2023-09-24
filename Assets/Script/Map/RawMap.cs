using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RawMap : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(2, 2);
    [Header("HexDetail")]
    public GameObject hex;
    public float outerSize = 0.5f;

    public Estate myEstate;
    [Header("MapDetail")]
    [InspectorButton(nameof(MapEditGrid))]
    public bool editButton;
    public Hex_Gen_Setting settings;
    public string saveName;
    [InspectorButton(nameof(OnSave))]
    public bool saveButton;
    [InspectorButton(nameof(OnLoad))]
    public bool loadButton;

    private GameObject rawMapRoot;
    Dictionary<Vector2Int, TileInfo> myMap = new Dictionary<Vector2Int, TileInfo>();

    private GameObject HexBase;

    private void Awake()
    {
        if (!(rawMapRoot = GameObject.Find("RawMapRoot")))
        {
            rawMapRoot = new GameObject("RawMapRoot");
        }
        
    }
    public void ClearMap(GameObject r)
    {
        if(r == null)
        {
            Debug.Log("map root not found");
            return;
        }
        List<GameObject> children = new List<GameObject>();

        for (int i = 0; i < r.transform.childCount; i++)
        {
            GameObject child = r.transform.GetChild(i).gameObject;
            children.Add(child);
        }

        foreach (GameObject child in children)
        {
            DestroyImmediate(child, true);
        }
        myMap.Clear();
    }
    public void MapEditGrid()
    {
        ClearMap(rawMapRoot);
        //GameObject xTile = GameObject.Instantiate(settings.GetTile(Hex_Gen_Setting.TileType.Edit));
        for (int y = 0; y < GridSize.y; y++)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                AddHex(new Vector2Int(x, y), 0, outerSize, 0.1f);
            }
        }

    }

    public void AddHex(Vector2Int pos, float innersize, float outersize, float height)
    {
        Vector3 tile_world_pos = HexGrid.get_world_pos(new Vector3(0.0f, 0.0f, 0.0f), new Vector2(pos.x, pos.y), outerSize);
        HexBase = GameObject.Instantiate(hex, tile_world_pos, Quaternion.identity);
        HexBase.transform.parent = this.transform;
        HexBase.name = $"Hex C{pos.x},R{pos.y}";
        HexTile hextileScript = HexBase.GetComponent<HexTile>();
        hextileScript.Init(innersize, outerSize, height, pos);

        TileInfo oneInfo = new TileInfo(0, outerSize, 0.1f, pos);
        myMap.Add(pos, oneInfo);
    }

    public void AddObject(Estate.Estates myestate, Vector2Int pos)
    {
        if (HexBase == null) return;

        GameObject objToGen = myEstate.GetEstate((int)myestate);
        if(objToGen == null)
        {
            Debug.Log("obj to gen is null");
            return; 
        }
        Vector3 obj_world_pos = HexGrid.get_world_pos(new Vector3(0.0f, 0.0f, 0.0f), new Vector2(pos.x, pos.y), outerSize);
        HexBase.GetComponent<HexTile>().AddObj(objToGen, obj_world_pos);
        myMap.TryGetValue(pos, out TileInfo atile);
        atile.AddEstate(new EstateInfo(myestate, obj_world_pos));
    }

    /*private void AddInfo2Dic(Vector2Int pos, TileInfo mytile)
    {
        if(myMap.TryGetValue(pos, out TileInfo atile))
        {
            //atile
        }
    }*/

    public void OnSave()
    {

        List<TileInfo> saveData = new List<TileInfo>();
        foreach(KeyValuePair<Vector2Int, TileInfo> pairs in myMap)
        {
            saveData.Add(pairs.Value);
        }
        Debug.Log(Application.persistentDataPath);
        SaveHandler.SaveToJSON<TileInfo>(saveData, saveName);
    }

    public void OnLoad()
    {
        ClearMap(rawMapRoot);

        List<TileInfo> loadMap = SaveHandler.ReadFromJSON<TileInfo>(saveName);
        foreach(TileInfo info in loadMap)
        {
            AddHex(info.pos, info.thisTile.innerSize, info.thisTile.outerSize, info.thisTile.height);
            AddObject(info.estateinfo.estateType, info.pos);

        }
    }
}
