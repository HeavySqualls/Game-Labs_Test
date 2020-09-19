using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] StatsDisplay shieldHealth;

    public GameObject shield;

    [SerializeField] ShipUI shipUI;
    private ShipController shipCon;

    private void Start()
    {
        shipCon = GetComponentInParent<ShipController>();
    }

    public void DamageShield(float damage)
    {
        shipCon.currentShield -= damage;
        shipCon.shipUI.SetShieldBar(shipCon.currentShield);

        if (shipCon.currentShield <= 0.5f)
        {
            shield.SetActive(false);
            StartCoroutine(shipUI.ShieldTimer(shipCon.shipShieldRegen.Stat.Value, this));
        }

        // TODO: Add in shield damage effect here 
    }
}
