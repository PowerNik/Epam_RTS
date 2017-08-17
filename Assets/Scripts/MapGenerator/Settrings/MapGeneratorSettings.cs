using UnityEngine;
using System.Collections;

[System.Serializable]
public class MapGeneratorSettings
{
	public int width = 200;
	public int length = 200;

	[Range(0.2f, 1)]
	public float tileSize = 1;

	[Tooltip("Ключ для генерации карты")]
	public string seed = "main";

	[Tooltip("Рандомно генерировать ключ?")]
	public bool isRandomSeed = false;

	[Tooltip("Максимальный процент непроходимой местности")]
	[Range(0, 100)]
	public int randomFillPercent = 47;
}
