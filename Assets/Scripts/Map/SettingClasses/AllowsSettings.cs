using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllowsSettings
{
	static public Allows MainPointBaseCitizen { get { return new Allows(0, 1, 0); } }
	static public Allows MainPointBaseFermer { get { return new Allows(0, 2, 0); } }

	static public Allows MainPointExtractCitizen { get { return new Allows(0, 0, 1); } }
	static public Allows MainPointExtractFermer { get { return new Allows(0, 0, 2); } }

	static public Allows MainPointSpawnNeutrals { get { return new Allows(0, 0, 0); } }


	static public Allows LayerGround { get { return new Allows(7, 2, 0); } }
	static public Allows LayerNoGround { get { return new Allows(7, 0, 0); } }


	static public Allows Framing { get { return new Allows(6, 0, 0); } }

	static public Allows Texturing { get { return new Allows(7, 0, 0); } }
	static public Allows Scenery { get { return new Allows(1, 0, 0); } }
	static public Allows Dynamic { get { return new Allows(7, 0, 0); } }
}
