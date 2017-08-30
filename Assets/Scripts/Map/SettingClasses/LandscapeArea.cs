using UnityEngine;

[System.Serializable]
public class LandscapeArea
{
	[Range(-1, 1000)]
	public int minSize = -1;

	[Range(-1, 1000)]
	public int maxCount = -1;
}
