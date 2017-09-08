using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToTargetAction : ActionBehaviour {

    public override void CurrentAction(System.Object obj)
    {
        RaycastHit hit = (RaycastHit)obj;
        Vector3 targetMove = hit.point;
		var movable = GetComponent<Movable>();
		if(movable != null)
			movable.MoveToTarget(targetMove);
    }


}
