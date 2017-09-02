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
	private SectorSettings localSectors;


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
		return NeutralPointSettings.GetNeutralPointSettings();
	}

	public SectorSettings GetRegionSettings()
	{
		return regionSettings;
	}

	public SectorSettings GetLocalSectorsSettings()
	{
		return localSectors;
	}

	public string GetSeed()
	{
		return seed;
	}

	public Dictionary<TileType, Tile> GetMainPointsDictionary()
	{
		Dictionary<TileType, Tile> dict = new Dictionary<TileType, Tile>();

		MainPointTile mpTile = BasePointSettings.GetBasePointSettings().GetBasePoints(Race.Citizen)[0];
		dict.Add(mpTile.GetTileType(), mpTile.GetTile());
		mpTile = BasePointSettings.GetBasePointSettings().GetBasePoints(Race.Fermer)[0];
		dict.Add(mpTile.GetTileType(), mpTile.GetTile());

		mpTile = ExtractPointSettings.GetExtractPointSettings().GetExtractPoint(Race.Citizen);
		dict.Add(mpTile.GetTileType(), mpTile.GetTile());
		mpTile = ExtractPointSettings.GetExtractPointSettings().GetExtractPoint(Race.Fermer);
		dict.Add(mpTile.GetTileType(), mpTile.GetTile());

		mpTile = TradePointSettings.GetTradePointSettings().GetTradePoint(Race.Citizen);
		dict.Add(mpTile.GetTileType(), mpTile.GetTile());

		mpTile = NeutralPointSettings.GetNeutralPointSettings().GetNeutralPoint(Race.Citizen);
		dict.Add(mpTile.GetTileType(), mpTile.GetTile());
		mpTile = NeutralPointSettings.GetNeutralPointSettings().GetNeutralPoint(Race.Fermer);
		dict.Add(mpTile.GetTileType(), mpTile.GetTile());

		return dict;
	}
}
