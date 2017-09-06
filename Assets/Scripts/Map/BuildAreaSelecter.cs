using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAreaSelecter : MonoBehaviour
{
	public Material unbuildableTileMat;
	public GameObject selectedTilePrefab;

	private Transform selectedArea;
	private Vector3 defaultPosition = new Vector3(0, -100, 0);

	private GridManager gridManager;
	private float tileSize;

	// Число тайлов под макет здания
	private int tileCountX;
	private int tileCountZ;

	public void SetGridManager(GridManager gridManager)
	{
		this.gridManager = gridManager;
		tileSize = gridManager.tileGrid.TileSize;
	}

	void Start()
	{
		selectedArea = new GameObject().transform;
		selectedArea.name = "BuildingArea";
		selectedArea.position = defaultPosition;
	}

	public bool SelectBuildArea(Vector3 pos, float areaSizeX, float areaSizeZ, Race race)
	{
		SetSelectAreaPosition(pos, areaSizeX, areaSizeZ);
		return SelectAreaRevision(race, true);
	}

	public bool SelectExtractArea(Vector3 pos, float areaSizeX, float areaSizeZ, Race race)
	{
		SetSelectAreaPosition(pos, areaSizeX, areaSizeZ);
		return SelectAreaRevision(race, false);
	}

	private void SetSelectAreaPosition(Vector3 pos, float areaSizeX, float areaSizeZ)
	{
		if (selectedArea.position == defaultPosition)
		{
			CreateTiles(areaSizeX, areaSizeZ);
			PlaceTilesOnSelectArea();
		}

		selectedArea.position = pos;
	}

	private void CreateTiles(float xSize, float zSize)
	{
		tileCountX = CalculateTileCount(xSize);
		tileCountZ = CalculateTileCount(zSize);

		for (int i = 0; i < tileCountX * tileCountZ; i++)
		{
			GameObject go = Instantiate(selectedTilePrefab);
			go.transform.parent = selectedArea.transform;
			go.transform.localScale *= tileSize;
		}
	}

	private int CalculateTileCount(float size)
	{
		int count = (int)(size / tileSize);

		if (size % tileSize != 0)
		{
			count++;
		}

		if (count % 2 == 0)
		{
			count++;
		}

		return count;
	}

	private void PlaceTilesOnSelectArea()
	{
		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				Vector3 parentPos = selectedArea.position;
				Vector3 dX = (x - tileCountX / 2) * Vector3.left * tileSize;
				Vector3 dZ = (z - tileCountZ / 2) * Vector3.forward * tileSize;

				selectedArea.GetChild(x * tileCountZ + z).transform.position = parentPos - dX + dZ;
			}
		}
	}

	/// <summary>
	/// Можно ли использовать выбранную область
	/// </summary>
	/// <param name="race"></param>
	/// <param name="isBuild">true - для зданий, false - для экстракторов</param>
	/// <returns></returns>
	private bool SelectAreaRevision(Race race, bool isBuild)
	{
		bool isSelectableArea = true;

		// TODO Nik
		// Читать из gridManager массив тайлов/булов нужного размера
		for (int i = 0; i < selectedArea.childCount; i++)
		{
			Renderer rend = selectedArea.GetChild(i).gameObject.GetComponent<Renderer>();
			rend.sharedMaterial = selectedTilePrefab.GetComponent<Renderer>().sharedMaterial;

			bool isAllow;
			if (isBuild)
			{
				isAllow = gridManager.IsBuildableTile(selectedArea.GetChild(i).transform.position, race);
			}
			else
			{
				isAllow = gridManager.IsExtractableTile(selectedArea.GetChild(i).transform.position, race);
			}

			if(!isAllow)
			{
				rend.sharedMaterial = unbuildableTileMat;
				isSelectableArea = false;
			}
		}

		return isSelectableArea;
	}

	public void DeselectArea()
	{
		if (selectedArea.transform.childCount > 0)
		{
			foreach (Transform child in selectedArea.transform)
			{
				Destroy(child.gameObject);
			}
		}

		selectedArea.position = defaultPosition;
	}
}
