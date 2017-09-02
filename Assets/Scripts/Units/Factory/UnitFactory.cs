using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitFactory {

    protected PlayerManager playerOwner;
    public UnitFactory(PlayerManager player)
    {
        playerOwner = player;
    }

    public abstract Unit CreateUnit(Vector3 spawnPosition);

}
