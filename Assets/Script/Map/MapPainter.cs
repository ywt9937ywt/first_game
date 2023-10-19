using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;


#if UNITY_EDITOR
//[CustomEditor(typeof(HexTile))]
[ExecuteInEditMode]
public class MapPainter : MonoBehaviour
{
    public string mapName;
    [InspectorButton(nameof(SaveMap))]
    public bool saveMap;
    public bool isEdit = false;
    public RemoveType removeType;
    //[InspectorButton(nameof(Switch2DPanel))]
    //public bool switch2d = false;
    public Color targetColor;
    public Estate.Estates estateToGen;
    public TileType tileType;

    //public GameObject mapEditor2D;

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnScene;
    }

    /*public void Switch2DPanel()
    {
        switch2d = !switch2d;
        mapEditor2D.SetActive(switch2d);
    }*/

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
            if (e.type == EventType.MouseDown && e.button == 2)
            {
                if (scriptHT != null) hit.transform.GetComponent<HexTile>().InvokeRootAddHex((int)tileType, scriptHT.pos);
            }
            else if(e.type == EventType.KeyDown && e.Equals(Event.KeyboardEvent(KeyCode.G.ToString())))
            {
                //Vector2Int pos = hit.transform.GetComponent<HexTile>().pos;
                //scriptHT = hit.transform.GetComponent<HexTile>();
                if (scriptHT != null) scriptHT.InvokeRootAddObj((int)estateToGen, scriptHT.pos);
            }
            else if(e.type == EventType.KeyDown && e.Equals(Event.KeyboardEvent(KeyCode.Backspace.ToString())))
            {
                Vector2Int pos = hit.transform.GetComponent<HexTile>().pos;
                if(removeType == RemoveType.EstateObj)
                {
                    //HexTile scriptHT = hit.transform.GetComponent<HexTile>();
                    if(scriptHT != null) scriptHT.InvokeRootChangeObj(-1, pos);
                }else if (removeType == RemoveType.HexTile)
                {
                    if (scriptHT != null) scriptHT.InvokeRootRemoveHex(1000, pos);
                }
            }else if (e.type == EventType.KeyDown && e.Equals(Event.KeyboardEvent(KeyCode.P.ToString())))
            {
                if (scriptHT != null) hit.transform.GetComponent<HexTile>().InvokeRootAddHex((int)tileType, scriptHT.pos);
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