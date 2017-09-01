using UnityEngine;
using System.Collections;

[System.Serializable]
public class GeneratorSettings
{
	[Tooltip("Ключ для генерации")]
	public string seed = "main";

	[Tooltip("Рандомно генерировать ключ?")]
	public bool isRandom = false;

	[Tooltip("Максимальный процент заполнения генерируемыми объектами")]
	[Range(0, 100)]
	public int fillPercent = 100;

	[Range(0, 100)]
	public int smoothCount = 5;
}
