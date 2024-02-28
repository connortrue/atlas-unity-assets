using System.Collections.Generic;
using UnityEngine;

// Base class for managing item databases
public abstract class ItemDatabase<T> : ScriptableObject where T : Item
{
    // List to hold all items of type T
    public List<T> items = new List<T>();

    // Function to add a new item to the database
    public void AddItem(T item)
    {
        // Check if the item is not null and does not already exist in the database
        if (item != null && !items.Contains(item))
        {
            items.Add(item);
        }
    }

    // Function to remove an item from the database
    public void RemoveItem(T item)
    {
        // Check if the item exists in the database
        if (items.Contains(item))
        {
            items.Remove(item);
        }
    }

    // Function to find an item by name
    public T FindItemByName(string name)
    {
        // Iterate through the items list to find an item with the specified name
        foreach (T item in items)
        {
            if (item.name == name)
            {
                return item;
            }
        }

        // Return null if no item with the specified name is found
        return null;
    }
}
