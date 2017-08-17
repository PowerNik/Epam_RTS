using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager s_Instance = null;
    public static GameManager Instance
    {
        get
        {
            if (s_Instance == null) {

                s_Instance =  FindObjectOfType(typeof (GameManager)) as GameManager;
            }
            if (s_Instance == null) {
                GameObject obj = new GameObject("GameManager");
                s_Instance = obj.AddComponent(typeof (GameManager)) as GameManager;
            }
            return s_Instance;
        }
    }

    private static GameObject structuresPlaceHolder;
    public static GameObject StructuresPlaceHolder
    {
        get
        {
            return structuresPlaceHolder;
        }
    }

    [SerializeField]
    private StructureScriptableObject sso;
    [SerializeField]
    private List<Structure> structures;

    //TODELETE
    #region RegionForTestingSpawnUnity
    [SerializeField]
    GameObject unit1;
    [SerializeField]
    GameObject unit2;
    [SerializeField]
    GameObject unit3;
    [SerializeField]
    GameObject unit4;
    #endregion



    public static StructureSettings getStructureSettings(StructureSettingsType type)
    {
        List<StructureSettings> settings = Instance.sso.structureSettings;
        for (int i = 0; i < settings.Count; i++)
        {
            if (settings[i].Type == type)
                return Instance.sso.structureSettings[0];
        }
        return null;
    }

    public static void AddStructure(Structure structure)
    {
        Instance.structures.Add(structure);
    }

    public void Awake()
    {
        s_Instance = Instance;
        structures = new List<Structure>();
        structuresPlaceHolder = new GameObject("Structures");
    }

    private void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                CitizenStructureFactory csf = new CitizenStructureFactory();
                csf.SpawnBaseStructure(StructureSettingsType.CitizenBaseStructure_level1, hit.point);
            }
        }
        //TODELETE
        #region RegionForTestingSpawnUnity
        if (Input.GetKeyDown("["))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                Instantiate(unit1, hit.point, transform.rotation);
            }
        }
        if (Input.GetKeyDown("i"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                Instantiate(unit2, hit.point, transform.rotation);
            }
        }
        if (Input.GetKeyDown("o"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                Instantiate(unit3, hit.point, transform.rotation);
            }
        }
        if (Input.GetKeyDown("p"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                Instantiate(unit4, hit.point, transform.rotation);
            }
        }
        #endregion
    }

    // public void Start()
    // {
    // }




}
