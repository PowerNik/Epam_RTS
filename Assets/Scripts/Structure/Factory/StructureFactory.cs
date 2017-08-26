using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StructureFactory
{
    public abstract BaseStructure SpawnBaseStructure(Vector3 SpawnPosition);
    public abstract MilitaryStructure SpawnMilitaryStructure(Vector3 SpawnPosition);
    public abstract ScientificStructure SpawnScientificStructure(Vector3 SpawnPosition);
    public abstract ExtractStructure SpawnExtractStructure(Vector3 SpawnPosition);
}
