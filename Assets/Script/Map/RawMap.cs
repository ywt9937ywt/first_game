using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventWithArgument;

[ExecuteInEditMode]
public class RawMap : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(2, 2);
    [Header("HexDetail")]
    public GameObject hex;
    public float outerSize = 0.5f;

    [Header("MapDetail")]
    [InspectorButton(nameof(MapEditGrid))]
    public bool editButton;
    public Hex_Gen_Setting settings;
    public string saveName;
    [InspectorButton(nameof(OnSave))]
    public bool saveButton;
    [InspectorButton(nameof(OnLoad))]
    public bool loadButton;
    [InspectorButton(nameof(ClearMap))]
    public bool clearButton;
    [InspectorButton(nameof(Awake))]
    public bool awakeButton;

    [ReadOnly] public Vector2Int currentPos;
    private GameObject rawMapRoot;
    //Dictionary<Vector2Int, TileInfo> myMap = new Dictionary<Vector2Int, TileInfo>();
    //GridContainer myGridContainer = new GridContainer();
    private GameObject HexBase;
    private GridContainer gridContainer = new GridContainer();
    private bool realTile = false;

    public UnityEventWithArgIV2I OnObjAdd = new UnityEventWithArgIV2I();
    public UnityEventWithArgIV2I OnObjChange = new UnityEventWithArgIV2I();
    public UnityEventWithArgIV2I OnHexAdd = new UnityEventWithArgIV2I();
    public UnityEventWithArgIV2I OnHexChange = new UnityEventWithArgIV2I();

    public void Awake()
    {
        if (!(rawMapRoot = GameObject.Find("RawMapRoot")))
        {
            rawMapRoot = new GameObject("RawMapRoot");
        }
        OnObjAdd.AddListener(AddObjectInfo);
        OnObjChange.AddListener(ChangeObjInfo);
        OnHexAdd.AddListener(AddHexInfo);
    }
    public void ClearMap()
    {
        if(rawMapRoot == null)
        {
            Debug.Log("map root not found");
            return;
        }
        List<GameObject> children = new List<GameObject>();

        for (int i = 0; i < rawMapRoot.transform.childCount; i++)
        {
            GameObject child = rawMapRoot.transform.GetChild(i).gameObject;
            children.Add(child);
        }

        foreach (GameObject child in children)
        {
            DestroyImmediate(child, true);
        }
        //GridContainer.GCinstance.Clear();
        gridContainer.Clear();
    }
    public void MapEditGrid()
    {
        ClearMap();
        //GameObject xTile = GameObject.Instantiate(settings.GetTile(Hex_Gen_Setting.TileType.Edit));
        for (int y = 0; y < GridSize.y; y++)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                AddHex(new Vector2Int(x, y), 0, outerSize, 0.1f, TileType.Edit);
                AddHexInfo((int)TileType.Edit, new Vector2Int(x, y));
            }
        }
        //Debug.Log("1.  " + gridContainer.TileCount());
    }

    public Transform AddHex(Vector2Int pos, float innersize, float outersize, float height, TileType tt)
    {
        Vector3 tile_world_pos = HexGrid.get_world_pos(new Vector3(0.0f, 0.0f, 0.0f), new Vector2(pos.x, pos.y), outerSize);
        HexBase = GameObject.Instantiate(hex, tile_world_pos, Quaternion.identity);
        HexBase.transform.parent = this.transform;
        HexBase.name = $"Hex C{pos.x},R{pos.y}";
        HexTile hextileScript = HexBase.GetComponent<HexTile>();
        hextileScript.Init(innersize, outerSize, height, pos, rawMapRoot, tt);

        //TileInfo oneInfo = new TileInfo(0, outerSize, 0.1f, pos);
        //GridContainer.GCinstance.Add(pos, oneInfo);
        //if(updateInfo) gridContainer.Add(pos, oneInfo);
        return HexBase.transform;
    }

    private void AddHexInfo(int myHexid, Vector2Int pos)
    {
        TileInfo info = new TileInfo(0, outerSize, 0.1f, pos, (TileType)myHexid);
        gridContainer.Add(pos, info);
    }

    private void AddObjectInfo(int myestate, Vector2Int pos)
    {
        //if (HexBase == null) return;
        
        Vector3 obj_world_pos = HexGrid.get_world_pos(new Vector3(0.0f, 0.0f, 0.0f), new Vector2(pos.x, pos.y), outerSize);
        gridContainer.TryEditTile(pos, out TileInfo atile);
        if(atile != null ) atile.AddEstate(new EstateInfo((Estate.Estates)myestate, obj_world_pos));
        //Debug.Log("2.  " + gridContainer.TileCount());
    }

    private void RemoveObjectInfo(int myestate, Vector2Int pos)
    {
        gridContainer.TryDeleteObjInfo(pos);
    }

    private void ChangeObjInfo(int myestate, Vector2Int pos)
    {
        if(myestate == -1)
        {
            RemoveObjectInfo(myestate, pos);
        }
        //Debug.Log("3.  " + gridContainer.TileCount());
    }

    private void AddObject(int myestate, Vector2Int pos)
    {
        if (HexBase == null) return;

        HexBase.GetComponent<HexTile>().AddObj(myestate, pos);
    }

    public void InvokeAddObj(int myestate, Vector2Int pos)
    {
        currentPos = pos;
        OnObjAdd?.Invoke(myestate, pos);
    }

    public void InvokeChangeObj(int myestate, Vector2Int pos)
    {
        currentPos = pos;
        OnObjChange?.Invoke(myestate, pos);
    }

    public void InvokeAddHex(int myestate, Vector2Int pos)
    {
        currentPos = pos;
        OnHexAdd?.Invoke(myestate, pos);
    }

    public void OnSave()
    {
        //Debug.Log("4.  " + gridContainer.TileCount());
        //GridContainer.GCinstance.Save(name);
        gridContainer.Save(saveName);
    }

    public void OnLoad()
    {
        ClearMap();

        //List<TileInfo> loadMap = GridContainer.GCinstance.Load(saveName);
        List<TileInfo> loadMap = gridContainer.Load(saveName);
        foreach (TileInfo info in loadMap)
        {
            Transform trans = AddHex(info.pos, info.thisTile.innerSize, info.thisTile.outerSize, info.thisTile.height, info.GetHexType());
            //AddObjectInfo((int)info.estateinfo.estateType, info.pos);
            AddObject((int)info.estateinfo.estateType, info.pos);
        }
    }
}
