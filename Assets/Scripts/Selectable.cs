using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {

    private SpriteRenderer highlight;
    private bool selected;
    public bool Selected { get { return selected; } }

    private void Start()
    {
        highlight = GetComponentInChildren<SpriteRenderer>();
    }

    public void Select()
    {
        selected = true;
        highlight.enabled = true;
        ActionButtonManager.Current.ClearButtons();
        foreach (var action in GetComponents<ActionBehaviour>())
            ActionButtonManager.Current.AddButton(action.ButtonIcon, action.GetClickAction());
    }


    public void Deselect()
    {
        selected = false;
        highlight.enabled = false;
        ActionButtonManager.Current.ClearButtons();
    }
    public void AttackSelection()
    {
        highlight.enabled = true;
        highlight.color = Color.red;
        highlight.GetComponent<BlinkingSelection>().blinking = true;
    }
    public void FriendlySelection()
    {
        highlight.enabled = true;
        highlight.color = Color.blue;
        highlight.GetComponent<BlinkingSelection>().blinking = true;
    }

    public void OnBecameVisible()
    {
        MouseManager.Current.VisibleUnits.Add(GetComponent<Selectable>());
    }
    public void OnBecameInvisible()
    {
        MouseManager.Current.VisibleUnits.Remove(GetComponent<Selectable>());
    }
    private void OnDestroy()
    {
        MouseManager.Current.SelectedObjects.Remove(this);
        Deselect();   
    }
}
