using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[AddComponentMenu("")]
public class CellContent : MonoBehaviour
{
    [HideInInspector] public bool active = true, isEditing = false;


    public Color defaultColor = Color.clear;

    public void SetHexColor(int x, int y, Color c)
    {

    }
}
