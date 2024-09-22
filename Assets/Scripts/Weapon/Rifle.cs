using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] AimComponent aimComp;
    [SerializeField] float Damage = 5f;
    public override void Attack()
    {
        GameObject target = aimComp.GetAimTarget();
       // Debug.Log($"aiming at {target}");
        DamageGameObject(target, Damage);

    }
}
