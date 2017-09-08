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

        if (MouseManager.Current.SelectedObjects.Count == 0)
            InfoPanel.Current.ShowSingleUnitInfo(GetComponent<Unit>());
        if (MouseManager.Current.SelectedObjects.Count == 1)
        {
            InfoPanel.Current.AddUnitToGroupSelection(MouseManager.Current.SelectedObjects[0].GetComponent<Unit>());
            InfoPanel.Current.AddUnitToGroupSelection(GetComponent<Unit>());
        }
        if (MouseManager.Current.SelectedObjects.Count > 1)
        {
            InfoPanel.Current.AddUnitToGroupSelection(GetComponent<Unit>());
        }

        
    }


    public void Deselect()
    {
        selected = false;
		//Hotfix2
		if(highlight != null)
        highlight.enabled = false;
        ActionButtonManager.Current.ClearButtons();
        if (MouseManager.Current.SelectedObjects.Count == 1)
        {
            InfoPanel.Current.Clear();
        }

		if (MouseManager.Current.SelectedObjects.Count >= 2)
		{
			//Hotfix2
			if (gameObject == null)
				return;

			var unit = GetComponent<Unit>();
			if(unit != null)
			InfoPanel.Current.RemoveUnitFromGroupSelection(unit);
		}
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
        #region ErrorDeselect
        //Deselect();
        #endregion
        MouseManager.Current.SelectedObjects.Remove(this);
    }
}
