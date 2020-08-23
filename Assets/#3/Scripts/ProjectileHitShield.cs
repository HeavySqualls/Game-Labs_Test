using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitShield : ProjectileInteractable
{
    public override void OnBulletHit(Collision other, Projectile projectile)
    {
        gameObject.GetComponentInParent<ShieldController>().DamageShield(projectile.projectileDamage);
        Debug.Log("Shield Hit!");
    }
}
