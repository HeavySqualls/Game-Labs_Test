using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_v2 : MonoBehaviour
{
    [SerializeField] List<sItem> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot_v2[] itemSlots;

    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot_v2>();
        }

        RefreshUI();
    }

    private void RefreshUI() // calls every time a change happens to the inventory
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++) // for every item we have, we'll assign it to an item slot
        {
            itemSlots[i].Item = items[i];
        }

        for (; i < itemSlots.Length; i++) // for every spot that does not have an item, set it to null
        {
            itemSlots[i].Item = null;
        }
    }

    public bool AddItem(sItem item) // public method for adding items to the inventory
    {
        if (IsFull())
        {
            return false;
        }

        items.Add(item);
        RefreshUI();
        return true;
    }

    public bool RemoveItem(sItem item)
    {
        if (items.Remove(item)) // if we are able to remove the item from the list, refresh UI and return true
        {
            RefreshUI();
            return true;
        }
        return false;
    }

    public bool IsFull() // checks if the current item count is equal to, or greater than the number of possible item slots
    {
        return items.Count >= itemSlots.Length;
    }
}
