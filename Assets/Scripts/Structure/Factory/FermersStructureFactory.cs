using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermersStructureFactory : StructureFactory
{
    public FermersStructureFactory(PlayerManager playerManagerRef) : base(playerManagerRef){ }

    public override Structure SpawnBaseStructure(Vector3 SpawnPosition)
    {
        return new FermersBaseStructure(SpawnPosition, playerManager.StructuresPlaceHolder.transform);
    }

    public override Structure SpawnExtractStructure(Vector3 SpawnPosition)
    {
        return new FermersExtractStructure();
    }

    public override Structure SpawnMilitaryStructure(Vector3 SpawnPosition)
    {
        return new FermersMilitaryStructure();
    }

    public override Structure SpawnScientificStructure(Vector3 SpawnPosition)
    {
        return new FermersScientificStructure();
    }
}
