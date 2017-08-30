using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapSettingsManager", menuName = "MapSettings/MapSettingsManager", order = 100)]
public class MapSettingsManagerSO : ScriptableObject
{
	[SerializeField]
	private MapSizeSettingsSO mapSizeSettings;

	[SerializeField]
	private HeightMapSettingsSO heightMapSettings;

	[SerializeField]
	private BasePointsSettingsSO basePointsSettings;

	[SerializeField]
	private LayerTileSettingsSO layerTileSettings;

	[SerializeField]
	private FramingTileSettingsSO framingTileSettings;

	[SerializeField]
	private ColoringTileSettingsSO coloringTileSettings;

	[SerializeField]
	private DecorationSettingsSO decorationSettings;

	[SerializeField]
	private ResourceSettingsSO resourceSettings;


	public MapSizeSettings GetMapSizeSettings()
	{
		return mapSizeSettings.GetMapSizeSettings();
	}

	public HeightMapSettings GetHeightMapSettings()
	{
		return heightMapSettings.GetHeightMapSettings();
	}

	public BasePointsSettings GetBasePointsSettings()
	{
		return basePointsSettings.GetBasePointsSettings();
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

	public ResourceSettings GetResourceSettings()
	{
		return resourceSettings.GetResourceSettings();
	}
}
