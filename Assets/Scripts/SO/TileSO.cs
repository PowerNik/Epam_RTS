using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "TileSettings", menuName = "My Scriptable Objects/Tile Settings", order = 0)]
public class TileSO : ScriptableObject
{
	[SerializeField] private Tile[] tiles;

	public Tile GetBulletSettings(TileType type)
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
}
