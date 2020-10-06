using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The player is able to view the tooltips and equip the item in to their inventory 

public class LootItemManager : MonoBehaviour
{
    [SerializeField] Inventory_v2 inventory;
    [SerializeField] Inventory_v2 playersInventory;

    [SerializeField] ItemTooltip itemTooltip;

    private void Awake()
    {
        // Right Click Events
        inventory.OnRightClickEvent += TakeItem;
        // Pointer Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        // Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
    }

    // ------------------------------------------------------------------------- EQUIP / UNEQUIP ----- //
    private void TakeItem(ItemSlot_v2 itemSlot)
    {
        if (!playersInventory.IsFull())
        {
            playersInventory.AddItem(itemSlot.Item);
            inventory.RemoveItem(itemSlot.Item);
        }
        else
        {
            Debug.LogError("Players inventory is full!");
        }
    }

    // ------------------------------------------------------------------------- TOOLTIPS ----- //
    private void ShowTooltip(ItemSlot_v2 itemSlot)
    {
        sEquipment equippableItem = itemSlot.Item as sEquipment;
        if (equippableItem != null)
        {
            itemTooltip.ShowTooltip(equippableItem);
        }
    }

    private void HideTooltip(ItemSlot_v2 itemSlot)
    {
        itemTooltip.HideTooltip();
    }
}

