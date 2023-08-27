using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private Vector2Int playerCoord { get; set; }
    private Vector3 playerPos { get; set; }
    private Hex_tile currentTile;
    public static PlayerStatusManager PSMinstance{ get; private set; }
    private void Awake()
    {
        if (PSMinstance == null)
        {
            PSMinstance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Hex_tile validTile = Grid_manager.GMinstance.GetValidPos();
        playerCoord = validTile.recCoordinate;
        playerPos = validTile.center_pos + new Vector3(0, 0.5f, 0);
        Instantiate(playerPrefab, playerPos, Quaternion.identity);
        currentTile = validTile;
    }

    public Hex_tile GetCurrentTile()
    {
        return currentTile;
    }
}
