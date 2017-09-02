using UnityEngine;
using System.Collections;

[System.Serializable]
public class GeneratorSettings
{
	[Tooltip("Ключ для генерации")]
	[SerializeField]
	private string overseed = "";

	[Tooltip("Рандомно генерировать ключ?")]
	[SerializeField]
	private bool isRandom = false;

	[Tooltip("Максимальный процент заполнения генерируемыми объектами")]
	[Range(0, 100)]
	[SerializeField]
	private int fillPercent = 100;

	[Range(0, 100)]
	[SerializeField]
	private int smoothCount = 5;

	public void SetSeed(string seed)
	{
		if (overseed == "")
		{
			overseed = seed;
		};
	}

	public string GetOverseed()
	{
		return overseed;
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
