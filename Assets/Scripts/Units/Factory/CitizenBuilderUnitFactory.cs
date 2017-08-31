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
        unit.Health = settings.Health;
        unit.Cost = settings.Cost;
        unit.Fraction = settings.Fraction;
        unit.IsLoadable = settings.IsLoadable;
        unit.Icon = settings.Icon;
        unit.name = settings.Name;
        //Add actions
        newUnit.AddComponent<BuildMainStructureAction>();

        unit.playerOwner = this.playerOwner;
        return unit;
    }
}
