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
    }
    public void Deselect()
    {
        selected = false;
        highlight.enabled = false;
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
