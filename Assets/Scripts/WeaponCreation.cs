using UnityEditor;
using UnityEngine;

public class WeaponCreation : EditorWindow
{
    private string weaponName;
    private int damage;
    private float range;

    [MenuItem("Window/Weapon Creation")]
    public static void ShowWindow()
    {
        GetWindow<WeaponCreation>("Weapon Creation");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create New Weapon", EditorStyles.boldLabel);

        weaponName = EditorGUILayout.TextField("Weapon Name", weaponName);
        damage = EditorGUILayout.IntField("Damage", damage);
        range = EditorGUILayout.FloatField("Range", range);

        if (GUILayout.Button("Create Weapon"))
        {
            CreateWeaponAsset();
        }
    }

    private void CreateWeaponAsset()
    {
        Weapon weapon = ScriptableObject.CreateInstance<Weapon>();
        weapon.weaponName = weaponName;
        weapon.damage = damage;
        weapon.range = range;

        AssetDatabase.CreateAsset(weapon, $"Assets/Items/Weapons/{weaponName}.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = weapon;
    }
}
