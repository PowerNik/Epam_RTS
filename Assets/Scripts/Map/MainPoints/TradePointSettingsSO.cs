using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TradePointSettings", menuName = "MapSettings/MainPoints/TradePointSettings", order = 2)]
public class TradePointSettingsSO : ScriptableObject
{
	[SerializeField]
	private TradePointSettings TradePointSettings;

	public TradePointSettings GetTradePointSettings()
	{
		return TradePointSettings;
	}
}
