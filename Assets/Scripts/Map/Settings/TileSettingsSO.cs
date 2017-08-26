using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TileSettings", menuName = "Map/Tile Settings", order = 0)]
public class TileSettingsSO : ScriptableObject
{
	[SerializeField] private Tile[] tiles;

	public Tile[] GetTiles()
	{
		return tiles;
	}

	public Dictionary<TileType, Tile> GetTileDictionary()
	{
		Dictionary<TileType, Tile> dict = new Dictionary<TileType, Tile>();
		for(int i = 0; i < tiles.Length; i++)
		{
			dict.Add(tiles[i].TileType, tiles[i]);
		}

		return dict;
	}
}
