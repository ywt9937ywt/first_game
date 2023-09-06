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

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnScene;
    }

    void OnScene(SceneView scene)
    {
        if (Application.isPlaying || !isEdit) return;

        Event e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 2)
        {
            Debug.Log("Middle Mouse was pressed");

            Vector3 mousePos = e.mousePosition;
            float ppp = EditorGUIUtility.pixelsPerPoint;
            mousePos.y = scene.camera.pixelHeight - mousePos.y * ppp;
            mousePos.x *= ppp;

            Ray ray = scene.camera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject.name);
                hit.transform.parent.GetComponent<HexTile>().SetHexBase(targetColor);
            }
            e.Use();
        }
    }
    
    public void SaveMap()
    {
        string pathname = "Assets/Models/Prefeb/PreMap/" + mapName + ".prefab";
        PrefabUtility.SaveAsPrefabAsset(GameObject.Find("RawMapRoot"), AssetDatabase.GenerateUniqueAssetPath(pathname));
    }
}
