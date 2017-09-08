using UnityEngine;

[System.Serializable]
public class LandscapeSettings
{
	[Range(-1, 1000)]
	public int minSize = -1;

	[Range(-1, 1000)]
	public int maxCount = -1;
}
