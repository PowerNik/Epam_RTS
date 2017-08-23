using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitScriptableObject", menuName = "Unit")]
public class UnitScriptableObject : ScriptableObject
{
    public List<UnitSettings> unitSettings;
}
