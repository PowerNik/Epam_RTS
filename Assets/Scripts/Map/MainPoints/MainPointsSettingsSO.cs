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

	[Tooltip("Сетка карты для баз")]
	[SerializeField]
	private SectorSettings regionSettings;

	[SerializeField]
	[Tooltip("Сетка каждого региона для ресурсов, рынков, нейтралов")]
	private SectorSettings localSectorSettings;

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
			case MainPointType.BasePoint:
				sets = BasePointSettings.GetBasePointSettings();
				break;

			case MainPointType.ExtractPoint:
				sets = ExtractPointSettings.GetExtractPointSettings();
				break;

			case MainPointType.TradePoint:
				sets = TradePointSettings.GetTradePointSettings();
				break;

			case MainPointType.NeutralPoint:
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

	public SectorSettings GetRegionSettings()
	{
		return regionSettings;
	}

	public SectorSettings GetLocalSectorsSettings()
	{
		return localSectorSettings;
	}
}
