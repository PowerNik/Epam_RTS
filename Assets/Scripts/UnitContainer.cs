using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitContainer : MonoBehaviour {

    [SerializeField]
    int capacity;
    public int Capacity { get { return capacity; } set { capacity = value; } }
    public List<Unit> UnitsInside = new List<Unit>();

    public void LoadUnit(Unit unit)
    {
        if (!unit.IsLoadable)
            return;
        if (UnitsInside.Count < capacity)
        {
            UnitsInside.Add(unit);
            unit.gameObject.SetActive(false);
            unit.GetComponent<Selectable>().Deselect();
            MouseManager.Current.SelectedObjects.Remove(unit.GetComponent<Selectable>());
        }
    }

    public void UnloadUnits()
    {
        foreach (var unit in UnitsInside)
        {
            unit.transform.position = transform.position+new Vector3(Random.Range(0, 2f), 0, Random.Range(0, 2f));
            unit.gameObject.SetActive(true);
        }
        UnitsInside.Clear();
    }

    public void UnloadUnit(Unit unit)
    {
        unit.transform.position = transform.position + new Vector3(Random.Range(0, 2f), 0, Random.Range(0, 2f));
        unit.gameObject.SetActive(true);
        UnitsInside.Remove(unit);
    }
}
