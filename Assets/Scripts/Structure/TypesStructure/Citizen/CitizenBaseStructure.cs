using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenBaseStructure : StructureSettings
{
    public CitizenBaseStructure()
    {
        //Get settings from SCriptableObject
        this.Init(GameManager.getStructureSettings(StructuresTypes.BaseStructure, Race.Citizen));
    }
    public Structure Build(Vector3 SpawnPoint, Transform placeHolder)
    {   
        GameObject spawnedStructure = GameObject.Instantiate<GameObject>(this.StructureGameObject, SpawnPoint, this.StructureGameObject.transform.rotation);
        Structure spawnedStructureMB = spawnedStructure.AddComponent<Structure>();
        //Place settings in Structure class after instantiating
        spawnedStructureMB.Init(this);
        spawnedStructure.transform.SetParent(placeHolder);
        return spawnedStructureMB;
    }
}
