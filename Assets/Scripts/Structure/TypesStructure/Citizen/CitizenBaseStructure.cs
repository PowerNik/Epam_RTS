﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenBaseStructure : BaseStructure {
    
    public CitizenBaseStructure(StructureSettings settings, Vector3 SpawnPoint)
    {
        this.StructureGameObject = GameObject.Instantiate<GameObject>(settings.PrefubStructure, SpawnPoint, settings.PrefubStructure.transform.rotation);
        this.StructureGameObject.transform.parent = GameManager.StructuresPlaceHolder.transform;
        GameManager.AddStructure(this);
    }
}