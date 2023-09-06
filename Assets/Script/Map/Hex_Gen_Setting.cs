using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Script/Map/Generationsettings")]
public class Hex_Gen_Setting: ScriptableObject
{
    public enum TileType
    {
        Standard,
        Water,
        Cliff,
        Edit = 1000
    }

    public GameObject Standard;
    public GameObject Water;
    public GameObject Cliff;

    public GameObject GetTile(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Standard:
            case TileType.Edit:
                return Standard;
            case TileType.Water:
                return Water;
            case TileType.Cliff:
                return Cliff;
        }
        return null;
    }

}
