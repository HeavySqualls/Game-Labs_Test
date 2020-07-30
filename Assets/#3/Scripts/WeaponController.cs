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

    public void SetTargetAndAttack(GameObject _target)
    {
        target = _target;
        ReloadWeapon();
    }

    private void ReloadWeapon()
    {
        StartCoroutine(RunReloadTime());
    }

    private IEnumerator RunReloadTime()
    {
        yield return new WaitForSeconds(weaponReload.Stat.Value);

        FireWeapon();
    }

    private void FireWeapon()
    {
        // Fire projectile at target
        Debug.Log("Ship " + gameObject.name + " shooting");

        // Reload Weapon
        ReloadWeapon();
    }
}
