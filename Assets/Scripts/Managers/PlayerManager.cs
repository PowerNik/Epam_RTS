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

    private Race playerRace;

    PlayerManager(Race race)
    {
        playerRace = race;
    }
    public int GetRace()
    {
        try
        {
            return (int) playerRace;
        }
        catch (NullReferenceException nExp)
        {
            Debug.Log("Race not setted");
            return -1;
        }
    }
}
