using UnityEngine;
using System.Collections;

[System.Serializable]
public class GeneratorSettings
{
	[SerializeField]
	private string seed = "";
	private string mainSeed = "";

	[SerializeField]
	private bool isRandom = false;

	[Tooltip("Максимальный процент заполнения генерируемыми объектами")]
	[Range(0, 100)]
	[SerializeField]
	private int fillPercent = 100;

	[Range(0, 100)]
	[SerializeField]
	private int smoothCount = 5;

	public void SetMainSeed(string mainSeed)
	{
		this.mainSeed = mainSeed;
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

	public bool IsRandom()
	{
		return isRandom;
	}

	public int GetFillPercent()
	{
		return fillPercent;
	}

	public int GetSmoothCount()
	{
		return smoothCount;
	}
}
