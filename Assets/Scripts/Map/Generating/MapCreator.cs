using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator
{
	public Vector3 CitizenBasePoint { get; private set; }
	public Vector3[] FermerBasePoints { get; private set; }

	private TileGrid tileGrid;
	private LayerTileSettings layerTileSets;

	private LayerCreator layerCreator;
	private MainPointsCreator mainPointsCreator;

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
		CreateMeshForLayer(map, TileType.GroundLayer);
		CreateMeshForLayer(map, TileType.WaterLayer);
		CreateMeshForLayer(map, TileType.MountainsLayer);
	}

	private void CreateMeshForLayer(GameObject map, TileType tileType)
	{
		int[,] mas = tileGrid.GetTileTypeMap(tileType);
		MeshSettings meshSets = layerTileSets.GetMeshSettings(tileType);

		GameObject layerGO = new GameObject();
		layerGO.name = tileType.ToString();
		layerGO.transform.parent = map.transform;
		layerGO.AddComponent<MeshCollider>();
		layerGO.AddComponent<MeshFilter>();

		LayerTile tile = layerTileSets.GetLayerTile(tileType);
		layerGO.AddComponent<MeshRenderer>().material = tile.GetMaterial();

		MeshGenerator meshGen = layerGO.AddComponent<MeshGenerator>();
		meshGen.GenerateMesh(mas, tileSize, meshSets);
		meshGen.gameObject.AddComponent<NavMeshSourceTag>();
	}
}
