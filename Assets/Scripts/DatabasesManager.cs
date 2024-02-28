using UnityEditor;
using UnityEngine;

public class DatabasesManager : EditorWindow
{
    // Add a menu item named "Database Manager" to the Window menu
    [MenuItem("Window/Database Manager")]
    public static void ShowWindow()
    {
        // Show existing window instance. If one doesn't exist, make one.
        DatabasesManager wnd = (DatabasesManager)EditorWindow.GetWindow(typeof(DatabasesManager));
        wnd.titleContent = new GUIContent("Weapons");
        wnd.minSize = new Vector2(450, 200);
        wnd.maxSize = new Vector2(1920, 720);
    }

    private void OnGUI()
    {
        GUILayout.Label("Database Manager", EditorStyles.boldLabel);

        // Button to open the Armor Database window
        if (GUILayout.Button("Open Armor Database"))
        {
            ArmorDatabaseWindow.ShowWindow();
        }

        // Button to open the Potion Database window
        if (GUILayout.Button("Open Potion Database"))
        {
            PotionDatabaseWindow.ShowWindow();
        }

        // Button to open the Weapon Database window
        if (GUILayout.Button("Open Weapon Database"))
        {
            WeaponDatabaseWindow.ShowWindow();
        }

        // Button to open the Weapon Creation window
        if (GUILayout.Button("Create New Weapon"))
        {
            WeaponCreation.ShowWindow();
        }
    }
}
