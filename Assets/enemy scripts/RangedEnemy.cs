using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyBase
{
    ShootProjectile shootingControler;

    private void Awake()
    {
        shootingControler = GetComponentInChildren<ShootProjectile>();
    }

    public override void attack()
    {
        Projectile projectile = shootingControler.shootAt(player.transform.position);
        projectile.damage = attackDamage;
        projectile.whoShouldIHitTag = player.tag;
    }
}
