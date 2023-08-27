using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rice : PlantBase
{
    public void Start()
    {
        init();
    }
    override protected void init()
    {
        Plant_Const_Reader.PlantList x = Plant_Const_Reader.myplantList;
        mature_count = Plant_Const_Reader.myplantList.Plants[(int)Plant_Type.RICE].mature_count;

    }

    override public void check_growing()
    {

    }
}
