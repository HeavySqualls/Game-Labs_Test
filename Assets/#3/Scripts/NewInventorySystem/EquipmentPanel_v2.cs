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

    //TODO: Add Item is where I need to come up with a way that checks, if the first equip type slot is full, check for another empty one, 
    //     and if there are no others, then replace the first one. 

    // in the case that there is an item already equipped in the slot, we provide an "out" parameter to pass off the old equipment piece
    public bool AddItem(sEquipment item, out sEquipment previousItem) 
    {
        for (int i = 0; i < equipmentSlots.Length; i++) // loop through all the equipment slots 
        {
            if (equipmentSlots[i].equipmentType == item.equipmentType) // when we find a slot that is the same as the equipment type we are trying to equip
            {
                previousItem = (sEquipment)equipmentSlots[i].Item;
                equipmentSlots[i].Item = item; // add the item to that slot, and return true
                return true;
            }
        }

        previousItem = null;
        return false; // else, return false. This item can not be added to any equipment slots
    }

    public bool RemoveItem(sEquipment item)
    {
        for (int i = 0; i < equipmentSlots.Length; i++) // loop through all the equipment slots 
        {
            if (equipmentSlots[i].Item == item) // look for the item, instead of the equipment type
            {
                equipmentSlots[i].Item = null; // assign null to the slot, removing the item
                return true;
            }
        }
        return false; // else, return false. This item can not be added to any equipment slots
    }
}
