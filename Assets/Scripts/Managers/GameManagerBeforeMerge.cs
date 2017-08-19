using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManagerBeforeMerge : MonoBehaviour
{
    private static GameManagerBeforeMerge instance = null;

    public float GameClock { get; private set; }

    private List<PlayerManager> players;

    [SerializeField]
    PlayerManager playerPrefab;

    [SerializeField]
    MapManager mapManagerPrefab;

    public MapManager MapManagerInstance { get; private set; }

    delegate void InitGameDelegate();
    InitGameDelegate initGame;

    delegate void GameClockDelegate(float TimeUpdate);
    private GameClockDelegate updateClock;

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

        #region TestInit_TODELETE
        StartGame(1);
        #endregion
    }

    #endregion

    public static GameManagerBeforeMerge GetGameManager()
    {
        return instance;
    }

    public void StartGame(int raceId)
    {
        //SceneManager.LoadScene("MainScene");
        initGame += InstantiateMapManager;
        if((Race)raceId == Race.Citizen)
        {
            initGame += InitCitizenPlayer;
        }else{
            initGame += InitFermerPlayer;
        }
        updateClock = UpdateGameClock;
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
            initGame.Invoke();
        }
    }
    #endregion

    void InstantiateMapManager()
    {
        MapManagerInstance = Instantiate(mapManagerPrefab, Vector3.zero, transform.rotation);
    }

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

    #region MonoBehaviour
    void Update()
    {
        if (updateClock != null)
        {
            updateClock.Invoke(Time.deltaTime);
        }
    }
    #endregion

    private void UpdateGameClock(float timeUpdate)
    {
        GameClock += timeUpdate;
    }
}
