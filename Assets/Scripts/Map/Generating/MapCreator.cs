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

	private MapLayers mapLayers;
	public MapLayerType[,] LayerGrid { get; private set; }

	private float tileSize;


	public MapCreator(MapSettingsSO mapSettings)
	{
		SetParams(mapSettings);
		MapCreating();
	}

	private void SetParams(MapSettingsSO mapSettings)
	{
		mapLayers = mapSettings.GetMapLayers();

		MapSizeSettings mapSizeSets = mapSettings.GetMapSizeSettings();
		tileSize = mapSizeSets.tileSize;

		layerCreator = new LayerCreator(mapSettings);
		basePointsGen = new BasePointsGenerator(mapSettings);
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
		CreateMeshForLayer(map, MapLayerType.Ground);
		CreateMeshForLayer(map, MapLayerType.Water);
		CreateMeshForLayer(map, MapLayerType.Mountain);
	}

	private void CreateMeshForLayer(GameObject map, MapLayerType layerType)
	{
		int[,] mas = layerCreator.GetLayerMap(layerType);
		MeshSettings meshSets = mapLayers.GetMeshSettings(layerType);

		GameObject layerGO = new GameObject();
		layerGO.transform.parent = map.transform;
		layerGO.AddComponent<MeshCollider>();
		layerGO.AddComponent<MeshFilter>();

		Material mat = mapLayers.GetBasicTile(layerType).material;
		layerGO.AddComponent<MeshRenderer>().material = mat;

		MeshGenerator meshGen = layerGO.AddComponent<MeshGenerator>();
		meshGen.GenerateMesh(mas, tileSize, meshSets);
		meshGen.gameObject.AddComponent<NavMeshSourceTag>();
	}
}
