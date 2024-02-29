using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class WeaponDatabase : ItemDatabase<Weapon>
{
    [MenuItem("Window/Item Manager/Weapon Database")]
    public static void ShowWindow()
    {
        GetWindow<WeaponDatabase>("Weapon Database");
    }

    protected void OnGUI()
    {
        base.OnGUI();

        // Button to open the Weapon Creation window
        if (GUILayout.Button("Create New Weapon"))
        {
            WeaponCreation.ShowWindow();
        }
    }


    protected override void DrawItemList()
    {
        foreach (var weapon in FindAssets<Weapon>())
        {
            if (GUILayout.Button(weapon.itemName))
            {
                selectedItem = weapon;
            }
        }
    }

    protected override void DrawPropertiesSection()
    {
        // Draw properties of the selected Weapon item
        if (selectedItem != null)
        {
            EditorGUILayout.LabelField("Item Name:", selectedItem.itemName);
            EditorGUILayout.LabelField("Description:", selectedItem.description);
            EditorGUILayout.EnumPopup("Weapon Type:", ((Weapon)selectedItem).weaponType);
            EditorGUILayout.FloatField("Attack Power:", ((Weapon)selectedItem).attackPower);
            EditorGUILayout.FloatField("Attack Speed:", ((Weapon)selectedItem).attackSpeed);
            EditorGUILayout.FloatField("Durability:", ((Weapon)selectedItem).durability);
            EditorGUILayout.FloatField("Range:", ((Weapon)selectedItem).range);
            EditorGUILayout.FloatField("Critical Hit Chance:", ((Weapon)selectedItem).criticalHitChance);
        }
    }

    protected override void ExportItemsToCSV()
    {
        var items = FindAssets<Weapon>();
        if (items.Count > 0)
        {
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("Name,Description,WeaponType,AttackPower,AttackSpeed,Durability,Range,CriticalHitChance");
            foreach (var item in items)
            {
                csv.AppendLine($"{item.itemName},{item.description},{item.weaponType},{item.attackPower},{item.attackSpeed},{item.durability},{item.range},{item.criticalHitChance}");
            }
            string path = EditorUtility.SaveFilePanel("Export Weapons to CSV", "", "weapon_export", "csv");
            if (!string.IsNullOrEmpty(path))
            {
                File.WriteAllText(path, csv.ToString());
            }
        }
    }

    protected override void ImportItemsFromCSV()
    {
        string path = EditorUtility.OpenFilePanel("Import Weapons from CSV", "", "csv");
        if (!string.IsNullOrEmpty(path))
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 1; i < lines.Length; i++) // Skip header
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length >= 8)
                {
                    Weapon newWeapon = CreateInstance<Weapon>();
                    newWeapon.itemName = parts[0];
                    newWeapon.description = parts[1];
                    newWeapon.weaponType = (WeaponType)Enum.Parse(typeof(WeaponType), parts[2]);
                    newWeapon.attackPower = float.Parse(parts[3]);
                    newWeapon.attackSpeed = float.Parse(parts[4]);
                    newWeapon.durability = float.Parse(parts[5]);
                    newWeapon.range = float.Parse(parts[6]);
                    newWeapon.criticalHitChance = float.Parse(parts[7]);
                    // Save the new item as an asset
                    string assetPath = "Assets/Items/Weapons/" + newWeapon.itemName + ".asset";
                    AssetDatabase.CreateAsset(newWeapon, assetPath);
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
