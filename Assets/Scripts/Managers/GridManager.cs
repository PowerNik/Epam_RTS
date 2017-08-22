using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	private MapManager mapManager;

	void Start()
	{
		mapManager = SceneManagerRTS.MapManager;
	}
}

