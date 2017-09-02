using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapSettingsManager", menuName = "MapSettings/MapSettingsManager", order = 100)]
public class MapSettingsManagerSO : ScriptableObject
{
	private string mainSeed;

	[SerializeField]
	private MapSizeSettingsSO mapSizeSettings;

	[SerializeField]
	private MainPointsSettingsSO mainPointsSettings;

	[SerializeField]
	private LayerSettingsSO layerTileSettings;


	[SerializeField]
	private HeightMapSettingsSO heightMapSettings;

	[SerializeField]
	private ColoringTileSettingsSO coloringTileSettings;

	[SerializeField]
	private DecorationSettingsSO decorationSettings;

	private void OnEnable()
	{
		mainSeed = mapSizeSettings.GetMapSizeSettings().mainSeed;
	}

	public MapSizeSettings GetMapSizeSettings()
	{
		return mapSizeSettings.GetMapSizeSettings();
	}

	public MainPointsSettingsSO GetMainPointsSettings()
	{
		mainPointsSettings.SetMainSeed(mainSeed);
		return mainPointsSettings;
	}

	public LayerSettings GetLayerTileSettings()
	{
		layerTileSettings.GetLayerSettings().SetMainSeed(mainSeed);
		return layerTileSettings.GetLayerSettings();
	}

	public HeightMapSettings GetHeightMapSettings()
	{
		return heightMapSettings.GetHeightMapSettings();
	}

	public ColoringTileSettings GetColoringTileSettings()
	{
		return coloringTileSettings.GetColoringTileSettings();
	}

	public DecorationSettings[] GetDecorationSettings()
	{
		return decorationSettings.GetDecorationSettings();
	}
}
