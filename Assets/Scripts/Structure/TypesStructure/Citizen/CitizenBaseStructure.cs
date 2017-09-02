using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenBaseStructure : StructureSettings
{
    public Structure Build(Vector3 SpawnPoint, Transform placeHolder)
    {
        this.Init(GameManager.getStructureSettings(StructuresTypes.BaseStructure, Race.Citizen));
        Debug.Log("Spawn Base Structure");
        GameObject spawnedStructure = GameObject.Instantiate<GameObject>(this.StructureGameObject, SpawnPoint, this.StructureGameObject.transform.rotation);
        Structure spawnedStructureMB = spawnedStructure.AddComponent<Structure>();
        spawnedStructureMB.Init(this);
        spawnedStructure.transform.SetParent(placeHolder);
        return spawnedStructureMB;
    }
}
