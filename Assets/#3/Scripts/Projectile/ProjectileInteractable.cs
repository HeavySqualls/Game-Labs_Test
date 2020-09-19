using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileInteractable : MonoBehaviour
{
    public abstract void OnBulletHit(Collision other, Projectile projectile);
}
