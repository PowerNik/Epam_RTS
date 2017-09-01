using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToTargetAction : ActionBehaviour {

    bool Enabled = false;
    int index = 0;


    public override UnityAction GetClickAction()
    {
        return delegate ()
        {
            foreach (var unit in MouseManager.Current.SelectedObjects)
            {
                unit.GetComponent<Unit>().ActionsQueue.Clear();
            }
            Enabled = true;
            MouseManager.Current.enabled = false;
        };
    }


    private void Update()
    {
        if (Enabled)
        {
            print("enabled");
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                if (Input.GetKey(KeyCode.LeftShift))
                { 
                    foreach (var unit in MouseManager.Current.SelectedObjects)
                    {
                        unit.GetComponent<Unit>().ActionsQueue.Enqueue(delegate ()
                            {
                                unit.GetComponent<Movable>().MoveToTarget(hit.point);
                            });
                    }
                }
                else
                {
                    foreach (var unit in MouseManager.Current.SelectedObjects)
                    {
                        unit.GetComponent<Unit>().ActionsQueue.Enqueue(delegate ()
                        {
                            unit.GetComponent<Movable>().MoveToTarget(hit.point);
                        });

                    }
                    Enabled = false;
                    MouseManager.Current.enabled = true;
                    index = 0;
                }
            }
        }
    }
}
