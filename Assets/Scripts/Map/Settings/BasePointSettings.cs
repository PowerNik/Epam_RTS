using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasePointSettings
{
	[Tooltip("Размер базы горожан")]
	public int citizenBaseSize = 20;

	[Space(10)]
	[Tooltip("Число и размер баз фермеров")]
	public int[] fermerBases = new int[3] { 10, 15, 10 };

	[Space(20)]
	[Tooltip("Число секторов по Х. В одном секторе будет только одна база")]
	public int sectorsAtX = 4;

	[Tooltip("Число секторов по Z. В одном секторе будет только одна база")]
	public int sectorsAtZ = 4;

	public bool isCitizenAtCenter = true;

	[Tooltip("Ключ для генерации размещения баз")]
	public string seed = "main";
}
