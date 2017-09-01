using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NeutralSpawnSettings", menuName = "MapSettings/MainPoints/NeutralSpawnSettings", order = 3)]
public class NeutralPointSettingsSO : ScriptableObject
{
	[SerializeField]
	private NeutralPointSettings neutralPointSettings;

	public NeutralPointSettings GetNeutralPointSettings()
	{
		return neutralPointSettings;
	}
}
