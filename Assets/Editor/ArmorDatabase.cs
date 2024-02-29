using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class ArmorDatabase : ItemDatabase<Armor>
{
    [MenuItem("Window/Item Manager/Armor Database")]
    public static void ShowWindow()
    {
        GetWindow<ArmorDatabase>("Armor Database");
    }

    protected override void DrawItemList()
    {
        foreach (var armor in FindAssets<Armor>())
        {
            if (GUILayout.Button(armor.itemName))
            {
                selectedItem = armor;
            }
        }
    }

    protected override void DrawPropertiesSection()
    {
        // Draw properties of the selected Armor item
        if (selectedItem != null)
        {
            EditorGUILayout.LabelField("Item Name:", selectedItem.itemName);
            EditorGUILayout.LabelField("Description:", selectedItem.description);
            EditorGUILayout.EnumPopup("Armor Type:", ((Armor)selectedItem).armorType);
            EditorGUILayout.FloatField("Defense Power:", ((Armor)selectedItem).defensePower);
            EditorGUILayout.FloatField("Resistance:", ((Armor)selectedItem).resistance);
            EditorGUILayout.FloatField("Weight:", ((Armor)selectedItem).weight);
            EditorGUILayout.FloatField("Movement Speed Modifier:", ((Armor)selectedItem).movementSpeedModifier);
        }
    }

    protected override void ExportItemsToCSV()
    {
        var items = FindAssets<Armor>();
        if (items.Count > 0)
        {
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("Name,Description,Defense");
            foreach (var item in items)
            {
                csv.AppendLine($"{item.itemName},{item.description},{item.defensePower}");
            }
            string path = EditorUtility.SaveFilePanel("Export Armor to CSV", "", "armor_export", "csv");
            if (!string.IsNullOrEmpty(path))
            {
                File.WriteAllText(path, csv.ToString());
            }
        }
    }

    protected override void ImportItemsFromCSV()
    {
        string path = EditorUtility.OpenFilePanel("Import Armor from CSV", "", "csv");
        if (!string.IsNullOrEmpty(path))
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 1; i < lines.Length; i++) // Skip header
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length >= 3)
                {
                    Armor newArmor = CreateInstance<Armor>();
                    newArmor.itemName = parts[0];
                    newArmor.description = parts[1];
                    newArmor.defensePower = int.Parse(parts[2]);
                    // Save the new item as an asset
                    string assetPath = "Assets/Items/Armor/" + newArmor.itemName + ".asset";
                    AssetDatabase.CreateAsset(newArmor, assetPath);
                }
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private List<T> FindAssets<T>() where T : Object
    {
        List<T> items = new List<T>();
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            T item = AssetDatabase.LoadAssetAtPath<T>(path);
            if (item != null)
            {
                items.Add(item);
            }
        }
        return items;
    }
}
