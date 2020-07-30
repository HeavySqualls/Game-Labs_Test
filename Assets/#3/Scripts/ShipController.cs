using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Space]
    [Header("SHIP STATS:")]
    [SerializeField] StatsDisplay shipHP;
    [SerializeField] StatsDisplay shipShield;
    [SerializeField] StatsDisplay shipShieldRegen;
    [Space]
    //[Header("Weapon A:")]
    //[Header("WEAPON STATS:")]
    //[SerializeField] StatsDisplay weaponADamage;
    //[SerializeField] StatsDisplay weaponAReload;
    //[Header("Weapon B:")]
    //[SerializeField] StatsDisplay weaponBDamage;
    //[SerializeField] StatsDisplay weaponBReload;

    [SerializeField] WeaponController[] weapons;
    [SerializeField] ShieldController shield;

    public void BeginCombat(GameObject target)
    {
        foreach (WeaponController w in weapons)
        {
            w.SetTargetAndAttack(target);
        }
    }
}
