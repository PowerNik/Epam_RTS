using UnityEngine;

[System.Serializable]
public class DomainSettings
{
	[Range(3, 100)]
	public int mainRadius = 10;

	[Range(5, 100)]
	public int nonDecorableRadius = 15;
}
