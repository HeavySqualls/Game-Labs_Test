using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<sItem> items;
    [SerializeField] Transform moduleItemsParent;
    [SerializeField] ItemSlot[] moduleItemSlots;

    [SerializeField] Transform weaponsItemsParent;
    [SerializeField] ItemSlot[] weaponsItemSlots;

    private void OnValidate()
    {
        if (moduleItemsParent != null)
        {
            moduleItemSlots = moduleItemsParent.GetComponentsInChildren<ItemSlot>();
        }

        if (weaponsItemsParent != null)
        {
            weaponsItemSlots = weaponsItemsParent.GetComponentsInChildren<ItemSlot>();
        }

        RefreshUI();
    }

    // refresh equipment list UI every time there is a change to it
    private void RefreshUI()
    {
        // refresh module UI
        int i = 0;

        for (; i < items.Count && i < moduleItemSlots.Length; i++)
        {
            moduleItemSlots[i].item = items[i];
        }

        for (; i < moduleItemSlots.Length; i++)
        {
            moduleItemSlots[i].item = null;
        }

        // refresh weapons UI
        int y = 0;

        for (; y < items.Count && y < weaponsItemSlots.Length; y++)
        {
            weaponsItemSlots[y].item = items[y];
        }

        for (; y < weaponsItemSlots.Length; y++)
        {
            weaponsItemSlots[y].item = null;
        }

    }
}
