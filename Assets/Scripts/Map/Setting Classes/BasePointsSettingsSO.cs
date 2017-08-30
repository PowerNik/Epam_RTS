using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasePointsSettings", menuName = "MapSettings/BasePointsSettings", order = 3)]

public class BasePointsSettingsSO : ScriptableObject
{
	[SerializeField]
	private BasePointsSettings basePointsSettings;

	public BasePointsSettings GetBasePointsSettings()
	{
		return basePointsSettings;
	}
}
