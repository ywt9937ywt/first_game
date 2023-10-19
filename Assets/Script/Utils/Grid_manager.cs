using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_manager: MonoBehaviour
{
    public static Grid_manager GMinstance { get; private set; }
    [SerializeField]
    private GameObject fogOfWarPrefab;
    private Dictionary<Vector3Int, Hex_tile> tiles;
    private Hex_tile[] hexTiles;

    private void Awake()
    {
        if(GMinstance == null)
        {
            GMinstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        tiles = new Dictionary<Vector3Int, Hex_tile>();

        hexTiles = GameObject.Find("TileChunkHelper").GetComponentsInChildren<Hex_tile>();
        foreach(Hex_tile hexTile in hexTiles)
        {
            RegisterTile(hexTile);
            //AddFogOfWarTile(hexTile);
        }

        foreach (Hex_tile hexTile in hexTiles)
        {
            List<Hex_tile> neighbours = GetNeighbours(hexTile);
            hexTile.neighbours = neighbours;
        }
    }

    private void AddFogOfWarTile(Hex_tile tile)
    {
        GameObject fow = Instantiate(fogOfWarPrefab, transform);
        fow.name = "FOW" + tile.recCoordinate;
        fow.transform.position = tile.transform.position;
        tile.fow = fow;
    }
    private void RegisterTile(Hex_tile tile)
    {
        tiles.Add(tile.cubeCoordinate, tile);
    }

    private List<Hex_tile> GetNeighbours(Hex_tile tile)
    {
        List<Hex_tile> neighbours = new List<Hex_tile>();
        Vector3Int[] neighbourCoords = new Vector3Int[]
        {
            new Vector3Int(1, -1, 0),
            new Vector3Int(1, 0, -1),
            new Vector3Int(0, 1, -1),
            new Vector3Int(-1, 1, 0),
            new Vector3Int(-1, 0, 1),
            new Vector3Int(0, -1, 1),
        };
        foreach(Vector3Int neighborCoord in neighbourCoords)
        {
            Vector3Int tileNbCoord = tile.cubeCoordinate + neighborCoord;
            if(tiles.TryGetValue(tileNbCoord, out Hex_tile neighbor))
            {
                neighbours.Add(neighbor);
            }
        }
        return neighbours;
    }

    public Hex_tile GetValidPos()
    {
        Hex_tile tile = hexTiles[Random.Range(0, hexTiles.Length)];
        while(tile.tileType != TileType.Standard)
        {
            tile = hexTiles[Random.Range(0, hexTiles.Length)];
        }
        return tile;
    }


    
}
