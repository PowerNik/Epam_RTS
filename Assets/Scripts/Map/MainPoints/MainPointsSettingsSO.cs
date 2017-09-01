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
	private NeutralPointSettingsSO NeutralSpawnSettings;

	[Space(10)]
	[SerializeField]
	private SectorSettings basePointsSectors;

	[SerializeField]
	private SectorSettings smallSectors;


	public BasePointSettings GetBasePointSettings()
	{
		return BasePointSettings.GetBasePointSettings();
	}

	public ExtractPointSettings GetExtractPointSettings()
	{
		return ExtractPointSettings.GetExtractPointSettings();
	}

	public TradePointSettings GetTradePointSettings()
	{
		return TradePointSettings.GetTradePointSettings();
	}

	public NeutralPointSettings GetNeutralSpawnSettings()
	{
		return NeutralSpawnSettings.GetNeutralPointSettings();
	}

	public SectorSettings GetBaseSectorSettings()
	{
		return basePointsSectors;
	}

	public SectorSettings GetSmallSettings()
	{
		return smallSectors;
	}
}
