using UnityEngine;

[System.Serializable]
public class DomainSettings
{
	[Range(1, 100)]
	public int mainSize = 3;

	[Range(2, 100)]
	public int nonDecorableSize = 5;
}
