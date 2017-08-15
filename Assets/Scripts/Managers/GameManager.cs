using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    private List<PlayerManager> players;

    #region MonoBehaviour
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            Debug.Log(players[0].GetRace());
        }
    }
    #endregion

    public static GameManager GetGameManager()
    {
        return instance;
    }

}
