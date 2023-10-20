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
    [InspectorButton(nameof(OnSave))]
    public bool saveButton;
    [InspectorButton(nameof(OnLoad))]
    public bool loadButton;
    [InspectorButton(nameof(ClearMap))]
    public bool clearButton;
    [InspectorButton(nameof(Awake))]
    public bool awakeButton;

    public string saveName;
    [ReadOnly] public Vector2Int currentPos;
    private GameObject rawMapRoot;
    private GameObject HexBase;
    private GridContainer gridContainer = new GridContainer();
    private bool realTile = false;

    public UnityEventWithArgIV2I OnObjChange = new UnityEventWithArgIV2I();
    public UnityEventWithArgIV2I OnHexChange = new UnityEventWithArgIV2I();

    public void Awake()
    {
        if (!(rawMapRoot = GameObject.Find("RawMapRoot")))
        {
            rawMapRoot = new GameObject("RawMapRoot");
        }
        OnObjChange.AddListener(ChangeObjInfo);
        OnHexChange.AddListener(ChangeHexInfo);
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
        gridContainer.Clear();
    }
    public void MapEditGrid()
    {
        ClearMap();
        for (int y = 0; y < GridSize.y; y++)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                AddHex(new Vector2Int(x, y), 0, outerSize, 0.1f, TileType.Edit);
                AddHexInfo((int)TileType.Edit, new Vector2Int(x, y));
            }
        }
    }

    public Transform AddHex(Vector2Int pos, float innersize, float outersize, float height, TileType tt)
    {
        Vector3 tile_world_pos = HexGrid.get_world_pos(new Vector3(0.0f, 0.0f, 0.0f), new Vector2(pos.x, pos.y), outerSize);
        HexBase = GameObject.Instantiate(hex, tile_world_pos, Quaternion.identity);
        HexBase.transform.parent = this.transform;
        HexBase.name = $"Hex C{pos.x},R{pos.y}";
        HexTile hextileScript = HexBase.GetComponent<HexTile>();
        hextileScript.Init(innersize, outerSize, height, pos, rawMapRoot, tt);

        return HexBase.transform;
    }

    private void AddHexInfo(int myHexid, Vector2Int pos)
    {
        TileInfo info = new TileInfo(0, outerSize, 0.1f, pos, (TileType)myHexid);
        gridContainer.Add(pos, info);
    }

    private void ChangeHexInfo(int myHexid, Vector2Int pos)
    {
        gridContainer.EditTile(myHexid, pos);
    }

    private void ChangeObjInfo(int myestate, Vector2Int pos)
    {
        gridContainer.TryChangeObjInfo(pos, myestate);
    }

    private void AddObject(int myestate, Vector2Int pos)
    {
        if (HexBase == null) return;

        HexBase.GetComponent<HexTile>().AddObj(myestate, pos);
    }

    public void InvokeChangeObj(Estate.Estates myestate, Vector2Int pos)
    {
        currentPos = pos;
        OnObjChange?.Invoke((int)myestate, pos);
    }

    public void InvokeChangeHex(TileType myestate, Vector2Int pos)
    {
        currentPos = pos;
        OnHexChange?.Invoke((int)myestate, pos);
    }

    public void OnSave()
    {
        gridContainer.Save(saveName);
    }

    public void OnLoad()
    {
        ClearMap();

        List<TileInfo> loadMap = gridContainer.Load(saveName);
        foreach (TileInfo info in loadMap)
        {
            Transform trans = AddHex(info.pos, info.thisTile.innerSize, info.thisTile.outerSize, info.thisTile.height, info.GetHexType());
            AddObject((int)info.estateType, info.pos);
        }
    }
}
