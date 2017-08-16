using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StructureFactory {

    public abstract BaseStructure SpawnBaseStructure(StructureSettingsType settings, Vector3 SpawnPosition);
    public abstract MilitaryStructure SpawnMilitaryStructure(StructureSettingsType settings, Vector3 SpawnPosition);
    public abstract ScientificStructure SpawnScientificStructure(StructureSettingsType settings, Vector3 SpawnPosition);
    public abstract ExtractStructure SpawnExtractStructure(StructureSettingsType settings, Vector3 SpawnPosition);
}
