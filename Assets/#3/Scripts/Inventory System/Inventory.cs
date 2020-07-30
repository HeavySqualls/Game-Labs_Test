using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Module Items
    [Header("MODUELS:")]
    [SerializeField] List<sItem> startingModuleItems;
    [SerializeField] Transform moduleItemsParent;
    [SerializeField] ItemSlot[] moduleItemSlots;


    [Header("WEAPONS:")]
    [Space]
    [Space]

    // Weapon Items
    [SerializeField] List<sItem> startingWeaponItems;
    [SerializeField] Transform weaponsItemsParent;
    [SerializeField] ItemSlot[] weaponsItemSlots;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;

    public event Action<ItemSlot> OnModuleItemRightClickEvent;
    public event Action<ItemSlot> OnWeaponItemRightClickEvent;
    //public event Action<ItemSlot> OnRightClickEvent;

    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private void Start()
    {
        for (int i = 0; i < moduleItemSlots.Length; i++)
        {
            moduleItemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            moduleItemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            moduleItemSlots[i].OnRightClickEvent += OnModuleItemRightClickEvent;
            moduleItemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            moduleItemSlots[i].OnEndDragEvent += OnEndDragEvent;
            moduleItemSlots[i].OnDragEvent += OnDragEvent;
            moduleItemSlots[i].OnDropEvent += OnDropEvent;
        }

        for (int i = 0; i < weaponsItemSlots.Length; i++)
        {
            weaponsItemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            weaponsItemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            weaponsItemSlots[i].OnRightClickEvent += OnWeaponItemRightClickEvent;
            weaponsItemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            weaponsItemSlots[i].OnEndDragEvent += OnEndDragEvent;
            weaponsItemSlots[i].OnDragEvent += OnDragEvent;
            weaponsItemSlots[i].OnDropEvent += OnDropEvent;
        }

        SetStartingItems();
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

        SetStartingItems();
    }

    // refresh equipment list UI every time there is a change to it
    private void SetStartingItems()
    {
        // refresh module UI
        int i = 0;

        for (; i < startingModuleItems.Count && i < moduleItemSlots.Length; i++)
        {
            moduleItemSlots[i].item = Instantiate(startingModuleItems[i]);
        }

        for (; i < moduleItemSlots.Length; i++)
        {
            moduleItemSlots[i].item = null;
        }

        // refresh weapons UI
        int y = 0;

        for (; y < startingWeaponItems.Count && y < weaponsItemSlots.Length; y++)
        {
            weaponsItemSlots[y].item = Instantiate(startingWeaponItems[y]);
        }

        for (; y < weaponsItemSlots.Length; y++)
        {
            weaponsItemSlots[y].item = null;
        }
    }

    public bool AddItem(sItem _item)
    {
        for (int i = 0; i < moduleItemSlots.Length; i++)
        {
            if (moduleItemSlots[i].item == null)
            {
                moduleItemSlots[i].item = _item;
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(sItem _item)
    {
        for (int i = 0; i < moduleItemSlots.Length; i++)
        {
            if (moduleItemSlots[i].item == _item)
            {
                moduleItemSlots[i].item = null;
                return true;
            }
        }
        return false;
    }

    public bool IsFull()
    {
        for (int i = 0; i < moduleItemSlots.Length; i++)
        {
            if (moduleItemSlots[i].item == null)
            {
                return false;
            }
        }
        return true;
    }
}
