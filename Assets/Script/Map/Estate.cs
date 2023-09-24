using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Script/Map/Estates")]
public class Estate : ScriptableObject
{
    const int segment = 1000;
    public enum Estates
    {
        Pine = 1,


        Church = segment,
    }
    public GameObject pine; 
    public GameObject church;

    public GameObject GetEstate(int enumid)
    {
        if (enumid >= 1 && enumid < segment)
            return pine;
        if (enumid >= segment && enumid < 2* segment)
            return church;

        return null;
        
    }

}
[System.Serializable]
public class EstateInfo
{
    public Estate.Estates estateType;
    public Vector3 position;
    //public Vector3 rotation;

    public EstateInfo(Estate.Estates est, Vector3 pos)
    {
        estateType = est;
        position = pos;
    }
}
