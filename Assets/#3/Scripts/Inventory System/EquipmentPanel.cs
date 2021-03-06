﻿using System;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [Space]
    [Header("MODULE EQUIPMENT SLOTS:")]
    [SerializeField] Transform moduleEquipmentSlotsParent;
    [SerializeField] EquipmentSlot[] moduleEquipmentSlots;
    [Space]
    [Header("WEAPON EQUIPMENT SLOTS:")]
    [Space]
    [Space]
    [SerializeField] Transform weaponEquipmentSlotsParent;
    public EquipmentSlot[] weaponEquipmentSlots;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;

    public event Action<ItemSlot> OnModuleItemRightClickEvent;
    public event Action<ItemSlot> OnWeaponItemRightClickEvent;

    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private void Start()
    {
        for (int i = 0; i < moduleEquipmentSlots.Length; i++)
        {
            moduleEquipmentSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            moduleEquipmentSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            moduleEquipmentSlots[i].OnRightClickEvent += OnModuleItemRightClickEvent;
            moduleEquipmentSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            moduleEquipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
            moduleEquipmentSlots[i].OnDragEvent += OnDragEvent;
            moduleEquipmentSlots[i].OnDropEvent += OnDropEvent;
        }

        for (int i = 0; i < weaponEquipmentSlots.Length; i++)
        {
            weaponEquipmentSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            weaponEquipmentSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            weaponEquipmentSlots[i].OnRightClickEvent += OnWeaponItemRightClickEvent;
            weaponEquipmentSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            weaponEquipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
            weaponEquipmentSlots[i].OnDragEvent += OnDragEvent;
            weaponEquipmentSlots[i].OnDropEvent += OnDropEvent;
        }
    }

    private void OnValidate()
    {
        moduleEquipmentSlots = moduleEquipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
        weaponEquipmentSlots = weaponEquipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public int currentModuleSlot;
    public int currentWeaponEquipSlot;

    public bool AddItem(sEquipment _item, out sEquipment previousItem)
    {
        // MODULE SLOTS
        if (_item.equipmentType == EquipmentType.Module)
        {
            for (int i = 0; i < moduleEquipmentSlots.Length; i++)
            {
                if (moduleEquipmentSlots[i].equipmentType == _item.equipmentType && moduleEquipmentSlots[i].item == null)
                {
                    currentModuleSlot = i;
                    previousItem = (sEquipment)moduleEquipmentSlots[i].item;
                    moduleEquipmentSlots[i].item = _item;
                    return true;
                }
            }
        }

        // WEAPON SLOTS 
        else if (_item.equipmentType == EquipmentType.Weapon)
        {
            for (int y = 0; y < weaponEquipmentSlots.Length; y++)
            {
                if (weaponEquipmentSlots[y].equipmentType == _item.equipmentType && weaponEquipmentSlots[y].item == null)
                {
                    Debug.Log("Equipped ");
                    currentWeaponEquipSlot = y;
                    previousItem = (sEquipment)weaponEquipmentSlots[y].item;
                    weaponEquipmentSlots[y].item = _item;
                    return true;
                }
            }
        }

        previousItem = null;
        return false;
    }

    public bool RemoveItem(sEquipment _item)
    {
        // MODULE SLOTS
        if (_item.equipmentType == EquipmentType.Module)
        {
            for (int i = 0; i < moduleEquipmentSlots.Length; i++)
            {
                if (moduleEquipmentSlots[i].item == _item)
                {
                    currentModuleSlot = i;
                    moduleEquipmentSlots[i].item = null;
                    return true;
                }
            }
        }

        // WEAPON SLOTS
        else if (_item.equipmentType == EquipmentType.Weapon)
        {
            for (int y = 0; y < weaponEquipmentSlots.Length; y++)
            {
                if (weaponEquipmentSlots[y].item == _item)
                {
                    currentWeaponEquipSlot = y;
                    weaponEquipmentSlots[y].item = null;
                    return true;
                }
            }
        }

        return false;
    }
}
