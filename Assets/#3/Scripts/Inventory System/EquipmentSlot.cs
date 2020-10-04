
public class EquipmentSlot : ItemSlot
{
    public EquipmentType equipmentType;

    protected override void OnValidate()
    {
        base.OnValidate();

        // Automatically name the slot
        gameObject.name = equipmentType.ToString() + " Slot";
    }

    public override bool CanRecieveItem(sItem Item)
    {
        if (Item == null)
        {
            return true;
        }

        sEquipment equippableItem = Item as sEquipment;
        return equippableItem != null && equippableItem.equipmentType == equipmentType;
    }
}
   
