using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenStructureFactory : StructureFactory
{
    public override BaseStructure SpawnBaseStructure(StructureSettingsType settingsType, Vector3 SpawnPosition)
    {
        StructureSettings settings = GameManager.getStructureSettings(settingsType);
   
        CitizenBaseStructure newStructure = new CitizenBaseStructure(settings, SpawnPosition);
        return newStructure;
    }

    public override ExtractStructure SpawnExtractStructure(StructureSettingsType settings, Vector3 SpawnPosition)
    {
        return new CitizenExtractStructure();
    }

    public override MilitaryStructure SpawnMilitaryStructure(StructureSettingsType settings, Vector3 SpawnPosition)
    {
        return new CitizenMiitaryStructure();
    }

    public override ScientificStructure SpawnScientificStructure(StructureSettingsType settings, Vector3 SpawnPosition)
    {
        
        return new CitizenScientificStructure();
    }
}
