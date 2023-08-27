using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerInitialization : MonoBehaviour
{
    // Start is called before the first frame update
    public UIManager uiManager { get; private set; }
    void Awake()
    {
        uiManager = new UIManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
