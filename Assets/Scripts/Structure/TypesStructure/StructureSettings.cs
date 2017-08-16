using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StructureSettings {
    [SerializeField]
    private StructureSettingsType type;
    [SerializeField]
    private GameObject prefubStructure;

    public GameObject PrefubStructure
    {
        get
        {
            return prefubStructure;
        }
    }

    public StructureSettingsType Type
    {
        get
        {
            return type;
        }
    }
    //private StructureSettingsType nextLevel;

}

public enum StructureSettingsType
{
    CitizenBaseStructure_level1 = 0
}
