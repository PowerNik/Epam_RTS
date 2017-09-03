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
	[Tooltip("Ключ для генерации точек")]
	[SerializeField]
	private string seed = "";
	private string mainSeed = "";

	[SerializeField]
	private SectorSettings sectorSettings;

	public void SetMainSeed(string mainSeed)
	{
		this.mainSeed = mainSeed;
	}

	public IMainPointSettings GetMainPointSettings(MainPointType type)
	{
		IMainPointSettings sets;
		switch (type)
		{
			default:
			case MainPointType.Base:
				sets = BasePointSettings.GetBasePointSettings();
				break;

			case MainPointType.Extract:
				sets = ExtractPointSettings.GetExtractPointSettings();
				break;

			case MainPointType.Trade:
				sets = TradePointSettings.GetTradePointSettings();
				break;

			case MainPointType.Neutral:
				sets = NeutralPointSettings.GetNeutralPointSettings();
				break;
		}

		if (seed != "")
		{
			sets.SetMainSeed(seed);
		}
		else
		{
			sets.SetMainSeed(mainSeed);
		}

		return sets;
	}

	public SectorSettings GetSectorsSettings()
	{
		return sectorSettings;
	}
}
