using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] StatsDisplay shieldHealth;

    public void DamageShield(float damage)
    {
        //shieldHealth.Stat.Value -= damage;
    }
}
