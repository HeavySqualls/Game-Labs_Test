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
}
