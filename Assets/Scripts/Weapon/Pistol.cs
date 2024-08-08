using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] AimComponent aimComp;
    public override void Attack()
    {
        GameObject target = aimComp.GetAimTarget();
        Debug.Log($"aiming at {target}");
    }
}
