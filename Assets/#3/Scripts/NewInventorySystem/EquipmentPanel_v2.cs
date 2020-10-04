using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel_v2 : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;

    private void OnValidate()
    {
        if (equipmentSlotsParent != null)
        {
            equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
        }
    }

    //public bool AddItem(sEquipment item)
    //{
    //    for (int i = 0; i < equipmentSlots.Length; i++) // loop through all the equipment slots 
    //    {
    //        if (equipmentSlots[i].equipmentType == item.equipmentType) // if the slot type is the same as the equipment type we are trying to equip
    //        {
    //            equipmentSlots[i].item = item;
    //        }
    //    }
    //}
}
