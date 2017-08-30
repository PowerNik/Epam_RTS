using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeightMapSettings", menuName = "MapSettings/HeightMapSettings", order = 1)]
public class HeightMapSettingsSO : ScriptableObject
{
	[SerializeField]
	private HeightMapSettings heightMapSettings;

	public HeightMapSettings GetHeightMapSettings()
	{
		return heightMapSettings;
	}
}
