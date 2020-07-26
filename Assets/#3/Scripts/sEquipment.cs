using UnityEngine;

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
}
