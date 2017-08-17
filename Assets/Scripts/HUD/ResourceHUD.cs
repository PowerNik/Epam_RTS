using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
	Food = 1,
	Eqipment = 2,
	Special = 3
}
public class ResourceHUD : MonoBehaviour 
{

	[SerializeField]
	ResourceType resource;
	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
