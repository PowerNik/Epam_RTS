using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "TileSettings", menuName = "Map/Tile Settings", order = 0)]
public class TileSettingsSO : ScriptableObject
{
	[SerializeField] private Tile[] tiles;

	public Tile GetTileSettings(TileType type)
	{
		foreach (var tile in tiles)
		{
			if (tile.TileType == type)
			{
				return tile;
			}
		}

		return tiles[0];
	}

	public Tile[] GetAllSettings()
	{
		return tiles;
	}

	public TileType[] GetTileTypes()
	{
		TileType[] res = new TileType[tiles.Length];

		for(int i = 0; i < tiles.Length; i++)
		{
			res[i] = tiles[i].TileType;
		}

		return res;
	}
}
