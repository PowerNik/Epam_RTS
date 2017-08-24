using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    public static InfoPanel Current;
    public InfoPanel()
    {
        Current = this;
    }
    public GroupSelectionLinks GroupSelection;
    public SingleSelectionLinks SingleSelection;

    public Button UnitInGroupSelectionPrefab;

    Unit SelectedUnit;
    float timeWaited = 0;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(SingleSelection.gameObject.activeSelf)
        {
            if (timeWaited >= 1)
            {
                SingleSelection.Health.text = SelectedUnit.Health.ToString();
                timeWaited = 0;
            }
            else
            {
                timeWaited += Time.deltaTime;
            }
        }
	}



    public void ShowSingleUnitInfo(Unit unit)
    {
        SelectedUnit = unit;
        GroupSelection.gameObject.SetActive(false);
        SingleSelection.gameObject.SetActive(true);
        SingleSelection.Icon.sprite = unit.Icon;
        SingleSelection.Name.text = unit.Name;
        SingleSelection.Fraction.text = unit.Fraction.ToString();
        SingleSelection.Health.text = unit.Health.ToString();
    }

    public void AddUnitToGroupSelection(Unit unit)
    {
        if (GroupSelection.SelectedUnits.ContainsKey(unit))
            return;
        GroupSelection.gameObject.SetActive(true);
        SingleSelection.gameObject.SetActive(false);
        var go = Instantiate(UnitInGroupSelectionPrefab, GroupSelection.transform);
        go.image.sprite = unit.Icon;
        go.onClick.AddListener(delegate ()
       {
           foreach (var obj in MouseManager.Current.SelectedObjects)
           {
               obj.GetComponent<Selectable>().Deselect();
           }
           MouseManager.Current.SelectedObjects.Clear();
           unit.GetComponent<Selectable>().Select();
           MouseManager.Current.SelectedObjects.Add(unit.GetComponent<Selectable>());
       });
        GroupSelection.SelectedUnits.Add(unit, go);
    }

    public void RemoveUnitFromGroupSelection(Unit unit)
    {
        GroupSelection.gameObject.SetActive(true);
        SingleSelection.gameObject.SetActive(false);
        Button b;
        GroupSelection.SelectedUnits.TryGetValue(unit, out b);
        print(b);
        Destroy(b.gameObject);
        GroupSelection.SelectedUnits.Remove(unit);
    }

    public void Clear()
    {
        foreach (var pair in GroupSelection.SelectedUnits)
        {
            Button b;
            GroupSelection.SelectedUnits.TryGetValue(pair.Key, out b);
            Destroy(b.gameObject);
        }
        GroupSelection.SelectedUnits.Clear();
        GroupSelection.gameObject.SetActive(false);
        SingleSelection.gameObject.SetActive(false);

    }
}
