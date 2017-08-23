using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasePointSettings
{
	[Tooltip("Число и размер баз горожан")]
	public int[] citizenBases = new int[1] { 30 };

	[Space(10)]
	[Tooltip("Число и размер баз фермеров")]
	public int[] fermerBases = new int[3] { 15, 20, 15 };

	[Space(20)]
	[Tooltip("Число секторов по Х. В одном секторе будет только одна база")]
	public int sectorsAtX = 4;

	[Tooltip("Число секторов по Z. В одном секторе будет только одна база")]
	public int sectorsAtZ = 4;

	public bool isCitizenAtCenter = true;

	[Tooltip("Ключ для генерации размещения баз")]
	public string seed = "main";
}
