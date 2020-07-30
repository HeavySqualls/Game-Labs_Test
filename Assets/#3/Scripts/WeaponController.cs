using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int weaponID;

    [Header("WEAPON STATS:")]
    [SerializeField] StatsDisplay weaponDamage;
    [SerializeField] StatsDisplay weaponReload;

    private GameObject target;
    [SerializeField] ShipController shipCon;

    private void Start()
    {
        shipCon = GetComponentInParent<ShipController>();
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
        Debug.Log(shipCon.name + "'s " + gameObject.name + " fired.");
       
        // Reload Weapon
        ReloadWeapon();
    }
}
