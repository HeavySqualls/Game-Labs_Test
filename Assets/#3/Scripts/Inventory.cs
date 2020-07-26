using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Module Items
    [Header("MODUELS:")]

    [SerializeField] List<sItem> moduleItems;
    [SerializeField] Transform moduleItemsParent;
    [SerializeField] ItemSlot[] moduleItemSlots;


    [Header("WEAPONS:")]
    [Space]
    [Space]

    // Weapon Items
    [SerializeField] List<sItem> weaponItems;
    [SerializeField] Transform weaponsItemsParent;
    [SerializeField] ItemSlot[] weaponsItemSlots;

    public event Action<sItem> OnModuleItemRightClickEvent;
    public event Action<sItem> OnWeaponItemRightClickEvent;

    private void Awake()
    {
        for (int i = 0; i < moduleItemSlots.Length; i++)
        {
            moduleItemSlots[i].OnRightClickEvent += OnModuleItemRightClickEvent;
        }

        for (int i = 0; i < weaponsItemSlots.Length; i++)
        {
            weaponsItemSlots[i].OnRightClickEvent += OnWeaponItemRightClickEvent;
        }
    }

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

        for (; i < moduleItems.Count && i < moduleItemSlots.Length; i++)
        {
            moduleItemSlots[i].item = moduleItems[i];
        }

        for (; i < moduleItemSlots.Length; i++)
        {
            moduleItemSlots[i].item = null;
        }

        // refresh weapons UI
        int y = 0;

        for (; y < weaponItems.Count && y < weaponsItemSlots.Length; y++)
        {
            weaponsItemSlots[y].item = weaponItems[y];
        }

        for (; y < weaponsItemSlots.Length; y++)
        {
            weaponsItemSlots[y].item = null;
        }
    }

    public bool AddItem(sItem _item)
    {
        if (IsFull())
        {
            return false;
        }

        moduleItems.Add(_item);
        RefreshUI();
        return true;
    }

    public bool RemoveItem(sItem _item)
    {
        if (moduleItems.Remove(_item))
        {
            RefreshUI();
            return true;
        }

        return false;
    }

    public bool IsFull()
    {
        return moduleItems.Count >= moduleItemSlots.Length;
    }
}
