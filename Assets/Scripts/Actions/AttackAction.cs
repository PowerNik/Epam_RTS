using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackAction : ActionBehaviour {

    bool Enabled = false;
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
                
                if (hit.collider.GetComponent<Unit>() != null)
                {
                    if (hit.collider.GetComponent<Unit>().IsEnemy == true)
                        GetComponent<Attack>().EnableAttack(hit.collider.GetComponent<Unit>());
                    Enabled = false;
                    MouseManager.Current.enabled = true;
                }
                else
                {
                    Enabled = false;
                    MouseManager.Current.enabled = true;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                if (hit.collider.GetComponent<Unit>() != null)
                    if (hit.collider.GetComponent<Unit>().IsEnemy)
                    {

                        if (Input.GetKey(KeyCode.LeftShift))
                        {

                            foreach (var unit in MouseManager.Current.SelectedObjects)
                            {
                                unit.GetComponent<Unit>().ActionsQueue.Enqueue(delegate ()
                                {
                                    unit.GetComponent<Attack>().EnableAttack(hit.collider.GetComponent<Unit>());
                                });
                            }
                        }
                        else
                        {
                            foreach (var unit in MouseManager.Current.SelectedObjects)
                            {
                                unit.GetComponent<Unit>().ActionsQueue.Enqueue(delegate ()
                                {
                                    unit.GetComponent<Attack>().EnableAttack(hit.collider.GetComponent<Unit>());
                                });

                            }
                            Enabled = false;
                            MouseManager.Current.enabled = true;
                        }
                    }
            }
        }

    }
}
