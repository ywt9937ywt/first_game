using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

//[CustomEditor(typeof(HexTile))]
[ExecuteInEditMode]
public class MapPainter : MonoBehaviour
{
    public string mapName;
    [InspectorButton(nameof(SaveMap))]
    public bool saveMap;
    public bool isEdit = false;
    public Color targetColor;

    public Estate.Estates estateToGen;

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
            if (e.type == EventType.MouseDown && e.button == 2)
            {
                hit.transform.parent.GetComponent<HexTile>().SetHexBase(targetColor);
            }
            if(e.type == EventType.KeyDown && e.Equals(Event.KeyboardEvent(KeyCode.G.ToString())))
            {
                Debug.Log(" gen obj");
                Vector2Int pos = hit.transform.parent.GetComponent<HexTile>().pos;
                hit.transform.parent.parent.GetComponent<RawMap>().AddObject(estateToGen, pos);
            }
                    
        }
        //e.Use();
    }
    
    public void SaveMap()
    {
        string pathname = "Assets/Models/Prefeb/PreMap/" + mapName + ".prefab";
        PrefabUtility.SaveAsPrefabAsset(GameObject.Find("RawMapRoot"), AssetDatabase.GenerateUniqueAssetPath(pathname));
    }
}
