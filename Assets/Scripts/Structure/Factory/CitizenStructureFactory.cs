using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenStructureFactory : StructureFactory
{
    public CitizenStructureFactory(PlayerManager playerManagerRef) : base(playerManagerRef){}

    CitizenBaseStructure citizenBaseStructure = new CitizenBaseStructure();
    CitizenExtractStructure citizenExtractStructure = new CitizenExtractStructure();
    CitizenMilitaryStructure citizenMilitaryStructure = new CitizenMilitaryStructure();
    CitizenScientificStructure citizenScientificStructure = new CitizenScientificStructure();

    public override Structure SpawnBaseStructure(Vector3 SpawnPosition)
    {
        playerManager.playerResources -= citizenBaseStructure.structurePrice;
        return citizenBaseStructure.Build(SpawnPosition, playerManager.StructuresPlaceHolder.transform);
    }

    public override Structure SpawnExtractStructure(Vector3 SpawnPosition)
    {
        return new CitizenExtractStructure();
    }

    public override Structure SpawnMilitaryStructure(Vector3 SpawnPosition)
    {
        return new CitizenMilitaryStructure();
    }

    public override Structure SpawnScientificStructure(Vector3 SpawnPosition)
    {
        return new CitizenScientificStructure();
    }
}
