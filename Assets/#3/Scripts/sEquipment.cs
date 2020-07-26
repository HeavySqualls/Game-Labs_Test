﻿using UnityEngine;
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
            s.GunA_Reload.AddModifier(new StatModifier(WeaponReloadBonus, StatModType.PercentMult, this));
    }

    public void Unequip(ShipInventoryManager s)
    {
        s.HP.RemoveAllModifiersFromSource(this);
        s.Shield.RemoveAllModifiersFromSource(this);
        s.ShieldRegen.RemoveAllModifiersFromSource(this);
        s.GunA_Reload.RemoveAllModifiersFromSource(this);
    }
}
