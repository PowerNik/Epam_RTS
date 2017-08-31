using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverUnitFactory : UnitFactory
{
    public RoverUnitFactory(PlayerManager player) : base(player){}
    public override Unit CreateUnit(Vector3 spawnPosition)
    {
        UnitSettings settings = GameManager.getUnitSettings(UnitType.Rover);
        GameObject newUnit = GameObject.Instantiate<GameObject>(settings.Prefab, spawnPosition, settings.Prefab.transform.rotation);
        Unit unit = newUnit.AddComponent<Unit>();
        unit.Health = settings.Health;
        unit.Cost = settings.Cost;
        unit.Fraction = settings.Fraction;
        unit.IsLoadable = settings.IsLoadable;
        unit.Icon = settings.Icon;
        unit.name = settings.Name;
        return unit;
    }
}
