using System.Collections.Generic;
using UnityEngine;

// Assuming Weapon class is defined somewhere as a subclass of Item
public class Weapon : Item
{
    public int damage;
    public float range;
    // Add other weapon-specific properties here
}

// Base class for managing item databases
public abstract class ItemDatabase<T> : ScriptableObject where T : Item
{
    public List<T> items = new List<T>();

    // Methods for adding, removing, and finding items by name are defined here
    // ...
}

// Specific database for managing Weapon items
public class WeaponDatabase : ItemDatabase<Weapon>
{
    // Specific functionality for WeaponDatabase
    // Example: A method to find the most powerful weapon
    public Weapon FindMostPowerfulWeapon()
    {
        Weapon mostPowerful = null;
        foreach (var weapon in items)
        {
            if (mostPowerful == null || weapon.damage > mostPowerful.damage)
            {
                mostPowerful = weapon;
            }
        }
        return mostPowerful;
    }

    // You can add more methods specific to WeaponDatabase here
}
