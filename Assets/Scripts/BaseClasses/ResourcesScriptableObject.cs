using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Resource
public enum GameResourceType
{
    foodResource = 1,
    equipResource = 2,
    metalResource = 3,
    energyResource = 4
}

[System.Serializable]
public struct GameResource
{
    public delegate void ChangeValueDelegate(int newVal);
    public ChangeValueDelegate ChangeValue;

    [SerializeField]
    private GameResourceType type;
    [SerializeField]
    private int value;
    [SerializeField]
    public Sprite backgroundUI;

    public int Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            ChangeValue.Invoke(value);
        }
    }
    public static GameResource operator-(GameResource left, GameResource right)
    {
        GameResource result;
        result = left;
        result.Value = left.Value - right.Value;
        return result;
    }
}

[System.Serializable]
public class GameResources
{
    public string ParamName;

    public Race playerRace;

    [SerializeField]
    public GameResource foodResource;
    [SerializeField]
    public GameResource equipResource;
    [SerializeField]
    public GameResource specialResource;

    public static GameResources operator-(GameResources left, GameResources right)
    {
        left.foodResource -= right.foodResource;
        left.equipResource -= right.equipResource;
        left.specialResource -= right.specialResource;
        return left;
    }
}
#endregion


[CreateAssetMenu(fileName = "StartupResources", menuName = "StartUp")]
public class ResourcesScriptableObject : ScriptableObject
{
    public List<GameResources> playerResources;
}
