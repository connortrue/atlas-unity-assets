using UnityEditor;
using UnityEngine;

public class WeaponCreation : BaseItemCreation<Weapon>
{
    [MenuItem("Window/Item Manager/Create New Weapon")]
    public static void ShowWindow()
    {
        GetWindow<WeaponCreation>("Create New Weapon");
    }

    void onGUI()
    {
        DrawCommonFields();
    }

    protected void DrawCommonFields()
    {
        base.DrawCommonFields();

        // Additional weapon-specific properties
        weaponType = (WeaponType)EditorGUILayout.EnumPopup("Weapon Type:", weaponType);
        attackPower = EditorGUILayout.FloatField("Attack Power:", attackPower);
        attackSpeed = EditorGUILayout.FloatField("Attack Speed:", attackSpeed);
        durability = EditorGUILayout.FloatField("Durability:", durability);
        range = EditorGUILayout.FloatField("Range:", range);
        criticalHitChance = EditorGUILayout.FloatField("Critical Hit Chance:", criticalHitChance);
    }

    protected void CreateItem()
    {
        if (string.IsNullOrEmpty(itemName))
        {
            EditorUtility.DisplayDialog("Error", "Item name cannot be empty.", "OK");
            return;
        }

        // Check if a weapon with the same name already exists
        var existingWeapon = AssetDatabase.LoadAssetAtPath<Weapon>($"Assets/Items/Weapons/{itemName}.asset");
        if (existingWeapon != null)
        {
            EditorUtility.DisplayDialog("Error", $"A weapon with the name '{itemName}' already exists.", "OK");
            return;
        }

        base.CreateItem<Weapon>();

        // Create the new weapon asset
        Weapon newWeapon = CreateInstance<Weapon>();
        newWeapon.itemName = itemName;
        newWeapon.description = description;
        newWeapon.weaponType = weaponType;
        newWeapon.attackPower = attackPower;
        newWeapon.attackSpeed = attackSpeed;
        newWeapon.durability = durability;
        newWeapon.range = range;
        newWeapon.criticalHitChance = criticalHitChance;

        // Save the new weapon as an asset
        string assetPath = $"Assets/Items/Weapons/{itemName}.asset";
        AssetDatabase.CreateAsset(newWeapon, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        // Clear the window
        itemName = "";
        description = "";
        weaponType = WeaponType.None;
        attackPower = 0;
        attackSpeed = 0;
        durability = 0;
        range = 0;
        criticalHitChance = 0;
    }

    // Additional weapon-specific properties
    private WeaponType weaponType;
    private float attackPower;
    private float attackSpeed;
    private float durability;
    private float range;
    private float criticalHitChance;
}
