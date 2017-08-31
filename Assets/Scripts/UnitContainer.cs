using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitContainer : MonoBehaviour {

    [SerializeField]
    int capacity;
    public int Capacity { get { return capacity; } set { capacity = value; } }
    public List<Unit> UnitsInside = new List<Unit>();
    void Start () {
		
	}
    
    void Update()
    {
        //TODO.Remove hardcode buttons
        if (Input.GetKeyDown(KeyCode.U) && GetComponent<Selectable>().Selected)
        {
            UnloadUnits();
            print("units unloaded");
        }
    }

    public void LoadUnit(Unit unit)
    {
        if (!unit.IsLoadable)
            return;
        if (UnitsInside.Count < capacity)
        {
            UnitsInside.Add(unit);
            unit.gameObject.SetActive(false);
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
}
