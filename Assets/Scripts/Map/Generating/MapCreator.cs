using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator
{
	public Vector3 CitizenBasePoint { get; private set; }
	public Vector3[] FermerBasePoints { get; private set; }

	private LayerCreator layerCreator;
	private BasePointsGenerator basePointsGen;

	private LayerSettings layerSets;
	public LayerType[,] LayerGrid { get; private set; }

	private float tileSize;


	public MapCreator(MapSettingsManagerSO mapSetsManager)
	{
		SetParams(mapSetsManager);
		MapCreating();
	}

	private void SetParams(MapSettingsManagerSO mapSetsManager)
	{
		layerSets = mapSetsManager.GetLayerSettings();

		MapSizeSettings mapSizeSets = mapSetsManager.GetMapSizeSettings();
		tileSize = mapSizeSets.tileSize;

		layerCreator = new LayerCreator(mapSetsManager);
		basePointsGen = new BasePointsGenerator(mapSetsManager);
	}

	private void MapCreating()
	{
		layerCreator.CreateLayers();
		basePointsGen.CreateBasePoints(layerCreator.LayerGrid);
		layerCreator.CorrectLayers(basePointsGen.LayerGrid);

		LayerGrid = layerCreator.LayerGrid;	
		CitizenBasePoint = basePointsGen.CitizenBasePoint;
		FermerBasePoints = basePointsGen.FermerBasePoints;
	}

	public void CreateMapMesh(GameObject map)
	{
		CreateMeshForLayer(map, LayerType.Ground);
		CreateMeshForLayer(map, LayerType.Water);
		CreateMeshForLayer(map, LayerType.Mountain);
	}

	private void CreateMeshForLayer(GameObject map, LayerType layerType)
	{
		int[,] mas = layerCreator.GetLayerMap(layerType);
		MeshSettings meshSets = layerSets.GetMeshSettings(layerType);

		GameObject layerGO = new GameObject();
		layerGO.transform.parent = map.transform;
		layerGO.AddComponent<MeshCollider>();
		layerGO.AddComponent<MeshFilter>();

		Material mat = layerSets.GetLayerBasicTile(layerType).material;
		layerGO.AddComponent<MeshRenderer>().material = mat;

		MeshGenerator meshGen = layerGO.AddComponent<MeshGenerator>();
		meshGen.GenerateMesh(mas, tileSize, meshSets);
		meshGen.gameObject.AddComponent<NavMeshSourceTag>();
	}
}
