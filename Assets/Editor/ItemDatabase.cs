using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class ItemDatabase<T> : EditorWindow where T : class
{
    /*[MenuItem("Window/Item Manager/Item Database")]
    public static void ShowWindow()
    {
        GetWindow<ItemDatabase>("Item Database");
    }*/

    protected Vector2 scrollPosition;
    protected T selectedItem;
    protected float propertiesSectionWidth = 400f;
    
    public ItemDatabase()
    {
        this.minSize = new Vector2(600, 400); 
    }

    protected abstract void DrawItemList();
    protected abstract void DrawPropertiesSection();

    protected virtual void OnGUI()
    {
        GUILayout.BeginHorizontal();
        // Button to open the Weapon Creation window
        if (GUILayout.Button("Create New Weapon"))
        {
            WeaponCreation.ShowWindow();
        }
        DrawItemList();
        
        GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(propertiesSectionWidth));
        DrawPropertiesSection();
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }

    protected void DeleteSelectedItem()
    {
        //Delete item
    }

    protected void DuplicateSelectedItem()
    {
        //Duplicate item
    }

    protected abstract void ExportItemsToCSV();

    protected abstract void ImportItemsFromCSV();

    protected void DrawTopLeftOptions()
    {
        GUILayout.Label("Database Admin Functions:", EditorStyles.boldLabel);
        if (GUILayout.Button("Export to CSV")) ExportItemsToCSV();
        if (GUILayout.Button("Import from CSV")) ImportItemsFromCSV();
        if (GUILayout.Button("Delete Selected Item")) DeleteSelectedItem();
        if (GUILayout.Button("Duplicate Selected Item")) DuplicateSelectedItem();
    }
}
