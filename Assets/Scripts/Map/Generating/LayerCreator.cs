using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCreator
{
	private int tileCountX;
	private int tileCountZ;

	public MapLayerType[,] LayerGrid { get; private set; }
	private MapLayers mapLayers;
	private LayerGenerator layerGen;

	public LayerCreator(MapSettingsSO mapSettings)
	{
		MapSizeSettings mapSizeSets = mapSettings.GetMapSizeSettings();
		tileCountX = mapSizeSets.TileCountX;
		tileCountZ = mapSizeSets.TileCountZ;

		layerGen = new LayerGenerator(tileCountX, tileCountZ);

		mapLayers = mapSettings.GetMapLayers();
	}

	public void CreateLayers()
	{
		CreateLayerGround();
		SetLayer(MapLayerType.LayerWater);
		SetLayer(MapLayerType.LayerMountain);
	}

	private void CreateLayerGround()
	{
		LayerGrid = new MapLayerType[tileCountX, tileCountZ];

		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				LayerGrid[x, z] = MapLayerType.LayerGround;
			}
		}
	}

	/// <summary>
	/// Рандомно генерирует и устанавливает слой layerType поверх всех предыдущих
	/// </summary>
	/// <param name="layerType"></param>
	private void SetLayer(MapLayerType layerType)
	{
		GeneratorSettings genSets = mapLayers.GetGeneratorSettings(layerType);
		int[,] grid = layerGen.Generate(genSets);

		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				if (grid[x, z] == 1)
				{
					LayerGrid[x, z] = layerType;
				}
			}
		}
	}

	/// <summary>
	/// Возвращает карту расположения слоя layerType
	/// </summary>
	/// <returns></returns>
	public int[,] GetLayerMap(MapLayerType layerType)
	{
		int[,] mas = new int[tileCountX, tileCountZ];
		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				if (LayerGrid[x, z] == layerType)
				{
					mas[x, z] = 1;
				}
				else
				{
					mas[x, z] = 0;
				}
			}
		}

		return mas;
	}
}
