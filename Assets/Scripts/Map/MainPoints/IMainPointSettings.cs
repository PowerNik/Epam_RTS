using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMainPointSettings
{
	void SetMainSeed(string seed);

	string GetSeed();

	bool GetIsCenter();

	MainPointTile GetMainPoint(Race race);

	Dictionary<TileType, Tile> GetTileDictionary();

	int GetMainPointCount(Race race);
}
