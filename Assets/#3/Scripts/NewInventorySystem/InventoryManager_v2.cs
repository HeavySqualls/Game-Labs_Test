using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager_v2 : MonoBehaviour
{
    [SerializeField] Inventory_v2 inventory;
    [SerializeField] EquipmentPanel_v2 equipmentPanel;

    private void Awake()
    {
        //SUBCSCRIBE TO EVENTS

            // If the event signal comes from the inventory, equip the item
        inventory.OnItemRightClickedEvent += EquipFromInventory;

            // If the event signal comes from the Equipment Panel, unequip the item
        equipmentPanel.OnItemRightClickedEvent += UnequipFromEquipPanel;
    }

    // As items from the inventory can be of any type, we must first check to ensure that the item we wish to 
    // equip is indeed an equippable item, then equip it if so
    private void EquipFromInventory(sItem item)
    {
        if (item is sEquipment)
        {
            Equip((sEquipment)item);
        }
    }

    private void UnequipFromEquipPanel(sItem item)
    {
        if (item is sEquipment)
        {
            Unequip((sEquipment)item);
        }
    }

    public void Equip(sEquipment item)
    {
        if (inventory.RemoveItem(item))
        {
            sEquipment previousItem;

            // if there was an item already inside the desired equipment slot...
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    // ...add that item back to the inventory
                    inventory.AddItem(previousItem);
                }
            }
            // if we could not equip the current item, return it to the inventory as well 
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void Unequip(sEquipment item)
    {
        // make sure the inventory is not full, then remove the item from the equipment panel...
        if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            // ...and add it to the inventory
            inventory.AddItem(item);
        }
    }
}
