using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class SceneEditorView
{
    //static string objCountStr = string.Empty;
    static bool objCountDisplay = false;

    static SceneEditorView()
    {
        SceneView.duringSceneGui += YWTOnSceneGUI;

    }

    static void YWTOnSceneGUI(SceneView sceneView)
    {
        Handles.BeginGUI();
        GUILayout.FlexibleSpace();
        GUI.skin.label.fontSize = 10;
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
        //GUILayout.Label(objCountStr);
        EditorGUILayout.BeginHorizontal();

        if(GUILayout.Button("button name", EditorStyles.miniButtonRight, GUILayout.Width(85))){

        }
        EditorGUILayout.EndHorizontal();
        GUI.skin.label.fontSize = 14;
        GUI.skin.label.alignment = TextAnchor.UpperLeft;
        Handles.EndGUI();
    }


}
