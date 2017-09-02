using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExtractPointSettings", menuName = "MapSettings/MainPoints/ExtractPointSettings", order = 1)]
public class ExtractPointSettingsSO : ScriptableObject
{
	[SerializeField]
	private ExtractPointSettings extractPointSettings;

	public ExtractPointSettings GetExtractPointSettings()
	{
		return extractPointSettings;
	}
}
