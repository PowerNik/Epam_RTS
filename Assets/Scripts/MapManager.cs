using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	[SerializeField]
	private TileSO tileSettings;

	public Tile[] GetTiles()
	{
		return tileSettings.GetAllSettings();
	}

	public TileType[] GetTileTypes()
	{
		return tileSettings.GetTileTypes();
	}
}
