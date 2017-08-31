using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildMainStructureAction : ActionBehaviour
{
    public override UnityAction GetClickAction()
    {
        return delegate ()
        {
            Debug.Log("Button clicked");
            PlayerManager playerOwner = GetComponent<Unit>().playerOwner;
            playerOwner.SpawnStructure(StructuresTypes.BaseStructure, GetComponent<Unit>().gameObject.transform.position);
            //playerOwner.playerFactory.SpawnBaseStructure(GetComponent<Unit>().gameObject.transform.position);
        };
    }
}
