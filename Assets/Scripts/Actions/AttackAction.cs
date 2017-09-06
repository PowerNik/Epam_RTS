using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackAction : ActionBehaviour {

    public override void CurrentAction(System.Object obj)
    {
        RaycastHit hit = (RaycastHit)obj;
        Unit targetUnit = hit.collider.GetComponent<Unit>();
        GetComponent<Attack>().EnableAttack(targetUnit);
    }
}
