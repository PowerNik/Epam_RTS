using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator
{
	public Vector3 CitizenBasePoint { get; private set; }
	public Vector3[] FermerBasePoints { get; private set; }

	private TileGrid tileGrid;
	private LayerSettings layerTileSets;

	private MainPointsCreator mainPointsCreator;
	private LayerCreator layerCreator;

	private float tileSize;


	public MapCreator(MapSettingsManagerSO mapSetsManager, ref TileGrid tileGrid)
	{
		this.tileGrid = tileGrid;
		SetParams(mapSetsManager);
		MapCreating();
	}

	private void SetParams(MapSettingsManagerSO mapSetsManager)
	{
		layerTileSets = mapSetsManager.GetLayerTileSettings();

		MapSizeSettings mapSizeSets = mapSetsManager.GetMapSizeSettings();
		tileSize = mapSizeSets.tileSize;

		mainPointsCreator = new MainPointsCreator(mapSizeSets, mapSetsManager.GetMainPointsSettings(), ref tileGrid);
		layerCreator = new LayerCreator(mapSizeSets, layerTileSets, ref tileGrid);
	}

	private void MapCreating()
	{
		mainPointsCreator.CreateMainPoints();
		layerCreator.CreateLayers();

		CitizenBasePoint = mainPointsCreator.CitizenBasePoint;
		FermerBasePoints = mainPointsCreator.FermerBasePoints;
	}

	public void CreateMapMesh(GameObject map)
	{
		foreach (var item in tileGrid.GetTileDictionary())
		{
			CreateMeshForLayer(map, item.Key);
		}
	}

	private void CreateMeshForLayer(GameObject map, TileType tileType)
	{
		int[,] mas = tileGrid.GetTileMap(tileType);

		GameObject layerGO = new GameObject();
		layerGO.name = tileType.ToString();
		layerGO.transform.parent = map.transform;
		layerGO.AddComponent<MeshCollider>();
		layerGO.AddComponent<MeshFilter>();

		MeshSettings meshSets = new MeshSettings();
		if (layerTileSets.GetLayerTileDictionary().ContainsKey(tileType))
		{
			meshSets = layerTileSets.GetMeshSettings(tileType);
		}

		MeshGenerator meshGen = layerGO.AddComponent<MeshGenerator>();
		meshGen.GenerateMesh(mas, tileSize, meshSets);
		meshGen.gameObject.AddComponent<NavMeshSourceTag>();

		Tile tile = tileGrid.GetTileDictionary()[tileType];
		layerGO.AddComponent<MeshRenderer>().material = tile.GetMaterial();
	}
}
