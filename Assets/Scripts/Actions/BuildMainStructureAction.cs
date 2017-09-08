using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildMainStructureAction : ActionBehaviour
{
    public override void CurrentAction(System.Object obj)
    {
        BuildStructure();
        print("yep");
    }

    void BuildStructure()
    {
        Debug.Log("Button clicked");
        PlayerManager playerOwner = GetComponent<Unit>().playerOwner;
        playerOwner.SpawnStructure(StructuresTypes.BaseStructure, GetComponent<Unit>().gameObject.transform.position);
    }
}
