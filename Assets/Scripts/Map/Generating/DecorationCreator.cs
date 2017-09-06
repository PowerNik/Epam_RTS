using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationCreator
{
	private DecorationSettings staticDecorSets;
	private DecorationSettings dynamicDecorSets;

	public void SetDecorationSettings(DecorationSettings staticDecorSets, DecorationSettings dynamicDecorSets)
	{
		this.staticDecorSets = staticDecorSets;
		this.dynamicDecorSets = dynamicDecorSets;
	}
}
