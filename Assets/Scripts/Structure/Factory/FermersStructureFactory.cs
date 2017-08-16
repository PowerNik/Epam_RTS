using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermersStructureFactory : StructureFactory
{

    public override BaseStructure SpawnBaseStructure(StructureSettingsType settings, Vector3 SpawnPosition)
    {
        return new FermersBaseStructure();
    }

    public override ExtractStructure SpawnExtractStructure(StructureSettingsType settings, Vector3 SpawnPosition)
    {
        return new FermersExtractStructure();
    }

    public override MilitaryStructure SpawnMilitaryStructure(StructureSettingsType settings, Vector3 SpawnPosition)
    {
        return new FermersMilitaryStructure();
    }

    public override ScientificStructure SpawnScientificStructure(StructureSettingsType settings, Vector3 SpawnPosition)
    {
        return new FermersScientificStructure();
    }
}
