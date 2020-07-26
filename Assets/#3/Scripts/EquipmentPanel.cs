using System;
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
    [SerializeField] EquipmentSlot[] weaponEquipmentSlots;

    public event Action<sItem> OnModuleItemRightClickEvent;
    public event Action<sItem> OnWeaponItemRightClickEvent;

    private void Awake()
    {
        for (int i = 0; i < moduleEquipmentSlots.Length; i++)
        {
            moduleEquipmentSlots[i].OnRightClickEvent += OnModuleItemRightClickEvent;
        }

        for (int i = 0; i < weaponEquipmentSlots.Length; i++)
        {
            weaponEquipmentSlots[i].OnRightClickEvent += OnWeaponItemRightClickEvent;
        }
    }

    private void OnValidate()
    {
        moduleEquipmentSlots = moduleEquipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
        weaponEquipmentSlots = weaponEquipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public bool AddItem(sEquipment _item, out sEquipment previousItem)
    {
        // MODULE SLOTS
        if (_item.equipmentType == EquipmentType.Module)
        {
            for (int i = 0; i < moduleEquipmentSlots.Length; i++)
            {
                if (moduleEquipmentSlots[i].equipmentType == _item.equipmentType && moduleEquipmentSlots[i].item == null)
                {
                    previousItem = null;
                    moduleEquipmentSlots[i].item = _item;
                    return true;
                }
                else if (moduleEquipmentSlots[i].equipmentType == _item.equipmentType && moduleEquipmentSlots[i].item != null)
                {
                    previousItem = (sEquipment)moduleEquipmentSlots[i++].item; previousItem = null;
                    moduleEquipmentSlots[i++].item = _item;
                    return true;
                }
            }
        }

        // WEAPON SLOTS 
        else if (_item.equipmentType == EquipmentType.Weapon)
        {
            for (int i = 0; i < weaponEquipmentSlots.Length; i++)
            {
                if (weaponEquipmentSlots[i].equipmentType == _item.equipmentType && weaponEquipmentSlots[i].item == null)
                {
                    previousItem = null;
                    weaponEquipmentSlots[i].item = _item;
                    return true;
                }
                else if (weaponEquipmentSlots[i].equipmentType == _item.equipmentType && weaponEquipmentSlots[i].item != null)
                {
                    previousItem = (sEquipment)weaponEquipmentSlots[i++].item; previousItem = null;
                    weaponEquipmentSlots[i++].item = _item;
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
                    moduleEquipmentSlots[i].item = null;
                    return true;
                }
            }
        }

        // WEAPON SLOTS
        else if (_item.equipmentType == EquipmentType.Weapon)
        {
            for (int i = 0; i < weaponEquipmentSlots.Length; i++)
            {
                if (weaponEquipmentSlots[i].item == _item)
                {
                    weaponEquipmentSlots[i].item = null;
                    return true;
                }
            }
        }

        return false;
    }
}
