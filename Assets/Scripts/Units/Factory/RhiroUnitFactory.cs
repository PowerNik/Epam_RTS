using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhiroUnitFactory : UnitFactory
{
    public RhiroUnitFactory(PlayerManager player) : base(player){ }
    public override Unit CreateUnit(Vector3 spawnPosition)
    {
        UnitSettings settings = GameManager.getUnitSettings(UnitType.Rhiro);
        GameObject newUnit = GameObject.Instantiate<GameObject>(settings.Prefab, spawnPosition, settings.Prefab.transform.rotation);
        Unit unit = newUnit.AddComponent<Unit>();
        unit.Settings = settings;
        unit.Health = settings.MaxHealth;
        unit.playerOwner = this.playerOwner;
        return unit;
    }
}
