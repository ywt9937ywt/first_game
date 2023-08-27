using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UIManager
{
    private Stack<BasePanel> panelStack;
    private Dictionary<string, GameObject> pushedPanel;
    private GameObject uiRoot;

    public UIManager()
    {
        panelStack = new Stack<BasePanel>();
        pushedPanel = new Dictionary<string, GameObject>();
        /*foreach(UITuple uiTuple in UIInfo.UIInfoList)
        {
            GameObject uiObj = GameObject.Find(uiTuple.path);
            UIDic.Add(uiTuple.path, uiTuple.panel);
        }*/
        uiRoot = GameObject.Find("Canvas");
        GameStartUI();
    }

    private void GameStartUI()
    {
        string startPath = UIInfo.pathList[(int)UINickName.GameStartUI];
        UIInfo.UIInfoDic.TryGetValue(startPath, out BasePanel panel);
        if (panel != null)
        {
            Push(panel);
            //GameObject.Instantiate(startui, uiRoot.transform);
        }
    }

    public void Push(BasePanel nextPanelBase)
    {
        if (panelStack.Count > 0)
        {
            BasePanel panel = panelStack.Peek();
            panel.OnPause();
        }
        panelStack.Push(nextPanelBase);
        GetUI(nextPanelBase.path, uiRoot);
        //GameObject.Instantiate(Resources.Load<GameObject>(nextPanelBase.path), uiRoot.transform);
        //GameObject nextPanel = GetUI(nextPanelBase.info);
    }

    public void Pop()
    {
        if (panelStack.Count > 0)
        {
            BasePanel exitPanel = panelStack.Peek();
            exitPanel.OnExit();
            DestroyUI(exitPanel);
            panelStack.Pop();
        }
        if (panelStack.Count > 0)
        {
            panelStack.Peek().OnResume();
        }
    }
    public GameObject GetUI(string path, GameObject parent)
    {
        //GameObject parent = GameObject.Find("Canvas");
        if (!parent)
        {
            return null;
        }
        if (UIInfo.UIInfoDic.ContainsKey(path))
        {
            GameObject ui = GameObject.Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)), uiRoot.transform);
            if (ui)
            {
                pushedPanel[path] = ui;
            }
            //ui.name = 
        }
        //GameObject ui = GameObject.Instantiate(Resources.Load<GameObject>(info.UIPath), parent.transform);

        //ui.name = info.UIName;
        //UIDic.Add(info, ui);
        return null;
    }

    private void DestroyUI(BasePanel destroyPanel)
    {
        if (pushedPanel.ContainsKey(destroyPanel.path))
        {
            GameObject.Destroy(pushedPanel[destroyPanel.path]);
            pushedPanel.Remove(destroyPanel.path);
        }
    }
}
