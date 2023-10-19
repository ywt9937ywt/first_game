using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GlobalSetting
{
    [ExecuteInEditMode]
    public class GlobalScripableObj : MonoBehaviour
    {
        private static GlobalScripableObj G_ScriptableObjinstance;
        private static GlobalScripableObj G_editScriptableObjinstance;
        public Estate EstateSetting;
        public Hex_Gen_Setting hexSetting;

        [InspectorButton(nameof(Awake))]
        public bool loadButton;
        public static GlobalScripableObj Instance
        {
            get
            {
                if (Application.isPlaying)
                    return G_ScriptableObjinstance;

                return G_editScriptableObjinstance;
            }
        }

        public void Awake()
        {
            if (Application.isPlaying)
            {
                if (G_ScriptableObjinstance)
                {
                    Destroy(this);
                }

                G_ScriptableObjinstance = this;
            }
            else
            {
                if (G_editScriptableObjinstance)
                {
                    DestroyImmediate(this);
                }

                G_editScriptableObjinstance = this;
            }
        }
    }
}

