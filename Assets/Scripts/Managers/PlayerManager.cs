using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Race
{
    Spectator = 0,
    Citizen = 1,
    Fermer = 2
}

public class PlayerManager : MonoBehaviour
{
    private int foodResource,
                equipResource,
                specialResource;

    public StructureFactory playerFactory { get;set; }
    
    List<Structure> playerStructures;

    List<Unit> playerUnits;

    private static GameObject structuresPlaceHolder;
    public static GameObject StructuresPlaceHolder
    {
        get
        {
            return structuresPlaceHolder;
        }
    }

    #region MonoBehaviour
    void Awake()
    {
        playerStructures = new List<Structure>();
        playerUnits = new List<Unit>();

        //DELETE
        #region TestInit
        playerFactory = new CitizenStructureFactory();
        #endregion
    }
    #endregion

}
