using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ActionBehaviour : MonoBehaviour
{

    public bool Enabled;
    public bool ShiftWasPressed = false;

    public abstract void CurrentAction(System.Object obj);

    public Sprite ButtonIcon;
    public UnityAction GetClickAction()
    {
        return delegate ()
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                foreach (var unit in MouseManager.Current.SelectedObjects)
                {
                    unit.GetComponent<Unit>().ActionsQueue.Clear();
                }
                Enabled = true;
                MouseManager.Current.enabled = false;
            }
            else
            {
                Enabled = true;
                MouseManager.Current.enabled = false;
            }
        };
    }


    public void ProceedAction()
    {
        if (Enabled)
        {
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
                            CurrentAction(hit);
                        });
                    }
                    ShiftWasPressed = true;
                }
                else
                {
                    if (ShiftWasPressed)
                    {
                        Enabled = false;
                        MouseManager.Current.enabled = true;
                        ShiftWasPressed = false;
                    }
                    else
                    {
                        foreach (var unit in MouseManager.Current.SelectedObjects)
                        {
                            unit.GetComponent<Unit>().ActionsQueue.Clear();
                            CurrentAction(hit);
                        }
                        Enabled = false;
                        MouseManager.Current.enabled = true;
                    }
                }
            }

        }
    }


    private void Update()
    {
        if (Enabled) ProceedAction();
    }
}
