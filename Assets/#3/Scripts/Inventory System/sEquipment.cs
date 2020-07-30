using UnityEngine;
using Kryz.CharacterStats;

public enum EquipmentType
{
    Module, 
    Weapon,
}

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "Ship Equipment/Equipment", order = 1)]
public class sEquipment : sItem
{
    public EquipmentType equipmentType;
    [Space]
    [Header("WEAPON DATA:")]
    public float damage;
    public float reloadTime;

    [Space]
    [Space]
    [Header("MODULE DATA:")]
    [Space]
    public float HPBoost;
    public float ShieldBoost;
    [Space]
    public float ShieldRegenBonus;
    public float WeaponReloadBonus;

    public void Equip(ShipInventoryManager s)
    {
        if (HPBoost != 0)
            s.HP.AddModifier(new StatModifier(HPBoost, StatModType.Flat, this));

        if (ShieldBoost != 0)
            s.Shield.AddModifier(new StatModifier(ShieldBoost, StatModType.Flat, this));

        if (ShieldRegenBonus != 0)
            s.ShieldRegen.AddModifier(new StatModifier(ShieldRegenBonus, StatModType.PercentMult, this));

        if (WeaponReloadBonus != 0)
        {
            s.GunA_Reload.AddModifier(new StatModifier(WeaponReloadBonus, StatModType.PercentMult, this));
            s.GunB_Reload.AddModifier(new StatModifier(WeaponReloadBonus, StatModType.PercentMult, this));
        }


        if (damage != 0)
        {
            if (s.equipmentPanel.currentWeaponEquipSlot == 0) 
            {
                s.GunA_Damage.AddModifier(new StatModifier(damage, StatModType.Flat, this));
                s.GunA_Reload.AddModifier(new StatModifier(reloadTime, StatModType.Flat, this));
            }
            else if (s.equipmentPanel.currentWeaponEquipSlot == 1)
            {
                s.GunB_Damage.AddModifier(new StatModifier(damage, StatModType.Flat, this));
                s.GunB_Reload.AddModifier(new StatModifier(reloadTime, StatModType.Flat, this));
            }
        }
    }

    public void Unequip(ShipInventoryManager s)
    {
        //s.HP.RemoveModifier(new StatModifier(HPBoost, StatModType.Flat, this));
        s.HP.RemoveAllModifiersFromSource(this);
        //s.Shield.RemoveModifier(new StatModifier(ShieldBoost, StatModType.Flat, this));
        s.Shield.RemoveAllModifiersFromSource(this);
        //s.ShieldRegen.RemoveModifier(new StatModifier(ShieldRegenBonus, StatModType.PercentMult, this));
        s.ShieldRegen.RemoveAllModifiersFromSource(this);

        if (s.equipmentPanel.currentWeaponEquipSlot == 0)
        {
            //s.GunA_Damage.RemoveModifier(new StatModifier(damage, StatModType.Flat, this));
            s.GunA_Damage.RemoveAllModifiersFromSource(this);
            //s.GunA_Reload.RemoveModifier(new StatModifier(WeaponReloadBonus, StatModType.Flat, this));
            s.GunA_Reload.RemoveAllModifiersFromSource(this);
        }
        else if (s.equipmentPanel.currentWeaponEquipSlot == 1)
        {
            //s.GunB_Damage.RemoveModifier(new StatModifier(damage, StatModType.Flat, this));
            s.GunB_Damage.RemoveAllModifiersFromSource(this);
            //s.GunB_Reload.RemoveModifier(new StatModifier(WeaponReloadBonus, StatModType.Flat, this));
            s.GunB_Reload.RemoveAllModifiersFromSource(this);
        }
    }
}
