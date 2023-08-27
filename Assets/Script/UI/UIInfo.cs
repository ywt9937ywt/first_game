using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum UINickName
{
    GameStartUI = 0,
}

/*public class UITuple
{
    public string path { get; private set; }
    public BasePanel panel { get; private set; }
    public UITuple(string path, BasePanel panel)
    {
        this.path = path;
        this.panel = panel;
    }
}*/
public class UIInfo
{
    public static Dictionary<string, BasePanel> UIInfoDic;
    public static List<string> pathList;

    static UIInfo()
    {
        UIInfoDic = new Dictionary<string, BasePanel>()
        {
            { "Assets/Models/Prefeb/UI/StartPanel.prefab", new StartPanel()},
        };

        pathList = new List<string>()
        {
            "Assets/Models/Prefeb/UI/StartPanel.prefab",
        };
        //UIName = path.Substring(path.LastIndexOf('/') + 1);
    }
}
