using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEventManager : MonoBehaviour {

    public delegate void SelectAction();
    public static event SelectAction OnSelect;
    public static event SelectAction OnDeselect;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
