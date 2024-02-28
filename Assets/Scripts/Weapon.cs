using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Items/Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public int damage;
    public float range;
}
