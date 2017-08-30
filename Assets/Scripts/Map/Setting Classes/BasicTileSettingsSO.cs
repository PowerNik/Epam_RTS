using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BasicTileSettings", menuName = "Map/Basic Tile Settings", order = 1)]
public class BasicTileSettingsSO : ScriptableObject
{
	[Header("Layer tiles")]
	public BasicTile groundTile = new BasicTile(BasicTileType.Ground);
	public BasicTile waterTile = new BasicTile(BasicTileType.Water);
	public BasicTile mountainTile = new BasicTile(BasicTileType.Mountain);

	[Header("Framing tiles")]
	public BasicTile waterSideTile = new BasicTile(BasicTileType.WaterSide);
	public BasicTile foothillTile = new BasicTile(BasicTileType.Foothill);

	private Dictionary<BasicTileType, BasicTile> tileDict = new Dictionary<BasicTileType, BasicTile>() { };

	private void OnEnable()
	{
		tileDict.Add(BasicTileType.Ground, groundTile);
		tileDict.Add(BasicTileType.Water, waterTile);
		tileDict.Add(BasicTileType.Mountain, mountainTile);

		tileDict.Add(BasicTileType.WaterSide, waterSideTile);
		tileDict.Add(BasicTileType.Foothill, foothillTile);
	}

	public Dictionary<BasicTileType, BasicTile> GetBasicTileDictionary()
	{
		return tileDict;
	}

	/// <summary>
	/// Возвращает пары типа (layerType, framingTile)
	/// </summary>
	/// <returns></returns>
	public Dictionary<BasicTileType, BasicTileType> GetFramingTilePairs()
	{
		Dictionary<BasicTileType, BasicTileType> dict = new Dictionary<BasicTileType, BasicTileType>();

		dict.Add(BasicTileType.Water, BasicTileType.WaterSide);
		dict.Add(BasicTileType.Mountain, BasicTileType.Foothill);

		return dict;
	}
}
