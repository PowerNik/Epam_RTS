using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
	public GameObject resourcePrefab;
	public ResourceType resourceType;
	public int recourceReserve = 100;

	public int count = 3;

	public int resourceAreaSize = 3;
	public int forcedAreaSize = 5;

	public bool isPlacingAroundCitizen = false;
}
