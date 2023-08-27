using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User_Target : MonoBehaviour
{
    public static User_Target instance { get; private set; }
    public Transform cam;
    private float playerActiveDistance = Mathf.Infinity;
    bool active = false;
    private Interactable last_interact_script;
    private GameObject cloestObject;
    private GameObject selectedObj;
    private GameObject LastSelectedObj;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        active = Physics.Raycast(ray, out hit, playerActiveDistance);
        if(active)
        {
            cloestObject = hit.transform.gameObject;
            Interactable cloest_interact_script = hit.transform.gameObject.GetComponent<Interactable>();
            if(cloest_interact_script != null)
            {
                if(last_interact_script != cloest_interact_script)
                {
                    cloest_interact_script.TriggeronUserHover();
                    if(last_interact_script != null)
                    {
                        last_interact_script.TriggeronUserLeave();
                    }
                    last_interact_script = cloest_interact_script;
                }
            }
            else
            {
                if(last_interact_script != null)
                {
                    last_interact_script.TriggeronUserLeave();
                    last_interact_script = null;
                }
            }
        }
        else
        {
            if (last_interact_script != null)
            {
                last_interact_script.TriggeronUserLeave();
                last_interact_script = null;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
    }

    private void OnMouseDown()
    {
        LastSelectedObj = selectedObj;
        selectedObj = cloestObject;
        if (selectedObj && selectedObj != LastSelectedObj)
        {
            cloestObject.GetComponent<Interactable>().TriggerOnSelected();
            if (LastSelectedObj)
            {
                LastSelectedObj.GetComponent<Interactable>().TriggerOffSelected();
            }
        }
        if (!selectedObj && LastSelectedObj)
        {
            LastSelectedObj.GetComponent<Interactable>().TriggerOffSelected();
        }
    }

    public GameObject getSelectedObj()
    {
        return selectedObj;
    }
}
