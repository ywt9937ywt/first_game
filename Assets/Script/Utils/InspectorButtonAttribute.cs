using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Reflection;

/// <summary>
/// This attribute can only be applied to fields because its
/// associated PropertyDrawer only operates on fields (either
/// public or tagged with the [SerializeField] attribute) in
/// the target MonoBehaviour.
/// </summary>
//[System.AttributeUsage(System.AttributeTargets.Method)]
public class InspectorButtonAttribute : PropertyAttribute
{
    public static float kDefaultButtonWidth = 80;

    public readonly string MethodName;

    private float _buttonWidth = kDefaultButtonWidth;
    public float ButtonWidth
    {
        get { return _buttonWidth; }
        set { _buttonWidth = value; }
    }

    public InspectorButtonAttribute(string MethodName)
    {
        this.MethodName = MethodName;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(InspectorButtonAttribute))]
public class InspectorButtonPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string methodName = (attribute as InspectorButtonAttribute).MethodName;
        Object target = property.serializedObject.targetObject;
        System.Type type = target.GetType();

        System.Reflection.MethodInfo method = type.GetMethod(methodName);
        if(GUI.Button(position, method.Name))
        {
            method.Invoke(target, null);
        }
    }
}
#endif