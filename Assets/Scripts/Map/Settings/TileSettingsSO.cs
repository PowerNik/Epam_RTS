using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "TileSettings", menuName = "Map/Tile Settings", order = 0)]
public class TileSettingsSO : ScriptableObject
{
	[SerializeField] private Tile[] tiles;

	public Tile[] GetAllSettings()
	{
		return tiles;
	}
}
