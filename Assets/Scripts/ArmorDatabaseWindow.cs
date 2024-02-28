using UnityEditor;
using UnityEngine;

public class ArmorDatabaseWindow : EditorWindow
{
    [MenuItem("Window/Armor Database")]
    public static void ShowWindow()
    {
        GetWindow<ArmorDatabaseWindow>("Armor Database");
    }

    private void OnGUI()
    {
        GUILayout.Label("Armor Database", EditorStyles.boldLabel);
        // Add elements
    }
}
