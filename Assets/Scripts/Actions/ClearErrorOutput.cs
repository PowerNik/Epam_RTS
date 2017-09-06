using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClearErrorOutput //: MonoBehaviour
{

    public UnityAction action()
    {
        return delegate ()
        {
            //PlayerManager playerOwner = GetComponent<Unit>().playerOwner;
            //playerOwner.SpawnStructure(StructuresTypes.BaseStructure, GetComponent<Unit>().gameObject.transform.position);
        };
    }
}
