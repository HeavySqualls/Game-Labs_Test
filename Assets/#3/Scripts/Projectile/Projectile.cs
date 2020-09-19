using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileDamage;

    public ParticleSystem shieldPs;
    public ParticleSystem shipPs;

    private CapsuleCollider projCollider;
    private MeshRenderer meshRend;
    private TrailRenderer trailRend;
    private Rigidbody rb;

    private void Start()
    {
        meshRend = GetComponentInChildren<MeshRenderer>();
        trailRend = GetComponent<TrailRenderer>();
        projCollider = GetComponentInChildren<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
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
            Instantiate(shieldPs, gameObject.transform);
        }

        if (shipWeHit != null)
        {
            // Do damage to the ship
            shipWeHit.OnBulletHit(other, this);
            Instantiate(shipPs, gameObject.transform);
        }

        // Destroy instance of projectile
        //ParticleSystem ps = Instantiate(Resources.Load("partSyst_ShieldHit 1.0"), gameObject.transform) as ParticleSystem;

        trailRend.enabled = false;
        meshRend.enabled = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        projCollider.enabled = false;
        Destroy(gameObject, 1);
    }
}
