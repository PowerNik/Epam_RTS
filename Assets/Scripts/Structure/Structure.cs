using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    protected StructureSettings structureSettings;

    public void Init(StructureSettings settings)
    {
        structureSettings = settings;
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy:" + Time.deltaTime);
    }
}
