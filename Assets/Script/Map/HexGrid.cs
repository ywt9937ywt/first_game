using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [Header("Grid Setting")]
    public Vector2Int GridSize;

    [Header("Tile Settings")]
    public float outerSize = 0.5f;
    public float innerSize = 0f;
    public float height = 1f;
    public bool isFlatTopped;
    public Material material;

    [InspectorButton(nameof(LayoutGrid))]
    public bool buttonField;
    public Hex_Gen_Setting settings;

    private void Awake()
    {
        LayoutGrid();
    }
    public void Clear()
    {
        List<GameObject> children = new List<GameObject>();

        for(int i=0; i<transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            children.Add(child);
        }

        foreach(GameObject child in children)
        {
            DestroyImmediate(child, true);
        }
    }

    public void LayoutGrid()
    {
        Clear();
        for(int y = 0; y< GridSize.y; y++)
        {
            for(int x = 0; x < GridSize.x; x++)
            {
                GameObject tile = new GameObject($"Hex C{x},R{y}");
                tile.AddComponent<Interactable>();
                Hex_tile hextile = tile.AddComponent<Hex_tile>();
                hextile.settings = settings;
                hextile.recCoordinate = new Vector2Int(x, y);
                hextile.cubeCoordinate = Offset2Cube(new Vector2Int(x, y));
                hextile.parentTrans = tile.transform;
                Vector3 tile_world_pos = get_world_pos(new Vector3(0.0f, 0.0f, 0.0f), new Vector2(x, y));
                hextile.center_pos = tile_world_pos;
                hextile.RollTileType();
                hextile.AddTile();
                tile.transform.parent = this.transform;
                tile.transform.position = tile_world_pos;
            }
        }
    }

    public Vector3 get_world_pos(Vector3 offset, Vector2 v2)
    {
        float worldX = Mathf.Sqrt(3) * v2[0] * outerSize + ((v2[1] % 2 != 0) ? Mathf.Sqrt(3)/2*outerSize : 0);
        float worldZ = 1.5f * v2[1] * outerSize;
        Vector3 center_pos = offset + new Vector3(worldX, 0.0f, worldZ);
        return center_pos;
    }

    public Vector3Int Offset2Cube(Vector2Int offset)
    {
        int q = offset.x - (offset.y - (offset.y % 2)) / 2;
        int r = offset.y;
        return new Vector3Int(q, r, -q - r);
    }
}
