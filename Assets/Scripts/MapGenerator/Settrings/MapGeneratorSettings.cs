using UnityEngine;
using System.Collections;

[System.Serializable]
public class MapGeneratorSettings
{
	[SerializeField] private int width = 200;
	[SerializeField] private int length = 200;

	[SerializeField]
	[Range(0.2f, 1)]
	private float squareSize = 1;

	[Tooltip("Ключ для генерации карты")]
	[SerializeField]
	private string seed = "main";

	[Tooltip("Рандомно генерировать ключ?")]
	[SerializeField]
	private bool isRandomSeed = false;

	[Tooltip("Максимальный процент непроходимой местности")]
	[SerializeField]
	[Range(0, 100)]
	private int randomFillPercent = 47;
}
