/*#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using UnityEngine;

public class NotNullAttribute : PropertyAttribute
{
    
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(NotNullAttribute))]
public class NotNullDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = base.GetPropertyHeight(property, label);
        EditorGUI.PropertyField(position, property, label);
        position.y += position.height;
        if (IsRequire(property))
        {
            EditorGUI.HelpBox(position, "Set Value", MessageType.Error);
            Debug.LogError($"{property.displayName} is required but has a null reference.");
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (IsRequire(property))
        {
            return base.GetPropertyHeight(property, label) * 2f;
        }
        return base.GetPropertyHeight(property, label);
    }

    bool IsRequire(SerializedProperty property)
    {
        if (property.isArray)
        {
            if (property.arraySize == 0)
            {
                return true;
            }
        }
        else
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    if (property.intValue == 0)
                    {
                        return true;
                    }
                    break;
                case SerializedPropertyType.Float:
                    if (property.floatValue == 0f)
                    {
                        return true;
                    }
                    break;
                case SerializedPropertyType.String:
                    if (string.IsNullOrEmpty(property.stringValue))
                    {
                        return true;
                    }
                    break;
                case SerializedPropertyType.ObjectReference:
                    if (property.objectReferenceValue == null)
                    {
                        return true;
                    }
                    break;
            }
        }
        
        

        return false;
    }
    
    [InitializeOnLoadMethod]
    static void Initialize()
    {
        EditorApplication.playModeStateChanged += LogErrorOnPlayModeStateChanged;
    }

    static void LogErrorOnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            // Log an error when entering edit mode
            Debug.LogError("You are entering edit mode with null properties.");
        }
    }
}
#endif*/
