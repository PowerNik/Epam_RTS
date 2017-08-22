using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelecter : MonoBehaviour
{
	public Material impassibleTileMat;
	public GameObject selectedTilePrefab;
	private Transform selectedTile;

	private MapManager mapManager;
	private float tileSize;

	// Число тайлов под макет здания
	private int selectedAreaCountX;
	private int selectAreaCountZ;

	void Start()
	{
		mapManager = SceneManagerRTS.MapManager;
		tileSize = mapManager.TileSize;

		selectedTile = Instantiate(selectedTilePrefab).transform;
		selectedTile.localScale *= tileSize;
	}

	private void Update()
	{
		AreaSelect(selectedTile.position, 2.1f, 1.9f);
		TileSelect();
	}

	public void AreaSelect(Vector3 pos, float areaSizeX, float areaSizeZ)
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			CreateSelectedArea(pos, areaSizeX, areaSizeZ);
			PlaceSelectedArea();
			AreaRevision();
		}

		if (Input.GetKeyUp(KeyCode.Alpha1))
		{
			AreaDeselect();
		}
	}

	private void TileSelect()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			Vector3 pos = mapManager.GetTilePos(hit.point);
			selectedTile.position = pos + Vector3.up * 0.1f;
		}
	}

	private void CreateSelectedArea(Vector3 pos, float xSize, float zSize)
	{
		selectedAreaCountX = (int)(xSize / tileSize) + 1;
		selectAreaCountZ = (int)(zSize / tileSize) + 1;
		selectedTile.position = pos;

		if (selectedAreaCountX % 2 == 0)
		{
			selectedTile.position -= Vector3.left * tileSize / 2f;
		}
		if (selectAreaCountZ % 2 == 0)
		{
			selectedTile.position += Vector3.forward * tileSize / 2f;
		}

		GameObject[] tileArea = new GameObject[selectedAreaCountX * selectAreaCountZ];
		for (int i = 0; i < selectedAreaCountX * selectAreaCountZ; i++)
		{
			Instantiate(selectedTilePrefab).transform.parent = selectedTile;
		}
	}

	private void PlaceSelectedArea()
	{
		for (int x = 0; x < selectedAreaCountX; x++)
		{
			for (int z = 0; z < selectAreaCountZ; z++)
			{
				Vector3 parentPos = selectedTile.position;
				Vector3 dX = x * Vector3.left * tileSize;
				Vector3 dZ = z * Vector3.forward * tileSize;

				selectedTile.GetChild(x * selectAreaCountZ + z).transform.position = parentPos - dX + dZ;
			}
		}
	}

	private void AreaRevision()
	{
		for (int i = 0; i < selectedTile.childCount; i++)
		{
			Renderer rend = selectedTile.GetChild(i).gameObject.GetComponent<Renderer>();
			if (((int)selectedTile.position.x) % 2 == 0)
			{
				rend.sharedMaterial = impassibleTileMat;
			}
			else
			{
				rend.sharedMaterial = selectedTilePrefab.GetComponent<Renderer>().sharedMaterial;
			}
		}
	}

	private void AreaDeselect()
	{
		if (selectedTile.transform.childCount > 0)
		{
			foreach (Transform child in selectedTile.transform)
			{
				Destroy(child.gameObject);
			}
		}
	}

	private void OnDrawGizmos()
	{
		if (mapManager == null)
		{
			return;
		}

		float width = mapManager.MapWidth;
		float length = mapManager.MapLength;

		for (int x = 0; x < mapManager.TileCountX; x++)
		{
			for (int z = 0; z < mapManager.TileCountZ; z++)
			{
				float curZ = z * tileSize - length / 2;
				Gizmos.DrawLine(new Vector3(-width / 2, 0, curZ), new Vector3(width / 2, 0, curZ));
			}
			float curX = x * tileSize - width / 2;
			Gizmos.DrawLine(new Vector3(curX, 0, -length / 2), new Vector3(curX, 0, length / 2));
		}
	}
}
