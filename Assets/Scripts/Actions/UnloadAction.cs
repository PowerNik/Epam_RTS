using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnloadAction : ActionBehaviour
{

    public override UnityAction GetClickAction()
    {
        return delegate ()
        {
            GetComponent<UnitContainer>().UnloadUnits();
        };
    }
}
