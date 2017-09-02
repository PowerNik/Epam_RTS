using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainPointsSettings", menuName = "MapSettings/MainPoints/MainPointsSettings", order = 20)]
public class MainPointsSettingsSO : ScriptableObject
{
	[SerializeField]
	private BasePointSettingsSO BasePointSettings;

	[SerializeField]
	private ExtractPointSettingsSO ExtractPointSettings;

	[SerializeField]
	private TradePointSettingsSO TradePointSettings;

	[SerializeField]
	private NeutralPointSettingsSO NeutralPointSettings;

	[Space(10)]
	[Tooltip("Главный ключ для генерации точек")]
	[SerializeField]
	private string seed = "main";

	[Tooltip("Сетка карты для баз")]
	[SerializeField]
	private SectorSettings regionSettings;

	[SerializeField]
	[Tooltip("Сетка каждого региона для ресурсов, рынков, нейтралов")]
	private SectorSettings localSectorSettings;


	public string GetSeed()
	{
		return seed;
	}

	public BasePointSettings GetBasePointSettings()
	{
		BasePointSettings.GetBasePointSettings().SetSeed(seed);
		return BasePointSettings.GetBasePointSettings();
	}

	public ExtractPointSettings GetExtractPointSettings()
	{
		ExtractPointSettings.GetExtractPointSettings().SetSeed(seed);
		return ExtractPointSettings.GetExtractPointSettings();
	}

	public TradePointSettings GetTradePointSettings()
	{
		TradePointSettings.GetTradePointSettings().SetSeed(seed);
		return TradePointSettings.GetTradePointSettings();
	}

	public NeutralPointSettings GetNeutralSpawnSettings()
	{
		NeutralPointSettings.GetNeutralPointSettings().SetSeed(seed);
		return NeutralPointSettings.GetNeutralPointSettings();
	}

	public SectorSettings GetRegionSettings()
	{
		return regionSettings;
	}

	public SectorSettings GetLocalSectorsSettings()
	{
		return localSectorSettings;
	}
}
