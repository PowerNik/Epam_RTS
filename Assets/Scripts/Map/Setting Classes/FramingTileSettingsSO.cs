using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FramingTileSettings", menuName = "MapSettings/FramingTileSettings", order = 5)]
public class FramingTileSettingsSO : ScriptableObject
{
	[SerializeField]
	private FramingTileSettings framingTileSettings;

	public FramingTileSettings GetFramingTileSettings()
	{
		return framingTileSettings;
	}
}
