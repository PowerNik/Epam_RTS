using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Structure {

    protected GameObject structureGameObject;

    public GameObject StructureGameObject
    {
        get
        {
            return structureGameObject;
        }

        set
        {
            structureGameObject = value;
        }
    }

}
