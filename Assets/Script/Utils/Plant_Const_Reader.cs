using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Plant_Const_Reader : MonoBehaviour
{
    public static Plant_Const_Reader plant_csv_reader;
    public TextAsset plant_const_csv;
    public static PlantList myplantList = new PlantList();

    [System.Serializable]
    public class Plant_Const
    {
        public string name { get; }
        public float mature_count { get; }
        public float growSpeed { get; }
        public Dictionary<GrowDirection, Vector3> gDirThresholdSpeed { get; }
        public List<ConditionBase> commonConditionList { get; }
        public Plant_Const(string name, float mature_value, float gs, Dictionary<GrowDirection, Vector3> gD, List<ConditionBase> commonCon)
        {
            this.name = name;
            this.mature_count = mature_value;
            this.growSpeed = gs;
            this.gDirThresholdSpeed = gD;
            this.commonConditionList = commonCon;
        }

    }

    public class PlantList
    {
        public Plant_Const[] Plants;
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (plant_csv_reader == null)
        {
            plant_csv_reader = this;
        }
        else
        {
            Destroy(this);
        }
        ReadCSV();
    }

    private void ReadCSV()
    {
        string[] row_data = plant_const_csv.text.Split(new string[] { "\n" }, StringSplitOptions.None);

        int tableSize = row_data.Length - 2;
        myplantList.Plants = new Plant_Const[tableSize];
        for (int i = 0; i < tableSize; i++)
        {
            string[] split_row_data = row_data[i + 1].Split(new string[] { "," }, StringSplitOptions.None);
            string name = split_row_data[0];
            float mature_value = float.Parse(split_row_data[1]);
            float commonGrowSpeed = float.Parse(split_row_data[2]);
            string[] commonCons = split_row_data[3].Split(new string[] { "+" }, StringSplitOptions.None);
            List<ConditionBase> commonConditionList = new List<ConditionBase>();
            for (int j = 0; j < commonCons.Length; j++)
            {
                commonConditionList.Add(Conditions.GetCondition((ConditionName)Int32.Parse(commonCons[j])));
            }

            string[] specialCons = split_row_data[4].Split(new string[] { "+" }, StringSplitOptions.None);
            Dictionary<GrowDirection, Vector3> gDirThresholdSpeed = new Dictionary<GrowDirection, Vector3>();
            for (int j = 0; j < specialCons.Length; j++)
            {
                GrowDirection dir = (GrowDirection)(specialCons[j][0] - 48);
                string[] leftString = specialCons[j].Split(new string[] { "-" }, StringSplitOptions.None);
                string[] pair = leftString[1].Split(new string[] { ":" }, StringSplitOptions.None);
                for (int x = 0; x < pair.Length; x += 3)
                {
                    gDirThresholdSpeed[dir] = new Vector3(Int32.Parse(pair[x]), Int32.Parse(pair[x + 1]), Int32.Parse(pair[x + 2]));
                }
            }

            myplantList.Plants[i] = new Plant_Const(name, mature_value, commonGrowSpeed, gDirThresholdSpeed, commonConditionList);
        }

        Debug.Log("loaded csv");
    }

}
