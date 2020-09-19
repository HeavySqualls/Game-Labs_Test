using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitShip : ProjectileInteractable
{
    public override void OnBulletHit(Collision other, Projectile projectile)
    {
        Debug.Log("Ship Hit!");
        gameObject.GetComponentInParent<ShipController>().DamageShip(projectile.projectileDamage);
    }
}
