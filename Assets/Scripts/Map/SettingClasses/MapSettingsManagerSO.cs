using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapSettingsManager", menuName = "MapSettings/MapSettingsManager", order = 100)]
public class MapSettingsManagerSO : ScriptableObject
{
	[SerializeField]
	private MapSizeSettingsSO mapSizeSettings;

	[SerializeField]
	private MainPointsSettingsSO mainPointsSettings;


	[SerializeField]
	private HeightMapSettingsSO heightMapSettings;

	[SerializeField]
	private LayerTileSettingsSO layerTileSettings;

	[SerializeField]
	private FramingTileSettingsSO framingTileSettings;

	[SerializeField]
	private ColoringTileSettingsSO coloringTileSettings;

	[SerializeField]
	private DecorationSettingsSO decorationSettings;


	public MapSizeSettings GetMapSizeSettings()
	{
		return mapSizeSettings.GetMapSizeSettings();
	}

	public MainPointsSettingsSO GetMainPointsSettings()
	{
		return mainPointsSettings;
	}

	public HeightMapSettings GetHeightMapSettings()
	{
		return heightMapSettings.GetHeightMapSettings();
	}

	public LayerTileSettings GetLayerTileSettings()
	{
		return layerTileSettings.GetLayerTileSettings();
	}

	public FramingTileSettings GetFramingTileSettings()
	{
		return framingTileSettings.GetFramingTileSettings();
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
