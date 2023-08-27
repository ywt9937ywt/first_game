using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hex_tile : MonoBehaviour
{
    private int halfCellSize = 1;
    private GameObject tile;
    private bool isDirty = false;
    private Interactable interactable_script;

    public UnityEvent onUserHover = new UnityEvent();
    public GameObject fow;

    public Vector3 center_pos;
    public Hex_Gen_Setting settings;
    public Hex_Gen_Setting.TileType tileType;
    public Transform parentTrans;
    public Vector2Int recCoordinate;
    public Vector3Int cubeCoordinate;
    public List<Hex_tile> neighbours;

    public Vector3 upperRightCorner { get; private set; }
    public Vector3 upperLeftCorner { get; private set; }
    public Vector3 upperCorner { get; private set; }
    public Vector3 lowerCorner { get; private set; }
    public Vector3 lowerRightCorner { get; private set; }
    public Vector3 lowerLeftCorner { get; private set; }
    void Start()
    {
        interactable_script = this.transform.GetComponent<Interactable>();
        init_event();
        info_generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDirty)
        {
            if (Application.isPlaying)
            {
                GameObject.Destroy(tile);
            }
            else
            {
                GameObject.DestroyImmediate(tile);
            }
            AddTile();
            isDirty = false;
        }
    }
    private void info_generate()
    {
        center_pos = this.transform.position;

        upperCorner = center_pos + new Vector3(0, 0, 1) * halfCellSize;
        lowerCorner = center_pos + new Vector3(0, 0, -1) * halfCellSize;

        upperRightCorner = center_pos + new Vector3(1, 0, 0) * halfCellSize + new Vector3(0, 0, 1) * halfCellSize * .5f;
        lowerRightCorner = center_pos + new Vector3(1, 0, 0) * halfCellSize + new Vector3(0, 0, -1) * halfCellSize * .5f;
        upperLeftCorner = center_pos + new Vector3(-1, 0, 0) * halfCellSize + new Vector3(0, 0, 1) * halfCellSize * .5f;
        lowerLeftCorner = center_pos + new Vector3(-1, 0, 0) * halfCellSize + new Vector3(0, 0, -1) * halfCellSize * .5f;
    }

    private void init_event()
    {
        interactable_script.onUserHover.AddListener(highLight_grid);
        interactable_script.onUserLeave.AddListener(highLight_grid_end);
    }

    public void highLight_grid()
    {
        Debug.Log("user begins hovering");
    }

    public void highLight_grid_end()
    {
        Debug.Log("user ends hovering");
    }

    public void RollTileType()
    {
        tileType = (Hex_Gen_Setting.TileType)Random.Range(0, 3);
    }

    private void OnMouseDown()
    {
        //Debug.Log(neighbours.Count);
    }

    public void AddTile()
    {
        tile = GameObject.Instantiate(settings.GetTile(tileType));
        if(gameObject.GetComponent<MeshCollider>() == null)
        {
            MeshCollider collider = gameObject.AddComponent<MeshCollider>();
            collider.sharedMesh = tile.GetComponentInChildren<MeshFilter>().mesh;
        }
        tile.transform.parent = parentTrans;
    }

    private void OnValidate()
    {
        if(tile == null) { return; }
        isDirty = true;
    }
    public void OnDrawGizmosSelected()
    {
        foreach (Hex_tile nb in neighbours)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.1f);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, nb.transform.position);
        }
    }
}
