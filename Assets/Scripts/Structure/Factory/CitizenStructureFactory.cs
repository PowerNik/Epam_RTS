using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenStructureFactory : StructureFactory
{
    public override BaseStructure SpawnBaseStructure(Vector3 SpawnPosition)
    {
        return new CitizenBaseStructure(SpawnPosition);
    }

    public override ExtractStructure SpawnExtractStructure(Vector3 SpawnPosition)
    {
        return new CitizenExtractStructure();
    }

    public override MilitaryStructure SpawnMilitaryStructure(Vector3 SpawnPosition)
    {
        return new CitizenMiitaryStructure();
    }

    public override ScientificStructure SpawnScientificStructure(Vector3 SpawnPosition)
    {
        return new CitizenScientificStructure();
    }
}
