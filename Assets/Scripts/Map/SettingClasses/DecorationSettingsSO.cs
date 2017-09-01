using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DecorationSettings", menuName = "MapSettings/DecorationSettings", order = 7)]
public class DecorationSettingsSO : ScriptableObject
{
	[SerializeField]
	private DecorationSettings[] decorationSettings;

	public DecorationSettings[] GetDecorationSettings()
	{
		return decorationSettings;
	}
}
