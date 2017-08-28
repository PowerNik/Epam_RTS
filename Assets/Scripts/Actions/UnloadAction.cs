using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnloadAction : ActionBehaviour {

    public override UnityAction GetClickAction()
    {
        return delegate ()
        {
            GetComponent<UnitContainer>().UnloadUnits();

            if (InfoPanel.Current.SingleSelection.gameObject.activeSelf)
            {
                foreach (var button in InfoPanel.Current.SingleSelection.ContainerPanel.GetComponentsInChildren<Button>())
                {
                    Destroy(button.gameObject);
                }
            }
        };
    }
}
