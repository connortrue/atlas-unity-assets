using UnityEditor;
using UnityEngine;

public class PotionDatabaseWindow : EditorWindow
{
    [MenuItem("Window/Potion Database")]
    public static void ShowWindow()
    {
        GetWindow<PotionDatabaseWindow>("Potion Database");
    }

    private void OnGUI()
    {
        GUILayout.Label("Potion Database", EditorStyles.boldLabel);
        // Add elements
    }
}
