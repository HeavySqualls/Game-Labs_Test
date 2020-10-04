using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kryz.CharacterStats;

public class InventoryManager_v2 : MonoBehaviour
{
    public CharacterStat HP;
    public CharacterStat Shield;
    public CharacterStat ShieldRegen;
    public CharacterStat ReloadSpeed;

    public CharacterStat GunA_Damage;
    public CharacterStat GunA_Reload;

    public CharacterStat GunB_Damage;
    public CharacterStat GunB_Reload;

    [SerializeField] Inventory_v2 inventory;
    [SerializeField] EquipmentPanel_v2 equipmentPanel;
    [SerializeField] StatPanel statPanel;

    private void Awake()
    {
        statPanel.SetStats(HP, Shield, ShieldRegen, GunA_Damage, GunA_Reload, GunB_Damage, GunB_Reload);
        statPanel.UpdateStatValues();

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
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
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
            item.Unequip(this);
            statPanel.UpdateStatValues();
        }
    }
}
