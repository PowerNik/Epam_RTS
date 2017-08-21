using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	public GameObject selectedTilePrefab;
	private Transform selectedTile;
	private MapManager mapManager;

	// Use this for initialization
	void Start()
	{
		selectedTile = Instantiate(selectedTilePrefab).transform;
		selectedTile.localScale *= SceneManagerRTS.MapManager.GetMapGeneratorSettings().tileSize;
		mapManager = SceneManagerRTS.MapManager;
	}

	private void Update()
	{
		MouseMove();
	}

	private void MouseMove()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			Vector3 pos = mapManager.GetTilePos(hit.point);
			selectedTile.position = pos + Vector3.up * 0.1f;
		}
		else
		{
			selectedTile.position = new Vector3(0, -10, 0);
		}
	}

	private void OnDrawGizmos()
	{
		if (mapManager == null)
		{
			return;
		}
		MapGeneratorSettings genSets = mapManager.GetMapGeneratorSettings();
		for (int x = 0; x < mapManager.TileGrid.Width; x++)
		{
			for (int z = 0; z < mapManager.TileGrid.Length; z++)
			{
				float xL = -genSets.width / 2;
				float xR = genSets.width / 2;
				float curZ = z * genSets.tileSize - genSets.length / 2;
				Gizmos.DrawLine(new Vector3(xL, 0, curZ), new Vector3(xR, 0, curZ));
			}

			float zD = -genSets.length / 2;
			float zU = genSets.length / 2;
			float curX = x * genSets.tileSize - genSets.width / 2;
			Gizmos.DrawLine(new Vector3(curX, 0, zD), new Vector3(curX, 0, zU));
		}
	}
}

