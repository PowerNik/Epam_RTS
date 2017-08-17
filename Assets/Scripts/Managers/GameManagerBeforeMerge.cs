using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManagerBeforeMerge : MonoBehaviour
{
    private static GameManagerBeforeMerge instance = null;

    private List<PlayerManager> players;

    [SerializeField]
    PlayerManager playerPrefab;

    delegate void InitPlayerDelegate();
    InitPlayerDelegate initPlayer;

    #region Init
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            players = new List<PlayerManager>();
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public static GameManagerBeforeMerge GetGameManager()
    {
        return instance;
    }

    public void StartGame(int raceId)
    {
        SceneManager.LoadScene("MainScene");
        if((Race)raceId == Race.Citizen)
        {
            initPlayer += InitCitizenPlayer;
        }else{
            initPlayer += InitFermerPlayer;
        }
    }
    #region SceneManagment
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "Menu"){
            initPlayer.Invoke();
        }
    }
    #endregion

    void InstantiatePlayerManager()
    {
            players.Add(
                Instantiate(playerPrefab, Vector3.zero, transform.rotation) as PlayerManager
            );
    }

    void InitCitizenPlayer()
    {
        InstantiatePlayerManager();
        players.Last().playerFactory = new CitizenStructureFactory();
    }

    void InitFermerPlayer()
    {
        InstantiatePlayerManager();
        players.Last().playerFactory = new FermersStructureFactory();
    }

}
