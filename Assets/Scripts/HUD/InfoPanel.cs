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
            if (timeWaited >= 0.5f)
            {
				if (SelectedUnit)
				{
					SingleSelection.Health.text = SelectedUnit.Health.ToString();
					SingleSelection.CurrentAction.text = SelectedUnit.currentAction.ToString();

					timeWaited = 0;
				}
            }
            else
            {
                timeWaited += Time.deltaTime;
            }
        }
        if (GroupSelection.gameObject.activeSelf)
        {
            if (timeWaited >= 0.5f)
            {
                foreach (var pair in GroupSelection.SelectedUnits)
                {
                    pair.Value.GetComponentInChildren<Slider>().value = pair.Key.Health;
                }
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
        SingleSelection.Icon.sprite = unit.Settings.Icon;
        SingleSelection.Name.text = unit.Settings.Name;
        SingleSelection.Fraction.text = unit.Settings.Fraction.ToString();
        SingleSelection.Health.text = unit.Health.ToString();
        SingleSelection.CurrentAction.text = unit.currentAction.ToString();
        foreach (var b in SingleSelection.ContainerPanel.GetComponentsInChildren<Button>())
        {
            Destroy(b.gameObject);
        }
		//TODO HOTFIX
       // if(unit.GetComponent<UnitContainer>()!=null && !unit.IsEnemyFor(GetComponent<Unit>().playerOwner))
		if (unit.GetComponent<UnitContainer>()!=null && !unit.IsEnemyFor(unit.GetComponent<Unit>().playerOwner))
        {
            foreach (var u in unit.GetComponent<UnitContainer>().UnitsInside)
            {
                var go = Instantiate(UnitInGroupSelectionPrefab, SingleSelection.ContainerPanel.transform);
                go.image.sprite = u.Settings.Icon;
                go.GetComponentInChildren<Slider>().maxValue = u.Settings.MaxHealth;
                go.GetComponentInChildren<Slider>().value = u.Health;
                go.onClick.AddListener(delegate ()
                {
                    unit.GetComponent<UnitContainer>().UnloadUnit(u);
                    Destroy(go.gameObject);
                });
            }
        }
    }

    public void AddUnitToGroupSelection(Unit unit)
    {

        if (GroupSelection.SelectedUnits.ContainsKey(unit))
            return;

        GroupSelection.gameObject.SetActive(true);
        SingleSelection.gameObject.SetActive(false);

        var go = Instantiate(UnitInGroupSelectionPrefab, GroupSelection.transform);
        go.image.sprite = unit.Settings.Icon;
        go.GetComponentInChildren<Slider>().maxValue = unit.Settings.MaxHealth;
        go.GetComponentInChildren<Slider>().value = unit.Health;
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
		if(GroupSelection)
			GroupSelection.gameObject.SetActive(true);
		if (SingleSelection)
			SingleSelection.gameObject.SetActive(false);
        Button b;
        GroupSelection.SelectedUnits.TryGetValue(unit, out b);
		if(b)
			Destroy(b.gameObject);
        GroupSelection.SelectedUnits.Remove(unit);
    }

    public void Clear()
    {
        foreach (var pair in GroupSelection.SelectedUnits)
        {
            Button b;
            GroupSelection.SelectedUnits.TryGetValue(pair.Key, out b);
			if(b)
                Destroy(b.gameObject);
        }
        GroupSelection.SelectedUnits.Clear();
		if(GroupSelection)
			GroupSelection.gameObject.SetActive(false);
		if (SingleSelection)
			SingleSelection.gameObject.SetActive(false);

    }
}
