using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DecorationSettings", menuName = "MapSettings/DecorationSettings", order = 7)]
public class DecorationSettingsSO : ScriptableObject
{
	[SerializeField]
	private string seed = "";
	private string mainSeed = "";

	[SerializeField]
	private DecorationSettings[] decorationSettings;

	public DecorationSettings[] GetDecorationSettings()
	{
		return decorationSettings;
	}

	public void SetMainSeed(string mainSeed)
	{
		this.mainSeed = mainSeed;

		for(int i = 0; i < decorationSettings.Length; i++)
		{
			decorationSettings[i].SetMainSeed(GetSeed());
		}
	}

	public string GetSeed()
	{
		if (seed != "")
		{
			return seed;
		}
		else
		{
			return mainSeed;
		}
	}
}
