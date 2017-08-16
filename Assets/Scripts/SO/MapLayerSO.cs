using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "MapLayerSettings", menuName = "My Scriptable Objects/MapLayer Settings", order = 1)]
public class MapLayerSO : ScriptableObject
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

	[SerializeField] private MapLayer[] mapLayer;

	public MapLayer[] MapSettings()
	{
		return mapLayer;
	}
}
