using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneManager
{
	private static MapManager mapManager;

	public static MapManager MapManager
	{
		get
		{
			if(mapManager == null)
			{
				mapManager = new MapManager();
			}

			return mapManager;
		}
	}
}
