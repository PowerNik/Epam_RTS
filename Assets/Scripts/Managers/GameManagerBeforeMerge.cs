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
    private InitGameDelegate initGame;

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
        //TODO.Delete test block of code
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
        initGame += InitResourceBoard;
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

    #region Player

    void InstantiatePlayerManager()
    {
        players.Add(
            Instantiate(playerPrefab, Vector3.zero, transform.rotation)
        );
    }

    void InitCitizenPlayer()
    {
        InstantiatePlayerManager();
        players.Last().playerRace = Race.Citizen;
        players.Last().playerFactory = new CitizenStructureFactory();
    }

    void InitFermerPlayer()
    {
        InstantiatePlayerManager();
        players.Last().playerRace = Race.Fermer;
        players.Last().playerFactory = new FermersStructureFactory();
    }

    #endregion


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

    #region HUD
    //TODO.Rewrite hardcode setting player.Will works only when playing local with bots.
    void InitResourceBoard()
    {
        
        Transform ResourceHUDTransform = GameObject.FindGameObjectWithTag("HUD").transform;
        GameObject ResHUD;
        if (players[0].playerRace == Race.Citizen)
        {
            ResHUD = (GameObject)Instantiate(Resources.Load("ResourceCitizenHUD"), ResourceHUDTransform);
        }
        else
        {
            ResHUD = (GameObject)Instantiate(Resources.Load("ResourceFermerHUD"), ResourceHUDTransform);
        }
        ResourceHUD[] resources = ResHUD.GetComponentsInChildren<ResourceHUD>();
        for (int it = 0; it < resources.Length; it++)
        {
            resources[it].SetPlayer(players[0]);
        }
        //Instantiate(ResourceBoard, Vector3.zero, transform.rotation);
    }

    #endregion
}
