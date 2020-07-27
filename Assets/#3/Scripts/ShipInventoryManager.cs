using UnityEngine;
using Kryz.CharacterStats;

public class ShipInventoryManager : MonoBehaviour
{
    public CharacterStat HP;
    public CharacterStat Shield;
    public CharacterStat ShieldRegen;
    public CharacterStat ReloadSpeed;

    public CharacterStat GunA_Damage;
    public CharacterStat GunA_Reload;

    public CharacterStat GunB_Damage;
    public CharacterStat GunB_Reload;

    public EquipmentPanel equipmentPanel;
    [SerializeField] Inventory inventory;
    [SerializeField] StatPanel statPanel;

    private void Awake()
    {
        statPanel.SetStats(HP, Shield, ShieldRegen, GunA_Damage, GunA_Reload, GunB_Damage, GunB_Reload);
        statPanel.UpdateStatValues();

        inventory.OnModuleItemRightClickEvent += EquipFromInventory;
        inventory.OnWeaponItemRightClickEvent += EquipFromInventory;

        equipmentPanel.OnModuleItemRightClickEvent += UnequipFromEquipPanel;
        equipmentPanel.OnWeaponItemRightClickEvent += UnequipFromEquipPanel;
    }

    private void EquipFromInventory(sItem _item)
    {
        Debug.Log("Equipped " + _item.name);

        if (_item is sEquipment)
        {
            Equip((sEquipment)_item);
        }
    }

    private void UnequipFromEquipPanel(sItem _item)
    {
        Debug.Log("Unequipped " + _item.name);
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
                previousItem.Unequip(this);

                statPanel.UpdateStatValues();

            }
            _item.Equip(this);
            statPanel.UpdateStatValues();
        }
        else
        {
            inventory.AddItem(_item);
        }
    }

    public void UnEquip(sEquipment _item)
    {
        equipmentPanel.RemoveItem(_item);
        _item.Unequip(this);
        statPanel.UpdateStatValues();
    }
}
