using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DecorationSettings", menuName = "Map/Decoration Settings", order = 3)]
public class DecorationSettingsSO : ScriptableObject
{
	[SerializeField]
	private Decoration[] decorations;

	public Decoration[] GetDecorations()
	{
		return decorations;
	}
}
