using UnityEditor;
using UnityEngine;

public class WeaponDatabaseWindow : EditorWindow
{
    [MenuItem("Window/Weapon Database")]
    public static void ShowWindow()
    {
        GetWindow<WeaponDatabaseWindow>("Weapon Database");
    }

    private void OnGUI()
    {
        GUILayout.Label("Weapon Database", EditorStyles.boldLabel);
        // my elements
    }
}
