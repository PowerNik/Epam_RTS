using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermerBuilderUnitFactory : UnitFactory
{
    public FermerBuilderUnitFactory(PlayerManager player) : base(player){ }
    public override Unit CreateUnit(Vector3 spawnPosition)
    {
        return new Unit();
    }
}
