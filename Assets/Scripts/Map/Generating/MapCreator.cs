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
	private Dictionary<TileType, Tile> tileDict;

	public MapLayerType[,] LayerGrid { get; private set; }

	private float tileSize;


	public MapCreator(MapSettingsSO mapSettings, Dictionary<TileType, Tile> tileDict)
	{
		SetParams(mapSettings, tileDict);
		MapCreating();
	}

	private void SetParams(MapSettingsSO mapSettings, Dictionary<TileType, Tile> tileDict)
	{
		mapLayers = mapSettings.GetMapLayers();

		MapSizeSettings mapSizeSets = mapSettings.GetMapSizeSettings();
		tileSize = mapSizeSets.tileSize;

		this.tileDict = tileDict;

		layerCreator = new LayerCreator(mapSettings);
		basePointsGen = new BasePointsGenerator(mapSettings);
	}

	private void MapCreating()
	{
		layerCreator.CreateLayers();
		basePointsGen.CreateBasePoints(layerCreator.LayerGrid);

		LayerGrid = basePointsGen.LayerGrid;	
		CitizenBasePoint = basePointsGen.CitizenBasePoint;
		FermerBasePoints = basePointsGen.FermerBasePoints;
	}

	public void CreateMapMesh(GameObject map)
	{
		CreateMeshForLayer(map, MapLayerType.LayerGround);
		CreateMeshForLayer(map, MapLayerType.LayerWater);
		CreateMeshForLayer(map, MapLayerType.LayerMountain);
	}

	private void CreateMeshForLayer(GameObject map, MapLayerType layerType)
	{
		int[,] mas = layerCreator.GetLayerMap(layerType);
		MeshSettings meshSets = mapLayers.GetMeshSettings(layerType);

		GameObject layerGO = new GameObject();
		layerGO.transform.parent = map.transform;
		layerGO.AddComponent<MeshCollider>();
		layerGO.AddComponent<MeshFilter>();

		TileType tileType = mapLayers.GetTileType(layerType);
		Material mat = tileDict[tileType].material;
		layerGO.AddComponent<MeshRenderer>().material = mat;

		MeshGenerator meshGen = layerGO.AddComponent<MeshGenerator>();
		meshGen.GenerateMesh(mas, tileSize, meshSets);
		meshGen.gameObject.AddComponent<NavMeshSourceTag>();
	}
}
