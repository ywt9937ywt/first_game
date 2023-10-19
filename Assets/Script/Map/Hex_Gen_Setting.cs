using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Script/Map/Generationsettings")]
public class Hex_Gen_Setting: ScriptableObject
{
    

    public GameObject Grass;
    public GameObject Water;
    public GameObject Mountain;

    public GameObject Edit;

    public GameObject GetTile(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Edit:
                return Edit;
            case TileType.Grass:
                return Grass;
            case TileType.Water:
                return Water;
            case TileType.Mountain:
                return Mountain;
        }
        return null;
    }

}
public enum TileType
{
    Grass,
    Water,
    Mountain,
    Edit = 1000
}