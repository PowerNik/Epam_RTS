using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BasicTileSettings", menuName = "Map/Basic Tile Settings", order = 1)]
public class BasicTileSettingsSO : ScriptableObject
{
	[SerializeField] private BasicTile[] basicTiles;

	public BasicTile[] GetTiles()
	{
		return basicTiles;
	}

	public Dictionary<BasicTileType, BasicTile> GetBasicTileDictionary()
	{
		Dictionary<BasicTileType, BasicTile> dict = new Dictionary<BasicTileType, BasicTile>();
		for(int i = 0; i < basicTiles.Length; i++)
		{
			dict.Add(basicTiles[i].TileType, basicTiles[i]);
		}

		return dict;
	}

	public BasicTile[] GetBasicTiles()
	{
		return basicTiles;
	}
}
