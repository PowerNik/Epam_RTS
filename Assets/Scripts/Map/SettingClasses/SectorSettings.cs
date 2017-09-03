using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SectorSettings
{
	[Header("Число регионов на карте (для баз)")]
	[Range(3, 5)]
	public int regionCountX = 4;
	[Range(3, 5)]
	public int regionCountZ = 4;

	[Space(10)]
	[Header("Число секторов в каждом регионе")]
	[Range(3, 5)]
	public int sectorCountX = 4;
	[Range(3, 5)]
	public int sectorCountZ = 4;

	[Space(10)]
	[Tooltip("В одном регионе располагается только база, без ресурсов?")]
	public bool isUnique = true;
}
