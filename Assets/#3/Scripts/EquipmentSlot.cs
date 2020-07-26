
public class EquipmentSlot : ItemSlot
{
    public EquipmentType equipmentType;

    protected override void OnValidate()
    {
        base.OnValidate();

        // Automatically name the slot
        gameObject.name = equipmentType.ToString() + " Slot";
    }
}
   
