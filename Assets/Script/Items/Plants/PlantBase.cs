using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlantBase: MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    protected float growing_count;
    protected float mature_count;
    protected float[] growing_dir_count;

    virtual protected void init()
    {
        growing_count = 0;
        mature_count = 100;
        growing_dir_count = new float[5];
    }
    abstract public void check_growing();

}
