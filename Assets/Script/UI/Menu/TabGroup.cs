using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public GameObject inventoryParent;
    private TabButton selectedTab;
    private List<TabButton> tabButtons;
    private List<GameObject> objects2Swap;

    private void Awake()
    {
        objects2Swap = new List<GameObject>();
        foreach(Transform t in inventoryParent.transform)
        {
            objects2Swap.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
    }
    public void subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(button);
    }
    // Start is called before the first frame update
    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        {
            //button.background.sprite = tabHover;
            button.background.color = Color.yellow;
        }
        
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        if(selectedTab != null)
        {
            selectedTab.Deselected();
        }
        selectedTab = button;
        ResetTabs();
        //button.background.sprite = tabActive;
        button.background.color = Color.red;
        int idx = button.transform.GetSiblingIndex();
        for(int i = 0; i<objects2Swap.Count; i++)
        {
            if(i == idx)
            {
                objects2Swap[i].SetActive(true);
            }
            else
            {
                objects2Swap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if(selectedTab != null && button == selectedTab) { continue; }
            button.background.color = Color.gray;
            //button.background.sprite = tabIdle;
        }
    }
}
