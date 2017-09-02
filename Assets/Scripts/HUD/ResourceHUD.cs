using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ResourceType
{
	Food = 1,
	Eqipment = 2,
	Special = 3
}
public class ResourceHUD : MonoBehaviour 
{

	[SerializeField]
	ResourceType resource;

    private Text resourceValuePresentation;

    void Awake()
    {
        resourceValuePresentation = gameObject.GetComponent<Text>();
    }

    public void SetPlayer(PlayerManager player)
    {
        switch (resource)
        {
            case ResourceType.Food:
                player.foodResChange = ChangeValue;
                break;
            case ResourceType.Eqipment:
                player.equipResChange = ChangeValue;
                break;
            case ResourceType.Special:
                player.specialResChange = ChangeValue;
                break;
        }
    }

    private void ChangeValue(int newVal)
    {
        resourceValuePresentation.text = newVal.ToString();
    }
}
