using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot_v2 : ItemSlot_v2
{
    public EquipmentType equipmentType;

    protected override void OnValidate()
    {
        base.OnValidate();

        // Automatically name the slot
        gameObject.name = equipmentType.ToString() + " Slot";
    }

    public override bool CanRecieveItem(sItem item)
    {
        // if the slot is empty, then we can recieve the item 
        if (this.Item == null)
        {
            return true;
        }

        // otherwise, if it is being used, check if the new item is of the proper type before 
        // accepting the new item
        sEquipment equippableItem = item as sEquipment;
        return equippableItem != null && equippableItem.equipmentType == equipmentType;
    }
}
