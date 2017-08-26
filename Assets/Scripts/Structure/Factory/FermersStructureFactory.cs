using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermersStructureFactory : StructureFactory
{

    public override BaseStructure SpawnBaseStructure(Vector3 SpawnPosition)
    {
        return new FermersBaseStructure();
    }

    public override ExtractStructure SpawnExtractStructure(Vector3 SpawnPosition)
    {
        return new FermersExtractStructure();
    }

    public override MilitaryStructure SpawnMilitaryStructure(Vector3 SpawnPosition)
    {
        return new FermersMilitaryStructure();
    }

    public override ScientificStructure SpawnScientificStructure(Vector3 SpawnPosition)
    {
        return new FermersScientificStructure();
    }
}
