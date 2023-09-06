using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMap : MonoBehaviour
{
    public GameObject hex;
    public Vector2Int GridSize = new Vector2Int(2, 2);
    public Hex_Gen_Setting settings;
    public float outerSize = 0.5f;
    [InspectorButton(nameof(MapEditGrid))]
    public bool editButton;

    public void Clear(GameObject r)
    {
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
    }
    public void MapEditGrid()
    {
        //Clear();
        GameObject rawMapRoot;
        if (!(rawMapRoot = GameObject.Find("RawMapRoot")))
        {
            rawMapRoot = new GameObject("RawMapRoot");
        }
        Clear(rawMapRoot);
        //GameObject xTile = GameObject.Instantiate(settings.GetTile(Hex_Gen_Setting.TileType.Edit));
        for (int y = 0; y < GridSize.y; y++)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                AddHex(new Vector2(x, y), 0, outerSize, 0.1f);
                /*GameObject tile = new GameObject($"Hex C{x},R{y}");
                Vector3 tile_world_pos = HexGrid.get_world_pos(new Vector3(0.0f, 0.0f, 0.0f), new Vector2(x, y), outerSize);
                tile.transform.parent = rawMapRoot.transform;
                tile.transform.position = tile_world_pos;
                //GameObject myTile = GameObject.Instantiate(settings.GetTile(Hex_Gen_Setting.TileType.Edit));
                //UnityEditor.Events.UnityEventTools.AddPersistentListener(btn.onClick, btn.BtnCall);
                GameObject myTile = GameObject.Instantiate(settings.GetTile(Hex_Gen_Setting.TileType.Edit));
                myTile.AddComponent<MeshCollider>();
                myTile.transform.parent = tile.transform;
                myTile.transform.localPosition = new Vector3(0, 0, 0);*/
            }
        }

    }

    public void AddHex(Vector2 pos, float innersize, float outersize, float height)
    {
        Vector3 tile_world_pos = HexGrid.get_world_pos(new Vector3(0.0f, 0.0f, 0.0f), new Vector2(pos.x, pos.y), outerSize);
        GameObject h = GameObject.Instantiate(hex, tile_world_pos, Quaternion.identity);
        h.transform.parent = this.transform;
        h.name = $"Hex C{pos.x},R{pos.y}";
        HexTile hextileScript = h.GetComponent<HexTile>();
        hextileScript.Init(innersize, outerSize, height);
    }
}
