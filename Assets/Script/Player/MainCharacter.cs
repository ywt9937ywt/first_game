using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public Hex_tile currentTile;
    private Interactable interactable_script;
    private bool Selected = false;
    List<Hex_tile> pathList;
    // Start is called before the first frame update
    void Awake()
    {
        pathList = new List<Hex_tile>();
        interactable_script = this.transform.GetComponent<Interactable>();
        initEvent();
    }

    private void initEvent()
    {
        interactable_script.onUserHover.AddListener(HighLightCharacter);
        interactable_script.onUserLeave.AddListener(HighLightCharacterEnd);
        interactable_script.onSelected.AddListener(CharacterSelected);
        interactable_script.offSelected.AddListener(CharacterSelectedEnd);
    }

    private void HighLightCharacter()
    {
        Debug.Log("highLightCharacter");
    }
    private void HighLightCharacterEnd()
    {
        Debug.Log("highLightCharacter finish");
    }

    private void CharacterSelected()
    {
        Debug.Log("Main Character is selected");
        Selected = true;
    }
    private void CharacterSelectedEnd()
    {
        Debug.Log("Main Character selected off");
        GameObject g = User_Target.instance.getSelectedObj();
        Hex_tile destinationTile = g.GetComponent<Hex_tile>();
        if (destinationTile)
        {
            pathList = PathFinder.PfInstance.FindPath(PlayerStatusManager.PSMinstance.GetCurrentTile(), destinationTile);
            Debug.Log(PlayerStatusManager.PSMinstance.GetCurrentTile().recCoordinate + " " + destinationTile.recCoordinate + " " + pathList.Count);
        }
        Selected = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTilePosition(Hex_tile tile)
    {
        currentTile = tile;
        this.transform.position = tile.center_pos + new Vector3(0, 0.5f, 0); ;
    }

       /* public void OnDrawGizmos()
    {
        if (pathList != null)
        {
            float t = 0.1f;
            foreach(Hex_tile tile in pathList)
            {
                Gizmos.color = new Color(t,0,0);
                t += 0.1f;
                Gizmos.DrawCube(tile.transform.position + new Vector3(0f, 0.5f, 0f), new Vector3(0.5f, 0.5f, 0.5f));
            }
        }
    }*/

    
}
