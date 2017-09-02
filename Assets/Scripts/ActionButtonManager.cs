using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionButtonManager : MonoBehaviour {

    public static ActionButtonManager Current;
    public List<Button> Buttons;
    private List<UnityAction> actionCalls = new List<UnityAction>();
    public ActionButtonManager()
    {
        Current = this;
    }
    public void ClearButtons()
    {
        foreach (var button in Buttons)
        if(button != null)
            button.gameObject.SetActive(false);

            actionCalls.Clear();
        
    }
    public void AddButton(Sprite icon, UnityAction onClick)
    {
        int index = actionCalls.Count;
        Buttons[index].gameObject.SetActive(true);
        Buttons[index].GetComponent<Image>().sprite = icon;
        actionCalls.Add(onClick);
    }

    public void OnButtonClick(int index)
    {
        actionCalls[index]();
    }

    private void Start()
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            int index = i;
            Buttons[index].onClick.AddListener(delegate ()
            {
                OnButtonClick(index);
            });
        }
        ClearButtons();
    }
}
