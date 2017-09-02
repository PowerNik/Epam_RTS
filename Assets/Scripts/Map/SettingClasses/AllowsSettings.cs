using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllowsSettings
{
	static public Allows GetAllow(TileType type)
	{
		switch(type)
		{
			default:
			case TileType.GroundLayer:
				return LayerGround;
			case TileType.WaterLayer:
			case TileType.MountainLayer:
				return LayerNoGround;

			case TileType.FramingFoothill:
			case TileType.FramingWaterSide:
				return Framing;

			case TileType.CitizenBasePoint:
				return BasePointCitizen;
			case TileType.FermersBasePoint:
				return BasePointFermers;

			case TileType.CitizenExtractPoint:
				return ExtractPointCitizen;
			case TileType.FermersExtractPoint:
				return ExtractPointFermer;

			case TileType.AggressiveNeuntralsPoint:
			case TileType.PeacefulNeuntralsPoint:
			case TileType.TradePoint:
				return NaturePoint;
		}
	}

	static private Allows LayerGround { get { return new Allows(7, 2, 0); } }
	static private Allows LayerNoGround { get { return new Allows(7, 0, 0); } }

	static private Allows Framing { get { return new Allows(6, 0, 0); } }

	static private Allows BasePointCitizen { get { return new Allows(0, 1, 0); } }
	static private Allows BasePointFermers { get { return new Allows(0, 2, 0); } }

	static private Allows ExtractPointCitizen { get { return new Allows(0, 0, 1); } }
	static private Allows ExtractPointFermer { get { return new Allows(0, 0, 2); } }

	static private Allows NaturePoint { get { return new Allows(0, 0, 0); } }
}
