using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;

    private void OnValidate()
    {
        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public bool AddItem(sEquipment _item, out sEquipment previousItem)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].equipmentType == _item.equipmentType && equipmentSlots[i].item == null)
            {
                previousItem = null;
                equipmentSlots[i].item = _item;
                return true;
            }
            else if (equipmentSlots[i].equipmentType == _item.equipmentType && equipmentSlots[i].item != null)
            {
                previousItem = (sEquipment)equipmentSlots[i++].item; previousItem = null;
                equipmentSlots[i++].item = _item;
                return true;
            }
        }
        previousItem = null;
        return false;
    }

    public bool RemoveItem(sEquipment _item)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].item == _item)
            {
                equipmentSlots[i].item = null;
                return true;
            }
        }
        return false;
    }
}
