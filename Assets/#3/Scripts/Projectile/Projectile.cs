using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileDamage;

    private CapsuleCollider projCollider;
    private MeshRenderer meshRend;

    private void Start()
    {
        meshRend = GetComponentInChildren<MeshRenderer>();
        projCollider = GetComponentInChildren<CapsuleCollider>();
    }

    public void AddProjectileValues(float damage)
    {
        projectileDamage = damage;
    }

    private void OnCollisionEnter(Collision other)
    {
        ProjectileHitShield shieldWeHit = other.collider.GetComponent<ProjectileHitShield>();
        ProjectileHitShip shipWeHit = other.collider.GetComponent<ProjectileHitShip>();

        if (shieldWeHit != null)
        {
            // Do damage to the shield 
            shieldWeHit.OnBulletHit(other, this);
        }

        if (shipWeHit != null)
        {
            // Do damage to the ship
            shipWeHit.OnBulletHit(other, this);
        }

        //GameObject shieldHit = Instantiate(Resources.Load("partSyst_ShieldHit", typeof(GameObject))) as GameObject;
        // Destroy instance of projectile
        ParticleSystem ps = Instantiate(Resources.Load("partSyst_ShieldHit"), gameObject.transform) as ParticleSystem;
        //Instantiate(Resources.Load("partSyst_ShieldHit"), gameObject.transform);
        meshRend.enabled = false;
        projCollider.enabled = false;
        Destroy(gameObject, 1);
    }
}
