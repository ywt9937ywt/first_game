using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;


#if UNITY_EDITOR
[ExecuteInEditMode]
public class MapPainter : MonoBehaviour
{
    public string mapName;
    public bool isEdit = false;
    public RemoveType removeType;
    public Color targetColor;
    public Estate.Estates estateToGen;
    public TileType tileType;

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnScene;
    }

    void OnScene(SceneView scene)
    {
        if (Application.isPlaying || !isEdit) return;

        Event e = Event.current;

        Vector3 mousePos = e.mousePosition;
        float ppp = EditorGUIUtility.pixelsPerPoint;
        mousePos.y = scene.camera.pixelHeight - mousePos.y * ppp;
        mousePos.x *= ppp;

        Ray ray = scene.camera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            HexTile scriptHT = hit.transform.GetComponent<HexTile>();
            if (scriptHT == null) return;
                if (e.type == EventType.MouseDown && e.button == 2)
            {
                hit.transform.GetComponent<HexTile>().InvokeRootChangeHex((int)tileType);
            }
            else if(e.type == EventType.KeyDown && e.Equals(Event.KeyboardEvent(KeyCode.G.ToString())))
            {
                scriptHT.InvokeRootChangeObj((int)estateToGen);
            }
            else if(e.type == EventType.KeyDown && e.Equals(Event.KeyboardEvent(KeyCode.Backspace.ToString())))
            {
                if(removeType == RemoveType.EstateObj)
                {
                    scriptHT.InvokeRootChangeObj(-1);
                }else if (removeType == RemoveType.HexTile)
                {
                    scriptHT.InvokeRootChangeHex(1000);
                }
            }else if (e.type == EventType.KeyDown && e.Equals(Event.KeyboardEvent(KeyCode.P.ToString())))
            {
                hit.transform.GetComponent<HexTile>().InvokeRootChangeHex((int)tileType);
            }
        }
    }
    
    public void SaveMap()
    {
        string pathname = "Assets/Models/Prefeb/PreMap/" + mapName + ".prefab";
        PrefabUtility.SaveAsPrefabAsset(GameObject.Find("RawMapRoot"), AssetDatabase.GenerateUniqueAssetPath(pathname));
    }
}


public enum RemoveType
{
    HexTile = 0,
    EstateObj,
}
#endif