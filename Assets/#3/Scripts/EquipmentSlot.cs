
public class EquipmentSlot : ItemSlot
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
        if (item == null)
        {
            return true;
        }

        sEquipment equippableItem = item as sEquipment;
        return equippableItem != null && equippableItem.equipmentType == equipmentType;
    }
}
   
