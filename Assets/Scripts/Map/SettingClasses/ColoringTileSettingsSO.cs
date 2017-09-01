using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColoringTileSettings", menuName = "MapSettings/ColoringTileSettings", order = 6)]
public class ColoringTileSettingsSO : ScriptableObject
{
	[SerializeField]
	private ColoringTileSettings сoloringTileSettings;

	public ColoringTileSettings GetColoringTileSettings()
	{
		return сoloringTileSettings;
	}
}
