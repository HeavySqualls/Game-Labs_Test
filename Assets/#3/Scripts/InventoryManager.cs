using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;

    private void Awake()
    {
        inventory.OnModuleItemRightClickEvent += EquipFromInventory;
        inventory.OnWeaponItemRightClickEvent += EquipFromInventory;

        equipmentPanel.OnModuleItemRightClickEvent += UnequipFromEquipPanel;
        equipmentPanel.OnWeaponItemRightClickEvent += UnequipFromEquipPanel;
    }

    private void EquipFromInventory(sItem _item)
    {
        if (_item is sEquipment)
        {
            Equip((sEquipment)_item);
        }
    }

    private void UnequipFromEquipPanel(sItem _item)
    {
        if (_item is sEquipment)
        {
            UnEquip((sEquipment)_item);
        }
    }

    public void Equip(sEquipment _item)
    {
        sEquipment previousItem;
        if (equipmentPanel.AddItem(_item, out previousItem))
        {
            if (previousItem != null)
            {
                //TODO: potentially just destroy the item here instead
                inventory.AddItem(previousItem);
            }
        }
        else
        {
            inventory.AddItem(_item);
        }

        // TOOK OUT AS WE DONT WANT TO REMOVE ITEMS FROM THE INVENTORY PANEL
        //if (inventory.RemoveItem(_item))
        //{
        //    sEquipment previousItem;
        //    if (equipmentPanel.AddItem(_item, out previousItem))
        //    {
        //        if (previousItem != null)
        //        {
        //            //TODO: potentially just destroy the item here instead
        //            inventory.AddItem(previousItem);
        //        }
        //    }
        //    else
        //    {
        //        inventory.AddItem(_item);
        //    }
        //}
    }

    public void UnEquip(sEquipment _item)
    {
        equipmentPanel.RemoveItem(_item);
        //if (!inventory.IsFull() && equipmentPanel.RemoveItem(_item))
        //{
        //    inventory.AddItem(_item);
        //}
    }
}
