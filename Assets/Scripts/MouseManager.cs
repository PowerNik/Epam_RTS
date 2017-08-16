using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static MouseManager Current;
    public MouseManager()
    {
        Current = this;
    }

    private List<Selectable> visibleUnits = new List<Selectable>();
    public List<Selectable> VisibleUnits { get { return visibleUnits; } }
    private List<Selectable> selectedObjects = new List<Selectable>();
    public List<Selectable> SelectedObjects { get { return selectedObjects; } }

    public GameObject targetPoint;

    // Для группового выделения
    float startPosX = 0, startPosY = 0;
    bool drawing = false;
    Rect rect = new Rect();


    // Use this for initialization
    void Start()
    {

    }
    void LeftMouseButtonHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            if (hit.collider.GetComponent<Selectable>())
            {
                var selectable = hit.collider.GetComponent<Selectable>();
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (selectable.GetComponent<Movable>() && !selectable.GetComponent<Unit>().IsEnemy)
                    {
                        selectedObjects.Add(selectable);
                        selectable.Select();
                    }
                    else
                    {
                        selectedObjects.Clear();
                        selectedObjects.Add(selectable);
                        selectable.Select();
                    }
                }
                else
                {
                    foreach (var selection in selectedObjects)
                    {
                        selection.Deselect();
                    }
                    selectedObjects.Clear();
                    selectedObjects.Add(selectable);
                    selectable.Select();
                }

            }
            else
            {
                foreach (var selection in selectedObjects)
                {
                    selection.Deselect();
                }
                selectedObjects.Clear();
            }
        }
    }
    void RightMouseButtonHandler()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (selectedObjects.Count == 0)
                return;
            else
            {
                if (selectedObjects[0].GetComponent<Unit>().IsEnemy)
                    return;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);

                if (hit.collider.GetComponent<Unit>() != null)
                {
                    if (hit.collider.GetComponent<Unit>().IsEnemy)
                    {
                        hit.collider.GetComponent<Selectable>().AttackSelection();
                        foreach (var obj in SelectedObjects)
                        {
                            if (obj.GetComponent<Attack>() != null)
                            {
                                obj.GetComponent<Attack>().EnableAttack(hit.collider.GetComponent<Unit>());
                            }
                        }
                    }
                    else if (hit.collider.GetComponent<UnitContainer>() != null)
                    {
                        hit.collider.GetComponent<Selectable>().FriendlySelection();
                        foreach (var unit in SelectedObjects)
                        {
                            if (unit.GetComponent<Movable>() != null)
                            {
                                unit.GetComponent<Movable>().MoveToTarget(hit.point);
                                unit.GetComponent<Movable>().ApproachingContainer = true;
                                unit.GetComponent<Movable>().Container = hit.collider.GetComponent<UnitContainer>();
                            }
                        }
                    }

                }
                else
                {
                    foreach (var unit in selectedObjects)
                    {
                        if (unit.GetComponent<Movable>()!=null)
                            unit.GetComponent<Movable>().MoveToTarget(hit.point);
                    }
                    if (hit.collider.GetComponent<Unit>() == null)
                        Instantiate(targetPoint, hit.point + Vector3.up * 0.05f, Quaternion.Euler(90f, 0, 0));
                }
            }

        }
    
    }
    // Update is called once per frame
    void Update()
    {
        LeftMouseButtonHandler();
        RightMouseButtonHandler();
    }

    private void OnGUI()
    {


        var texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, new Color(0, 0, 0, 0.25f));
        texture.Apply();

        if (Input.GetMouseButtonDown(0))
        {
            startPosX = Input.mousePosition.x;
            startPosY = Screen.height - Input.mousePosition.y;
            drawing = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            drawing = false;
            foreach (var u in VisibleUnits)
            {
                var holder = Camera.main.WorldToScreenPoint(u.transform.position);
                holder.y = Screen.height - holder.y;
                if ((rect.Contains(holder, true)) && (!u.GetComponent<Unit>().IsEnemy) && (u.GetComponent<Movable>() != null))
                {
                    selectedObjects.Add(u);
                    u.Select();
                }
            }
        }
        if (drawing)
        {
            rect = new Rect(startPosX, startPosY, Input.mousePosition.x - startPosX, Screen.height - Input.mousePosition.y - startPosY);

            GUI.DrawTexture(rect, texture);
        }

    }

}