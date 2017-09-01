using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenBuilderUnitFactory : UnitFactory
{
    public CitizenBuilderUnitFactory(PlayerManager player) : base(player){ }
    public override Unit CreateUnit(Vector3 spawnPosition)
    {
        UnitSettings settings = GameManager.getUnitSettings(UnitType.CitizenBuilder);
        GameObject newUnit = GameObject.Instantiate<GameObject>(settings.Prefab, spawnPosition, settings.Prefab.transform.rotation);
        Unit unit = newUnit.AddComponent<Unit>();
        unit.Settings = settings;
        //Add actions
        newUnit.AddComponent<BuildMainStructureAction>();

        unit.playerOwner = this.playerOwner;
        
        return unit;
    }
}
