using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class HexMapEditor : EditorWindow
{
    private static SceneGridEditorView view = SceneGridEditorView.None;
    private static CellContent contentLayer;

    private static Vector2Int selectedHex;
    private static Vector2Int activeHex;
    private static Color selectColor = new Color(0.9f, 0.8f, 0.7f, 0.3f);
    private static Color selectTextColor = new Color(1f, 0.9f, 0.75f);

    private static Color highColor = new Color(1f, 0.2f, 0.27f, 0.7f);
    private static Color lowColor = new Color(0.2f, 1f, 0.2f, 0.7f);

    private static Color fullColor = new Color(1f, 1f, 1f, 0.7f);
    private static Color emptyColor = new Color(0f, 0f, 0f, 0.7f);

    private static string sceneViewStr = "Scene";
    private static string hierViewStr = "UnityEditor.SceneHierarchyWindow,UnityEditor.dll";
    private static bool isMouseInSceneView, oIsMouseInSceneView;

    private static GUIContent viewButtionCnt, drawButtonCnt;

    //[MenuItem("Window/Map/MapEditor2D")]
    public static void Initialize()//CellContent cellContent
    {
        HexMapEditor wnd = GetWindow<HexMapEditor>();
        wnd.titleContent = new GUIContent("MyEditorWindow");

        selectedHex = new Vector2Int(int.MinValue, int.MinValue);
        activeHex = new Vector2Int(int.MinValue, int.MinValue);
        //contentLayer = AddComponent;
        contentLayer.isEditing = true;

        selectTextColor = new Color(1f, 0.9f, 0.75f);
        //activeCell = new Vector2Int(int.MinValue, int.MinValue);

        viewButtionCnt = new GUIContent();
        drawButtonCnt = new GUIContent();

        SceneView.duringSceneGui += YWTOnSceneView;
        GetWindow<HexMapEditor>("Hex Map Editor");//, new Type[] { Type.GetType(hierViewStr) }
    }

    private void Update()
    {
        isMouseInSceneView = mouseOverWindow != null && mouseOverWindow.titleContent != null;// && mouseOverWindow.titleContent.text.Equals(sceneViewStr);
        if(isMouseInSceneView != oIsMouseInSceneView)
        {
            if (isMouseInSceneView) OnMouseEnterScene();
            else OnMouseLeaveScene();

        }
        oIsMouseInSceneView = isMouseInSceneView;
        Repaint();
    } 

    private static void OnMouseEnterScene()
    {

    }

    private static void OnMouseLeaveScene()
    {
        if(activeHex != new Vector2Int(int.MinValue, int.MinValue))
        {
            contentLayer.SetHexColor(activeHex.x, activeHex.y, contentLayer.defaultColor);
        }
    }

    private void OnGUI()
    {
        if (!contentLayer.isEditing)
        {
            Close();
        }
        DrawTopBar();

    }

    private static void DrawTopBar()
    {
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.skin.label.fontSize = 15;
        if (!contentLayer.isEditing)
        {
            GUILayout.Label("No active grid");
        }
        else
        {
            //GUILayout.Label($"active Grid : {contentLayer.name}");
        }
        EditorGUI.DrawRect(new Rect(0, 25, 400, 3), Color.gray);
        GUILayout.Space(10);

        GUI.skin.button.alignment = TextAnchor.MiddleCenter;
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button(viewButtionCnt, GUILayout.Width(30), GUILayout.Height(30)))
        {

        }
        if (GUILayout.Button(drawButtonCnt, GUILayout.Width(30), GUILayout.Height(30)))
        {

        }
        EditorGUILayout.EndHorizontal();
    }

    private static void DrawViewMenu()
    {
        EditorGUILayout.LabelField("Grid View: ");
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("View Cell Value"))
        {
            //SetCellValueView();
            //view 
        }
        EditorGUILayout.EndHorizontal();

        GUI.contentColor = new Color(0.9f, 0.8f, 0.7f);
        if(GUILayout.Button("Erase Grid View"))
        {
            //EraseGridView();
            //
        }
        GUI.contentColor = Color.white;
        GUILayout.Space(30);
    }

    private static void DrawSelection()
    {
        GUI.skin.label.fontSize = 12;
        GUILayout.Label("Selected Cell:");
        GUI.skin.label.fontSize = 30;
        GUI.contentColor = selectTextColor;
        GUILayout.Label($"{selectedHex.x} , {selectedHex.y}");
        GUI.contentColor = Color.white;

        if(selectedHex != new Vector2(int.MinValue, int.MinValue))
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndHorizontal();
        }

        GUILayout.Space(15);
        EditorGUI.DrawRect(new Rect(0, 290, 4000, 1.5f), Color.gray);
        GUILayout.Space(15);
        GUI.skin.label.fontSize = 12;
        GUILayout.Label("Hovered Cell:");
        GUI.skin.label.fontSize = 20;
        GUILayout.Label($"{activeHex.x} , {activeHex.y}");
    }

    private void OnDestroy()
    {
        // EraseGirdView();
        contentLayer.isEditing = false;
        SceneView.duringSceneGui -= YWTOnSceneView;
    }

    public static void FinishEdit(CellContent cellCon)
    {
        contentLayer = cellCon;
        contentLayer.isEditing = false;
        SceneView.duringSceneGui -= YWTOnSceneView;

        // ask for whether save the map

        //EraseGridView();
        //cellContent.SaveGridToAssets(cellContent.saveAsset);
    }

    public static void YWTOnSceneView(SceneView sv)
    {
        /*Selection.activeGameObject = contentLayer.gameObject;
        if (Selection.activeGameObject == contentLayer.gameObject && isMouseInSceneView)
        {
            if(view == SceneGridEditorView.None)
            {
                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                /*Vector2Int curCell = GridContainer.GCinstance.PositionToCell(hit.point);
                if (activeHex != curCell && activeHex != selectedHex)
                {
                    layer.SetCellColor(activeHex.x, activeHex.y, layer.defaultColor);
                }
                activeHex = curCell;

                if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
                {
                    if (selectedHex != new Vector2Int(-1, -1))
                    {
                        layer.EraseCellColor(selectedHex.x, selectedHex.y);
                        selectedHex = new Vector2Int(-1, -1);
                    }

                    selectedHex = activeHex;
                    layer.SetCellColor(activeHex.x, activeHex.y, new Color(1, 1, 1, 0.8f));

                }
                else if (activeHex != selectedHex)
                {
                    layer.SetCellColor(activeHex.x, activeHex.y, selectColor);
                }
            }
        }*/
    }
}

public enum SceneGridEditorView
{
    None,
}
