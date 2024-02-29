using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class PotionDatabase : ItemDatabase<Potion>
{
    [MenuItem("Window/Item Manager/Potion Database")]
    public static void ShowWindow()
    {
        GetWindow<PotionDatabase>("Potion Database");
    }

    protected override void DrawItemList()
    {
        foreach (var potion in FindAssets<Potion>())
        {
            if (GUILayout.Button(potion.itemName))
            {
                selectedItem = potion;
            }
        }
    }

    protected override void DrawPropertiesSection()
    {
        // Draw properties of the selected Potion item
        if (selectedItem != null)
        {
            EditorGUILayout.LabelField("Item Name:", selectedItem.itemName);
            EditorGUILayout.LabelField("Description:", selectedItem.description);
            EditorGUILayout.EnumPopup("Potion Effect:", ((Potion)selectedItem).potionEffect);
            EditorGUILayout.FloatField("Effect Power:", ((Potion)selectedItem).effectPower);
            EditorGUILayout.FloatField("Duration:", ((Potion)selectedItem).duration);
            EditorGUILayout.FloatField("Cooldown:", ((Potion)selectedItem).cooldown);
            EditorGUILayout.Toggle("Is Stackable:", ((Potion)selectedItem).isStackable);
        }
    }

    protected override void ExportItemsToCSV()
    {
        var items = FindAssets<Potion>();
        if (items.Count > 0)
        {
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("Name,Description,PotionEffect,EffectPower,Duration,Cooldown,IsStackable");
            foreach (var item in items)
            {
                csv.AppendLine($"{item.itemName},{item.description},{item.potionEffect},{item.effectPower},{item.duration},{item.cooldown},{item.isStackable}");
            }
            string path = EditorUtility.SaveFilePanel("Export Potions to CSV", "", "potion_export", "csv");
            if (!string.IsNullOrEmpty(path))
            {
                File.WriteAllText(path, csv.ToString());
            }
        }
    }

    protected override void ImportItemsFromCSV()
    {
        string path = EditorUtility.OpenFilePanel("Import Potions from CSV", "", "csv");
        if (!string.IsNullOrEmpty(path))
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 1; i < lines.Length; i++) // Skip header
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length >= 7)
                {
                    Potion newPotion = CreateInstance<Potion>();
                    newPotion.itemName = parts[0];
                    newPotion.description = parts[1];
                    newPotion.potionEffect = (PotionEffect)Enum.Parse(typeof(PotionEffect), parts[2]);
                    newPotion.effectPower = float.Parse(parts[3]);
                    newPotion.duration = float.Parse(parts[4]);
                    newPotion.cooldown = float.Parse(parts[5]);
                    newPotion.isStackable = bool.Parse(parts[6]);
                    // Save the new item as an asset
                    string assetPath = "Assets/Items/Potions/" + newPotion.itemName + ".asset";
                    AssetDatabase.CreateAsset(newPotion, assetPath);
                }
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private List<T> FindAssets<T>() where T : UnityEngine.Object
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
