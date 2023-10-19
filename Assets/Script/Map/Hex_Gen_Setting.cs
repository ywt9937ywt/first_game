using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Script/Map/Generationsettings")]
public class Hex_Gen_Setting: ScriptableObject
{
    

    public GameObject Standard;
    public GameObject Water;
    public GameObject Cliff;

    public GameObject Edit;

    public GameObject GetTile(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Edit:
                return Edit;
            case TileType.Standard:
                return Standard;
            case TileType.Water:
                return Water;
            case TileType.Cliff:
                return Cliff;
        }
        return null;
    }

}
public enum TileType
{
    Standard,
    Water,
    Cliff,
    Mountain,
    Edit = 1000
}