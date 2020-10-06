using System.Collections;
using System;
using UnityEngine;

public class EquipmentPanel_v2 : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentSlot_v2[] equipmentSlots;

    public event Action<ItemSlot_v2> OnPointerEnterEvent;
    public event Action<ItemSlot_v2> OnPointerExitEvent;
    public event Action<ItemSlot_v2> OnRightClickEvent;
    public event Action<ItemSlot_v2> OnBeginDragEvent;
    public event Action<ItemSlot_v2> OnEndDragEvent;
    public event Action<ItemSlot_v2> OnDragEvent;
    public event Action<ItemSlot_v2> OnDropEvent;

    public int currentWeaponSlot;
    public int currentModuleSlot;

    private void Start()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            // Adds a listener to the slots' OnRightClickEvent to subscribe to the equipment panels OnItemRightClickedEvent 
            equipmentSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            equipmentSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            equipmentSlots[i].OnRightClickEvent += OnRightClickEvent;
            equipmentSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            equipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
            equipmentSlots[i].OnDragEvent += OnDragEvent;
            equipmentSlots[i].OnDropEvent += OnDropEvent;
        }
    }

    private void OnValidate()
    {
        if (equipmentSlotsParent != null)
        {
            equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot_v2>();
        }
    }

    // TRACKS THE CURRENT WEAPON AND MODULE SLOTS TO PERFORM ACTIONS ONLY TO THOSE SLOTS 

    // in the case that there is an item already equipped in the slot, we provide an "out" parameter to pass off the old equipment piece
    public bool AddItem(sEquipment item, out sEquipment previousItem) 
    {
        // Module Slots 
        if (item.equipmentType == EquipmentType.Module)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].equipmentType == item.equipmentType && equipmentSlots[i].Item == null)
                {
                    currentModuleSlot = i;
                    print("CURRENT MODULE SLOT: " + i);
                    previousItem = (sEquipment)equipmentSlots[i].Item;
                    equipmentSlots[i].Item = item;
                    return true;
                }
            }
        }

        // Weapon Slots 
        else if (item.equipmentType == EquipmentType.Weapon)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].equipmentType == item.equipmentType && equipmentSlots[i].Item == null)
                {
                    currentWeaponSlot = i;
                    print("CURRENT WEAPON SLOT: " + i);
                    previousItem = (sEquipment)equipmentSlots[i].Item;
                    equipmentSlots[i].Item = item;
                    return true;
                }
            }
        }

        previousItem = null;
        return false; // else, return false. This item can not be added to any equipment slots
    }

    public bool RemoveItem(sEquipment item)
    {
        //Module Slots 
        if (item.equipmentType == EquipmentType.Module)
        {
            for (int i = 0; i < equipmentSlots.Length; i++) // loop through all the equipment slots 
            {
                if (equipmentSlots[i].Item == item) // look for the item, instead of the equipment type
                {
                    currentWeaponSlot = i;
                    print("CURRENT MODULE SLOT: " + i);
                    equipmentSlots[i].Item = null; // assign null to the slot, removing the item
                    return true;
                }
            }
        }
        // Weapons
        else if (item.equipmentType == EquipmentType.Weapon)
        {
            for (int i = 0; i < equipmentSlots.Length; i++) // loop through all the equipment slots 
            {
                if (equipmentSlots[i].Item == item) // look for the item, instead of the equipment type
                {
                    currentWeaponSlot = i;
                    print("CURRENT WEAPON SLOT: " + i);
                    equipmentSlots[i].Item = null; // assign null to the slot, removing the item
                    return true;
                }
            }
        }

        return false; // else, return false. This item can not be added to any equipment slots
    }
}
