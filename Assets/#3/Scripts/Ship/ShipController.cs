using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Space]
    [Header("SHIP STATS:")]
    [SerializeField] StatsDisplay shipHP;
    public float currentHP;
    public StatsDisplay shipShield;
    public float currentShield;
    public StatsDisplay shipShieldRegen;

    [Space]
    [Header("SHIP EQUIPMENT:")]
    [SerializeField] ShieldController shield;
    [SerializeField] WeaponController[] weapons;

    public bool isShipDead = false;
    public ParticleSystem deathParticles;

    [SerializeField] GameObject shipModel;
    [SerializeField] GameObject shipHUD;

    [HideInInspector] public ShipUI shipUI;
    public BattleManager battleManager;

    private void Start()
    {
        shipUI = GetComponentInChildren<ShipUI>();
        //battleManager = Toolbox.GetInstance().GetBattleManager();
    }

    public void BeginCombat(GameObject target)
    {
        currentHP = shipHP.Stat.Value;
        currentShield = shipShield.Stat.Value;

        shipUI.SetMaxHealthValue(currentHP);
        shipUI.SetMaxShieldValue(currentShield);

        foreach (WeaponController w in weapons)
        {
            w.SetTargetAndAttack(target);
        }
    }

    public void DamageShip(float damage)
    {
        currentHP -= damage;
        shipUI.SetHealthBar(currentHP);

        if (currentHP <= 0.5f)
        {
            print("ship destroyed!");
            Instantiate(deathParticles, gameObject.transform);
            battleManager.EndBattle();
            isShipDead = true;
            shipModel.SetActive(false);
            shipHUD.SetActive(false);
            shipUI.StopAttack();
        }

        // TODO: Add in shield damage effect here 
    }
}
