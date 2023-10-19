using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ManagerInitialization : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject globalDataManger;
    private GameObject ManagersRoot;
    void Awake()
    {
        ManagersRoot = new GameObject("Managers");
        //CreateGlobalDataManager();
    }

    private void CreateGlobalDataManager()
    {
        GameObject.Instantiate(globalDataManger, ManagersRoot.transform);
    }
    
}
