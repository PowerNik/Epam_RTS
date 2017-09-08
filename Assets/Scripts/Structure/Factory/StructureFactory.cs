using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StructureFactory
{
    public StructureFactory(PlayerManager playerManagerRef)
    {
        playerManager = playerManagerRef;
    }
    protected PlayerManager playerManager { get; set; }

    public abstract GameResources GetStructurePrice(StructuresTypes type);
    public abstract Structure SpawnBaseStructure(Vector3 SpawnPosition);
    public abstract Structure SpawnMilitaryStructure(Vector3 SpawnPosition);
    public abstract Structure SpawnScientificStructure(Vector3 SpawnPosition);
    public abstract Structure SpawnExtractStructure(Vector3 SpawnPosition);
}
