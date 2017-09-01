using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasePointSettings", menuName = "MapSettings/MainPoints/BasePointSettings", order = 0)]

public class BasePointSettingsSO : ScriptableObject
{
	[SerializeField]
	private BasePointSettings basePointSets;

	public BasePointSettings GetBasePointSettings()
	{
		return basePointSets;
	}
}
