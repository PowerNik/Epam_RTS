using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Structures", menuName = "Structures")]
public class StructureScriptableObject : ScriptableObject 
{
    public List<StructureSettings> structures;
}
