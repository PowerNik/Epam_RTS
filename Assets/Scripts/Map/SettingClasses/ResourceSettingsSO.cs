using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceSettings", menuName = "MapSettings/ResourceSettings", order = 8)]
public class ResourceSettingsSO : ScriptableObject
{
	[SerializeField]
	private ResourceSettings resourceSettings;

	public ResourceSettings GetResourceSettings()
	{
		return resourceSettings;
	}
}
