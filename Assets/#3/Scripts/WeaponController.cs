﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int weaponID;

    [Header("WEAPON STATS:")]
    [SerializeField] StatsDisplay weaponDamage;
    [SerializeField] StatsDisplay weaponReload;

    [SerializeField] float thrust;
    [SerializeField] GameObject projectilePrefab;

    private Transform gunPos;
    private GameObject target;
    [SerializeField] ShipController shipCon;

    private void Start()
    {
        shipCon = GetComponentInParent<ShipController>();
        gunPos = this.transform;
    }

    public void SetTargetAndAttack(GameObject _target)
    {
        //Check to see if there is a weapon equipped before begining firing process
        if (weaponDamage.Stat.Value > 0)
        {
            target = _target;

            Debug.Log(shipCon.name + "'s " + gameObject.name + " targeting " + _target.name);

            ReloadWeapon();
        }
    }

    private void ReloadWeapon()
    {
        Debug.Log(shipCon.name + "'s " + gameObject.name + " reloading...");
        StartCoroutine(RunReloadTime());
    }

    private IEnumerator RunReloadTime()
    {
        yield return new WaitForSeconds(weaponReload.Stat.Value);

        FireWeapon();
    }

    private void FireWeapon()
    {
        // Instantiate projectile prefab 
        GameObject projectile = Instantiate(projectilePrefab, gunPos.position, transform.rotation);
        projectile.GetComponent<Projectile>().AddProjectileValues(weaponDamage.Stat.Value);
        projectile.transform.LookAt(target.transform);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * thrust, ForceMode.Impulse);

        Debug.Log(shipCon.name + "'s " + gameObject.name + " fired.");
       
        // Reload Weapon
        ReloadWeapon();
    }
}
