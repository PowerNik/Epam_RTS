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

    public override GameResources GetStructurePrice(StructuresTypes type)
    {
        //TODO.Fix other buildings
        switch (type)
        {
            case (StructuresTypes.BaseStructure):
                return this.citizenBaseStructure.structurePrice;
            case (StructuresTypes.ExtractStucture):
                return this.citizenBaseStructure.structurePrice;
            case (StructuresTypes.MilitaryStructure):
                return this.citizenBaseStructure.structurePrice;
            case (StructuresTypes.ScientificStructure):
                return this.citizenBaseStructure.structurePrice;
            default:
                return new GameResources();
        }
    }

    public override Structure SpawnBaseStructure(Vector3 SpawnPosition)
    {
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
