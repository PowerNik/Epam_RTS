using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;

//TODO.Rename class after merge
public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public float GameClock { get; private set; }

    private List<PlayerManager> players;

    [SerializeField]
    PlayerManager playerPrefab;

    [SerializeField]
    MapManager mapManagerPrefab;

    #region ScriptableObjects
    [SerializeField]
    private StructureScriptableObject sso;
    [SerializeField]
    private UnitScriptableObject uso;
    [SerializeField]
    private ResourcesScriptableObject rso;

    public static StructureSettings getStructureSettings(StructuresTypes type,Race race)
    {
        List<StructureSettings> settings = instance.sso.structures;
        for (int i = 0; i < settings.Count; i++)
        {
            if (settings[i].structureType == type && settings[i].structureRace == race)
            {
                return settings[i];
            }
        }
        return null;
    }

    public static UnitSettings getUnitSettings(UnitType type)
    {
        List<UnitSettings> settings = instance.uso.unitSettings;
        for (int i = 0; i < settings.Count; i++)
        {
            if (settings[i].Type == type)
                return settings[i];
        }
        return null;
    }

    public static GameResources getStartupInitResources(Race playerRace)
    {
        //TODO.Read about ICloneable
        List<GameResources> startupInit = new List<GameResources>();
        instance.rso.playerResources.ForEach((item) =>
        {
            startupInit.Add(new GameResources(item));
        });
        for (int i = 0; i < startupInit.Count; i++)
        {
            if (startupInit[i].playerRace == playerRace)
            {
                return startupInit[i];
            }
        }
        return null;
    }
    #endregion

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

    public static GameManager GetGameManager()
    {
        return instance;
    }

    public void StartGame(int raceId)
    {
        //SceneManager.LoadScene("MainScene");
        initGame += InstantiateMapManager;
        if ((Race)raceId == Race.Citizen)
        {
            initGame += InitCitizenPlayer;
        }else{
            initGame += InitFermerPlayer;
        }
        //HUD
        initGame += InitResourceBoard;
        initGame += InitErrorOutput;

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

        //TO DELETE
        MouseManager.Current.CurrentPlayer = players.Last();
    }

    void InitCitizenPlayer()
    {
        Debug.Log("IntCitizenPlayer");
        Vector3[] citizenStartPoint = new Vector3[1];
        citizenStartPoint[0] = MapManagerInstance.GetCitizenBasePoint();
        InstantiatePlayerManager();
        players.Last().Init(Race.Citizen, citizenStartPoint);
    }

    void InitFermerPlayer()
    {
        InstantiatePlayerManager();
        //players.Last().playerRace = Race.Fermer;
        //players.Last().playerFactory = new FermersStructureFactory(players.Last());
        //players.Last().startPoints = MapManagerInstance.GetFermerBasePoints();
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

    #region Camera
    //TODO.Add spawning main & minimap camera
    void InitCamera()
    {
    }


    #endregion

    #region HUD
    private Text errorOutput;
    private Timer errorOutputTimer;

    private UnityAction ClearErrorOutput()
    {
        return delegate ()
        {
            errorOutput.text = "";
        };
    }

    public void PrintError(string errorText)
    {
        errorOutput.text = errorText;
        errorOutputTimer.Init(5, false, ClearErrorOutput());
    }

    //TODO.Rewrite hardcode setting player.Will works only when playing local with bots.
    void InitResourceBoard()
    {
        //Debug.Log("IntResourceBoard");
        Transform ResourceHUDTransform = GameObject.FindGameObjectWithTag("ResourceHUD").transform;
        GameObject FoodResHUD = (GameObject)Instantiate(Resources.Load("ResourceBlock"), ResourceHUDTransform);
        GameObject EquipResHUD = (GameObject)Instantiate(Resources.Load("ResourceBlock"), ResourceHUDTransform);
        GameObject SpecResHUD = (GameObject)Instantiate(Resources.Load("ResourceBlock"), ResourceHUDTransform);
        players[0].InitResourceHUD(FoodResHUD, EquipResHUD, SpecResHUD);
    }

    void InitErrorOutput()
    {
        Transform HUDTransform = GameObject.FindGameObjectWithTag("HUD").transform;
        GameObject errorOutputInstance = Instantiate(Resources.Load("ErrorOutput"), HUDTransform) as GameObject;
        errorOutput = errorOutputInstance.GetComponent<Text>();
        errorOutputTimer = errorOutput.gameObject.AddComponent<Timer>();
    }

    #endregion
}
