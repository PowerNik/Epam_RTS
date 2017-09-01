﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamerUnitFactory : UnitFactory
{
    public FlamerUnitFactory(PlayerManager player) : base(player){ }
    public override Unit CreateUnit(Vector3 spawnPosition)
    {
        UnitSettings settings = GameManager.getUnitSettings(UnitType.FootSoldier_Flamer);
        GameObject newUnit = GameObject.Instantiate<GameObject>(settings.Prefab, spawnPosition, settings.Prefab.transform.rotation);
        Unit unit = newUnit.AddComponent<Unit>();
        unit.Settings = settings;

        unit.playerOwner = this.playerOwner;

        return unit;
    }
}