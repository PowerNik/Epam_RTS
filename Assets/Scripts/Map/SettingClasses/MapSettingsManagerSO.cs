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
	private DecorationSettingsSO staticDecorSettings;

	[SerializeField]
	private DecorationSettingsSO dynamicDecorSettings;

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

	public DecorationSettings[] GetStaticDecorationSettings()
	{
		staticDecorSettings.SetMainSeed(mainSeed);
		return staticDecorSettings.GetDecorationSettings();
	}

	public DecorationSettings[] GetDynamicDecorSettings()
	{
		dynamicDecorSettings.SetMainSeed(mainSeed);
		return dynamicDecorSettings.GetDecorationSettings();
	}
}
