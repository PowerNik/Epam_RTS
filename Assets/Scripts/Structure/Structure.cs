using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : Unit
{
    protected StructureSettings structureSettings;

    public void Init(StructureSettings settings)
    {
        structureSettings = settings;
        this.Settings = settings;
        Health = settings.MaxHealth;
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy:" + Time.deltaTime);
    }
}
