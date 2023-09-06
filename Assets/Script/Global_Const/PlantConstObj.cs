using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Script/Const/PlantConst")]
public class PlantConstObj : ScriptableObject
{
    public class PlantConst
    {
        public string name { get; }
        public float mature_count { get; }
        public float growSpeed { get; }
        public Dictionary<GrowDirection, List<GrowDirectionRequire>> gDirThresholdSpeed { get; }
        public List<ConditionBase> commonConditionList { get; }
        public PlantConst(string name, float mature_value, float gs, Dictionary<GrowDirection, List<GrowDirectionRequire>> gD, List<ConditionBase> commonCon)
        {
            this.name = name;
            this.mature_count = mature_value;
            this.growSpeed = gs;
            this.gDirThresholdSpeed = gD;
            this.commonConditionList = commonCon;
        }
    }

    public class GrowDirectionRequire
    {
        public ConditionName nameCon { get; }
        public int dirThreshold { get; }
        public float growSpeed { get; }
    }

    public PlantConst[] plantConstArray = new PlantConst[10];
    public void Awake()
    {
        
        //plantConstArray[(int)Plant_Type.FLOWER]
    }
}
